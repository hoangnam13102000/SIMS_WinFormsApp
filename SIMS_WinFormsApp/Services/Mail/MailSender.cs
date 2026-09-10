using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using SIMS_WinFormsApp.Infrastructure;

namespace SIMS_WinFormsApp.Services.Mail
{
 
    public class MailSender
    {
        public const string KeySenderAddress = "MAIL_SENDER_ADDRESS";
        public const string KeySenderAppPassword = "MAIL_SENDER_APP_PASSWORD";

        public static bool IsConfigured =>
            SecureConfigStore.Has(KeySenderAddress) && SecureConfigStore.Has(KeySenderAppPassword);

        public void Send(string toEmail, string subject, string bodyText)
        {
            string senderAddress = SecureConfigStore.Get(KeySenderAddress);
            string senderPassword = SecureConfigStore.Get(KeySenderAppPassword);

            if (string.IsNullOrWhiteSpace(senderAddress) || string.IsNullOrWhiteSpace(senderPassword))
            {
                throw new MailFailedException(
                    "Chưa cấu hình email gửi OTP. Vào Cài đặt > Cấu hình Email để thiết lập Gmail + App Password.");
            }

            try
            {
                using (var client = new SmtpClient("smtp.gmail.com", 587))
                {
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.Credentials = new NetworkCredential(senderAddress, senderPassword);

                    using (var message = new MailMessage(senderAddress, toEmail, subject, bodyText))
                    {
                        message.BodyEncoding = Encoding.UTF8;
                        message.SubjectEncoding = Encoding.UTF8;
                        message.IsBodyHtml = false;
                        client.Send(message);
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw new MailFailedException("Gửi email thất bại: " + ex.Message, ex);
            }
        }
    }

    public class MailFailedException : Exception
    {
        public MailFailedException(string message) : base(message) { }
        public MailFailedException(string message, Exception inner) : base(message, inner) { }
    }
}