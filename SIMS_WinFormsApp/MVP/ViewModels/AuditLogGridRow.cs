using System;
using System.Drawing;

namespace SIMS_WinFormsApp.MVP.ViewModels
{
    /// <summary>
    /// Dòng đã được chuẩn bị để vẽ. View không đưa mã hành động thô vào lưới —
    /// nhãn và màu đã được map trước khi tới painter.
    /// </summary>
    public sealed class AuditLogGridRow
    {
        public long LogId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string ActionLabel { get; set; }
        public string TableLabel { get; set; }
        public string DetailText { get; set; }
        public Color ActionBackground { get; set; }
        public Color ActionForeground { get; set; }
        public Color TableBackground { get; set; }
        public Color TableForeground { get; set; }
    }
}
