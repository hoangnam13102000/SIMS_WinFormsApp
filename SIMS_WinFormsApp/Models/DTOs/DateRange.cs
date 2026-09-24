using System;

namespace SIMS_WinFormsApp.Models.DTOs
{
    /// <summary>
    /// Khoảng ngày đã chuẩn hóa về 00:00 (không giờ). Dùng cho bộ lọc nhật ký —
    /// View không tự suy luận From/To, Presenter là nơi tạo và so sánh giá trị này.
    /// </summary>
    public sealed class DateRange : IEquatable<DateRange>
    {
        public static readonly DateRange Empty = new DateRange(null, null);

        public DateTime? From { get; }
        public DateTime? To { get; }

        public bool IsEmpty => !From.HasValue && !To.HasValue;

        public DateRange(DateTime? from, DateTime? to)
        {
            From = from?.Date;
            To = to?.Date;
        }

        public bool Equals(DateRange other)
        {
            if (ReferenceEquals(other, null)) return false;
            return Nullable.Equals(From, other.From) && Nullable.Equals(To, other.To);
        }

        public override bool Equals(object obj) => Equals(obj as DateRange);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + (From.HasValue ? From.Value.GetHashCode() : 0);
                hash = hash * 31 + (To.HasValue ? To.Value.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
