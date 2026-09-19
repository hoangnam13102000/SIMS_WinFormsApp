using System;
using System.Collections.Generic;

namespace SIMS_WinFormsApp.Models.DTOs
{
    /// <summary>1 dòng trong bảng "Nhật ký hệ thống".</summary>
    public sealed class AuditLogRowDto
    {
        public long LogId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public string TableName { get; set; }
        public string Detail { get; set; }

        /// <summary>true = xếp vào tab "Nhật ký sự cố" (đăng nhập thất bại, lỗi hệ thống...).</summary>
        public bool IsIncident { get; set; }
    }

    /// <summary>Chi tiết đầy đủ 1 bản ghi nhật ký - dùng cho dialog xem diff (OldValue/NewValue).</summary>
    public sealed class AuditLogDetailDto
    {
        public long LogId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string Action { get; set; }
        public string TableName { get; set; }
        public int? RecordId { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Detail { get; set; }
        public string IPAddress { get; set; }
    }

    /// <summary>4 số liệu ở đầu trang: Tổng nhật ký / Hoạt động hôm nay / Đăng nhập thất bại /
    /// Người dùng hoạt động (số user khác nhau có phát sinh nhật ký).</summary>
    public sealed class AuditLogStatsDto
    {
        public int TotalCount { get; set; }
        public int TodayCount { get; set; }
        public int FailedLoginCount { get; set; }
        public int ActiveUserCount { get; set; }
    }

    /// <summary>Tham số truy vấn 1 trang nhật ký - gộp lại thay vì nhiều tham số rời rạc để dễ mở
    /// rộng thêm điều kiện lọc sau này mà không phải đổi chữ ký GetPage().</summary>
    public sealed class AuditLogQuery
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; } = 10;
        public string Search { get; set; }
        public string ActionFilter { get; set; }
        public string TableFilter { get; set; }
        public bool IncidentOnly { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public sealed class AuditLogPageResult
    {
        public int TotalCount { get; set; }
        public IReadOnlyList<AuditLogRowDto> Rows { get; set; } = Array.Empty<AuditLogRowDto>();
    }
}