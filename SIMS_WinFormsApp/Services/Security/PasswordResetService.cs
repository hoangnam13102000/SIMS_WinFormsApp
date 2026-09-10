using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using SIMS_WinFormsApp.DAL;
using SIMS_WinFormsApp.Models;
using SIMS_WinFormsApp.Services.Mail;
using SIMS_WinFormsApp.UI.I18n;

namespace SIMS_WinFormsApp.Services.Security
{
    public sealed class PasswordResetService
    {
        public const int OtpTtlSeconds = 5 * 60;
        public const int VerifiedTtlSeconds = 10 * 60;
        public const int ChallengeTtlSeconds = 15 * 60;
        public const int ResendCooldownSeconds = 60;
        public const int RateWindowSeconds = 15 * 60;
        public const int MaxSendsPerWindow = 3;
        public const int MaxVerifyAttempts = 5;

        private const int OtpBound = 1_000_000;
        private static readonly Regex EmailPattern =
            new Regex(@"^[\w.+-]+@[\w-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);

        private static readonly Lazy<PasswordResetService> LazyInstance =
            new Lazy<PasswordResetService>(() => new PasswordResetService());
        public static PasswordResetService Instance => LazyInstance.Value;

        private readonly ConcurrentDictionary<string, Challenge> _challenges = new ConcurrentDictionary<string, Challenge>();
        private readonly ConcurrentDictionary<string, RateBucket> _sendRates = new ConcurrentDictionary<string, RateBucket>();

        private readonly UserRepository _userRepository;
        private readonly MailSender _mailSender;

        private PasswordResetService() : this(new UserRepository(), new MailSender()) { }

        // Constructor phụ để có thể unit test sau này (inject fake repo/mailer).
        internal PasswordResetService(UserRepository userRepository, MailSender mailSender)
        {
            _userRepository = userRepository;
            _mailSender = mailSender;
        }

        public RequestResult RequestOtp(string usernameInput, string emailInput)
        {
            string username = NormalizeUsername(usernameInput);
            string email = NormalizeEmail(emailInput);
            if (!IsValidIdentityInput(username, email))
                return new RequestResult(RequestStatus.InvalidInput, null, null, 0);

            DateTime now = DateTime.UtcNow;
            CleanupExpired(now);

            RateDecision rate = AcquireSendPermit(RateKey(username, email), now);
            if (!rate.Allowed)
                return new RequestResult(RequestStatus.RateLimited, null, null, rate.RetryAfterSeconds);

            User account;
            try
            {
                account = _userRepository.FindForPasswordReset(username, email);
            }
            catch (Exception)
            {
                return new RequestResult(RequestStatus.SystemError, null, null, 0);
            }

            string challengeId = Guid.NewGuid().ToString("N");
            bool decoy = account == null;
            var challenge = new Challenge(challengeId, decoy ? -1 : account.UserId, username, email, decoy, now);
            _challenges[challengeId] = challenge;

            if (decoy)
            {
                lock (challenge.Lock)
                {
                    challenge.LastSentAt = now;
                    challenge.SendCount = 1;
                }
                return AcceptedRequest(challengeId, email);
            }

            string otp = GenerateOtp();
            lock (challenge.Lock)
            {
                challenge.Sending = true;
                challenge.OtpHash = HashOtp(challengeId, otp);
                challenge.OtpExpiresAt = now.AddSeconds(OtpTtlSeconds);
            }

            try
            {
                SendOtpMail(email, otp);
            }
            catch (Exception)
            {
                InvalidateAndRemove(challenge);
                return new RequestResult(RequestStatus.MailFailed, null, null, 0);
            }

            lock (challenge.Lock)
            {
                if (challenge.Cancelled || !_challenges.TryGetValue(challengeId, out var current) || current != challenge)
                {
                    ClearOtpHash(challenge);
                    return new RequestResult(RequestStatus.SystemError, null, null, 0);
                }
                challenge.Sending = false;
                challenge.LastSentAt = now;
                challenge.SendCount = 1;
            }
            return AcceptedRequest(challengeId, email);
        }

        public VerifyResult VerifyOtp(string challengeId, string inputCode)
        {
            if (challengeId == null || inputCode == null || !Regex.IsMatch(inputCode, @"^\d{6}$"))
                return new VerifyResult(VerifyStatus.InvalidCode, MaxVerifyAttempts);

            DateTime now = DateTime.UtcNow;
            if (!_challenges.TryGetValue(challengeId, out var challenge))
                return new VerifyResult(VerifyStatus.NotFound, 0);

            lock (challenge.Lock)
            {
                if (challenge.Cancelled) return new VerifyResult(VerifyStatus.NotFound, 0);
                if (now >= challenge.ChallengeExpiresAt)
                {
                    InvalidateAndRemoveNoLock(challenge);
                    return new VerifyResult(VerifyStatus.Expired, 0);
                }
                if (challenge.Verified) return new VerifyResult(VerifyStatus.AlreadyVerified, 0);
                if (challenge.Sending || challenge.OtpHash == null) return RecordInvalidAttempt(challenge);
                if (now >= challenge.OtpExpiresAt)
                    return new VerifyResult(VerifyStatus.Expired, Math.Max(0, MaxVerifyAttempts - challenge.VerifyAttempts));

                byte[] inputHash = HashOtp(challengeId, inputCode);
                bool matches = FixedTimeEquals(challenge.OtpHash, inputHash);

                if (!matches || challenge.Decoy) return RecordInvalidAttempt(challenge);

                challenge.Verified = true;
                challenge.VerifiedUntil = now.AddSeconds(VerifiedTtlSeconds);
                challenge.VerifyAttempts = 0;
                ClearOtpHash(challenge);
                return new VerifyResult(VerifyStatus.Success, MaxVerifyAttempts);
            }
        }

        public ResendResult ResendOtp(string challengeId)
        {
            if (challengeId == null) return new ResendResult(ResendStatus.NotFound, 0);

            DateTime now = DateTime.UtcNow;
            if (!_challenges.TryGetValue(challengeId, out var challenge))
                return new ResendResult(ResendStatus.NotFound, 0);

            string otp = null;
            lock (challenge.Lock)
            {
                if (challenge.Cancelled) return new ResendResult(ResendStatus.NotFound, 0);
                if (now >= challenge.ChallengeExpiresAt)
                {
                    InvalidateAndRemoveNoLock(challenge);
                    return new ResendResult(ResendStatus.Expired, 0);
                }
                if (challenge.Verified) return new ResendResult(ResendStatus.AlreadyVerified, 0);
                if (challenge.Sending || challenge.Resetting) return new ResendResult(ResendStatus.InProgress, 1);

                double cooldownRemaining = (challenge.LastSentAt.AddSeconds(ResendCooldownSeconds) - now).TotalSeconds;
                if (cooldownRemaining > 0)
                    return new ResendResult(ResendStatus.Cooldown, CeilSeconds(cooldownRemaining));

                RateDecision rate = AcquireSendPermit(RateKey(challenge.Username, challenge.Email), now);
                if (!rate.Allowed)
                    return new ResendResult(ResendStatus.RateLimited, rate.RetryAfterSeconds);

                challenge.Sending = true;
                challenge.VerifyAttempts = 0;
                ClearOtpHash(challenge);
                if (!challenge.Decoy)
                {
                    otp = GenerateOtp();
                    challenge.OtpHash = HashOtp(challenge.Id, otp);
                    challenge.OtpExpiresAt = now.AddSeconds(OtpTtlSeconds);
                }
            }

            if (!challenge.Decoy)
            {
                try
                {
                    SendOtpMail(challenge.Email, otp);
                }
                catch (Exception)
                {
                    lock (challenge.Lock)
                    {
                        ClearOtpHash(challenge);
                        challenge.Sending = false;
                    }
                    return new ResendResult(ResendStatus.MailFailed, 0);
                }
            }

            lock (challenge.Lock)
            {
                if (challenge.Cancelled)
                {
                    ClearOtpHash(challenge);
                    return new ResendResult(ResendStatus.NotFound, 0);
                }
                challenge.Sending = false;
                challenge.LastSentAt = now;
                challenge.SendCount++;
                if (challenge.Decoy) challenge.OtpExpiresAt = now.AddSeconds(OtpTtlSeconds);
            }
            return new ResendResult(ResendStatus.Success, ResendCooldownSeconds);
        }

        public ResetResult ResetPassword(string challengeId, string newPassword)
        {
            if (challengeId == null)
                return new ResetResult(ResetStatus.NotFound, PasswordValidationStatus.Valid);

            PasswordValidationStatus validation = ValidatePassword(newPassword);
            if (validation != PasswordValidationStatus.Valid)
                return new ResetResult(ResetStatus.InvalidPassword, validation);

            DateTime now = DateTime.UtcNow;
            if (!_challenges.TryGetValue(challengeId, out var challenge))
                return new ResetResult(ResetStatus.NotFound, PasswordValidationStatus.Valid);

            lock (challenge.Lock)
            {
                if (challenge.Cancelled)
                    return new ResetResult(ResetStatus.NotFound, PasswordValidationStatus.Valid);
                if (!challenge.Verified)
                    return new ResetResult(ResetStatus.NotVerified, PasswordValidationStatus.Valid);
                if (now >= challenge.VerifiedUntil || now >= challenge.ChallengeExpiresAt)
                {
                    InvalidateAndRemoveNoLock(challenge);
                    return new ResetResult(ResetStatus.SessionExpired, PasswordValidationStatus.Valid);
                }
                if (challenge.Resetting)
                    return new ResetResult(ResetStatus.InProgress, PasswordValidationStatus.Valid);
                challenge.Resetting = true;
            }

            UserRepository.PasswordResetUpdateResult updateResult;
            try
            {
                updateResult = _userRepository.ResetPasswordFromRecovery(challenge.UserId, newPassword);
            }
            catch (Exception)
            {
                updateResult = UserRepository.PasswordResetUpdateResult.UpdateFailed;
            }

            if (updateResult == UserRepository.PasswordResetUpdateResult.Success)
            {
                _challenges.TryRemove(challengeId, out _);
                lock (challenge.Lock)
                {
                    challenge.Cancelled = true;
                    challenge.Resetting = false;
                    ClearOtpHash(challenge);
                }
                return new ResetResult(ResetStatus.Success, PasswordValidationStatus.Valid);
            }

            lock (challenge.Lock) { challenge.Resetting = false; }

            if (updateResult == UserRepository.PasswordResetUpdateResult.SameAsOldPassword)
                return new ResetResult(ResetStatus.SameAsOldPassword, PasswordValidationStatus.Valid);

            if (updateResult == UserRepository.PasswordResetUpdateResult.AccountUnavailable)
            {
                InvalidateAndRemove(challenge);
                return new ResetResult(ResetStatus.AccountUnavailable, PasswordValidationStatus.Valid);
            }
            return new ResetResult(ResetStatus.UpdateFailed, PasswordValidationStatus.Valid);
        }

        public void CancelChallenge(string challengeId)
        {
            if (challengeId == null) return;
            if (_challenges.TryRemove(challengeId, out var challenge))
            {
                lock (challenge.Lock)
                {
                    challenge.Cancelled = true;
                    ClearOtpHash(challenge);
                }
            }
        }

        public static PasswordValidationStatus ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return PasswordValidationStatus.Required;
            if (password.Length < 8 || password.Length > 72) return PasswordValidationStatus.Length;
            if (char.IsWhiteSpace(password[0]) || char.IsWhiteSpace(password[password.Length - 1]))
                return PasswordValidationStatus.Whitespace;

            bool hasLetter = false, hasDigit = false, hasNonWhitespace = false;
            foreach (char c in password)
            {
                if (char.IsLetter(c)) hasLetter = true;
                if (char.IsDigit(c)) hasDigit = true;
                if (!char.IsWhiteSpace(c)) hasNonWhitespace = true;
            }
            if (!hasNonWhitespace) return PasswordValidationStatus.Whitespace;
            if (!hasLetter) return PasswordValidationStatus.Letter;
            if (!hasDigit) return PasswordValidationStatus.Digit;
            if (Encoding.UTF8.GetByteCount(password) > 72) return PasswordValidationStatus.ByteLength;
            return PasswordValidationStatus.Valid;
        }

        public static string MaskEmail(string emailInput)
        {
            string email = NormalizeEmail(emailInput);
            int at = email.IndexOf('@');
            if (at <= 0 || at == email.Length - 1) return "***";
            string local = email.Substring(0, at);
            string domain = email.Substring(at);
            return local.Length == 1 ? local + "***" + domain : local[0] + "***" + domain;
        }

        private void SendOtpMail(string toEmail, string otp)
        {
            _mailSender.Send(toEmail, Lang.Get("forgot.mail.subject"), Lang.Get("forgot.mail.body", otp));
        }

        private RequestResult AcceptedRequest(string challengeId, string email) =>
            new RequestResult(RequestStatus.Accepted, challengeId, MaskEmail(email), ResendCooldownSeconds);

        private VerifyResult RecordInvalidAttempt(Challenge challenge)
        {
            challenge.VerifyAttempts++;
            int remaining = Math.Max(0, MaxVerifyAttempts - challenge.VerifyAttempts);
            if (remaining == 0)
            {
                ClearOtpHash(challenge);
                return new VerifyResult(VerifyStatus.TooManyAttempts, 0);
            }
            return new VerifyResult(VerifyStatus.InvalidCode, remaining);
        }

        private static string GenerateOtp()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[4];
                rng.GetBytes(bytes);
                uint value = BitConverter.ToUInt32(bytes, 0);
                int otpNumber = (int)(value % OtpBound);
                return otpNumber.ToString("D6");
            }
        }

        private static byte[] HashOtp(string challengeId, string otp)
        {
            using (var sha = SHA256.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(challengeId + ":" + otp));
            }
        }

        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            int diff = 0;
            for (int i = 0; i < a.Length; i++) diff |= a[i] ^ b[i];
            return diff == 0;
        }

        private void CleanupExpired(DateTime now)
        {
            foreach (var kv in _challenges)
            {
                var challenge = kv.Value;
                bool remove;
                lock (challenge.Lock)
                {
                    remove = !challenge.Sending && !challenge.Resetting &&
                             (challenge.Cancelled || now >= challenge.ChallengeExpiresAt ||
                              (challenge.Verified && now >= challenge.VerifiedUntil));
                }
                if (remove && _challenges.TryRemove(kv.Key, out _))
                {
                    lock (challenge.Lock)
                    {
                        challenge.Cancelled = true;
                        ClearOtpHash(challenge);
                    }
                }
            }

            foreach (var kv in _sendRates)
            {
                if (kv.Value.IsEmptyAfterCleanup(now))
                    _sendRates.TryRemove(kv.Key, out _);
            }
        }

        private void InvalidateAndRemove(Challenge challenge)
        {
            _challenges.TryRemove(challenge.Id, out _);
            lock (challenge.Lock)
            {
                challenge.Cancelled = true;
                challenge.Sending = false;
                challenge.Resetting = false;
                ClearOtpHash(challenge);
            }
        }

        // Gọi khi đã đang giữ lock(challenge.Lock) - không lock lại tránh deadlock.
        private void InvalidateAndRemoveNoLock(Challenge challenge)
        {
            _challenges.TryRemove(challenge.Id, out _);
            challenge.Cancelled = true;
            challenge.Sending = false;
            challenge.Resetting = false;
            ClearOtpHash(challenge);
        }

        private static void ClearOtpHash(Challenge challenge) => challenge.OtpHash = null;

        private RateDecision AcquireSendPermit(string key, DateTime now)
        {
            var bucket = _sendRates.GetOrAdd(key, _ => new RateBucket());
            return bucket.TryAcquire(now);
        }

        private static string NormalizeUsername(string username) => username == null ? "" : username.Trim();
        private static string NormalizeEmail(string email) => email == null ? "" : email.Trim().ToLowerInvariant();

        private static bool IsValidIdentityInput(string username, string email) =>
            username.Length >= 3 && username.Length <= 50 && EmailPattern.IsMatch(email);

        private static string RateKey(string username, string email) => username + "\n" + email;

        private static int CeilSeconds(double seconds) => Math.Max(1, (int)Math.Ceiling(seconds));

        // ===================== Kiểu dữ liệu =====================

        private sealed class Challenge
        {
            public readonly object Lock = new object();
            public readonly string Id;
            public readonly int UserId;
            public readonly string Username;
            public readonly string Email;
            public readonly bool Decoy;
            public readonly DateTime ChallengeExpiresAt;

            public byte[] OtpHash;
            public DateTime OtpExpiresAt;
            public int VerifyAttempts;
            public int SendCount;
            public DateTime LastSentAt;
            public bool Verified;
            public DateTime VerifiedUntil;
            public bool Sending;
            public bool Resetting;
            public bool Cancelled;

            public Challenge(string id, int userId, string username, string email, bool decoy, DateTime createdAt)
            {
                Id = id;
                UserId = userId;
                Username = username;
                Email = email;
                Decoy = decoy;
                ChallengeExpiresAt = createdAt.AddSeconds(ChallengeTtlSeconds);
            }
        }

        private sealed class RateBucket
        {
            private readonly Queue<DateTime> _sends = new Queue<DateTime>();

            public RateDecision TryAcquire(DateTime now)
            {
                lock (_sends)
                {
                    RemoveExpired(now);
                    if (_sends.Count >= MaxSendsPerWindow)
                    {
                        double retry = (_sends.Peek().AddSeconds(RateWindowSeconds) - now).TotalSeconds;
                        return new RateDecision(false, CeilSeconds(retry));
                    }
                    _sends.Enqueue(now);
                    return new RateDecision(true, 0);
                }
            }

            public bool IsEmptyAfterCleanup(DateTime now)
            {
                lock (_sends)
                {
                    RemoveExpired(now);
                    return _sends.Count == 0;
                }
            }

            private void RemoveExpired(DateTime now)
            {
                while (_sends.Count > 0 && (now - _sends.Peek()).TotalSeconds >= RateWindowSeconds)
                    _sends.Dequeue();
            }
        }

        private struct RateDecision
        {
            public readonly bool Allowed;
            public readonly int RetryAfterSeconds;
            public RateDecision(bool allowed, int retryAfterSeconds) { Allowed = allowed; RetryAfterSeconds = retryAfterSeconds; }
        }

        public enum RequestStatus { Accepted, InvalidInput, RateLimited, MailFailed, SystemError }
        public enum VerifyStatus { Success, InvalidCode, Expired, TooManyAttempts, NotFound, AlreadyVerified }
        public enum ResendStatus { Success, Cooldown, RateLimited, MailFailed, Expired, NotFound, AlreadyVerified, InProgress }
        public enum ResetStatus { Success, InvalidPassword, NotVerified, SessionExpired, SameAsOldPassword, AccountUnavailable, UpdateFailed, NotFound, InProgress }
        public enum PasswordValidationStatus { Valid, Required, Length, Letter, Digit, Whitespace, ByteLength }

        public sealed class RequestResult
        {
            public RequestStatus Status { get; }
            public string ChallengeId { get; }
            public string MaskedEmail { get; }
            public int RetryAfterSeconds { get; }
            public RequestResult(RequestStatus status, string challengeId, string maskedEmail, int retryAfterSeconds)
            { Status = status; ChallengeId = challengeId; MaskedEmail = maskedEmail; RetryAfterSeconds = retryAfterSeconds; }
        }

        public sealed class VerifyResult
        {
            public VerifyStatus Status { get; }
            public int RemainingAttempts { get; }
            public VerifyResult(VerifyStatus status, int remainingAttempts)
            { Status = status; RemainingAttempts = remainingAttempts; }
        }

        public sealed class ResendResult
        {
            public ResendStatus Status { get; }
            public int RetryAfterSeconds { get; }
            public ResendResult(ResendStatus status, int retryAfterSeconds)
            { Status = status; RetryAfterSeconds = retryAfterSeconds; }
        }

        public sealed class ResetResult
        {
            public ResetStatus Status { get; }
            public PasswordValidationStatus ValidationStatus { get; }
            public ResetResult(ResetStatus status, PasswordValidationStatus validationStatus)
            { Status = status; ValidationStatus = validationStatus; }
        }
    }
}