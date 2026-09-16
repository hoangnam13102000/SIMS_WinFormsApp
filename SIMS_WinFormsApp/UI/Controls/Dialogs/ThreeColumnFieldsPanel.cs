using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls
{
    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public class ThreeColumnFieldsPanel : Panel
    {
        private const int DefaultAvatarColumnWidth = 168;
        private const int ColumnGap = 28;

        private readonly Panel _secondColumn;
        private readonly Panel _thirdColumn;

        public AvatarUploadPanel Avatar { get; }

        public int AvatarColumnWidth { get; set; } = DefaultAvatarColumnWidth;

        public ThreeColumnFieldsPanel()
        {
            BackColor = Color.Transparent;
            Dock = DockStyle.Top;

            Avatar = new AvatarUploadPanel();
            _secondColumn = new Panel { BackColor = Color.Transparent };
            _thirdColumn = new Panel { BackColor = Color.Transparent, Visible = false };

            // Thứ tự Add không quan trọng ở đây vì 3 cột được định vị thủ công bằng Location/Width
            // trong Reflow() (không dùng Dock cho chính các cột), khác với quy ước "add ngược" áp
            // dụng cho các control con Dock=Top BÊN TRONG mỗi cột.
            Controls.Add(Avatar);
            Controls.Add(_secondColumn);
            Controls.Add(_thirdColumn);

            Resize += (s, e) => Reflow();
        }

        /// <summary>Gán các control cho cột thứ 2 (giữa), theo đúng thứ tự hiển thị mong muốn từ
        /// TRÊN XUỐNG (ví dụ: header, Họ tên, Email, SĐT...). Mỗi control cần tự Dock=Top như
        /// LabeledIconField/LabeledDateField/LabeledComboField/FieldGroupHeader đang có sẵn.</summary>
        public void SetSecondColumnFields(params Control[] fieldsTopToBottom) =>
            FillColumn(_secondColumn, fieldsTopToBottom);

        /// <summary>Gán các control cho cột thứ 3 (phải), cùng quy ước với
        /// <see cref="SetSecondColumnFields"/>. Truyền mảng rỗng/null nếu popup chỉ cần avatar +
        /// 1 cột trường - cột thứ 3 khi đó ẩn đi và cột giữa chiếm toàn bộ phần còn lại.</summary>
        public void SetThirdColumnFields(params Control[] fieldsTopToBottom) =>
            FillColumn(_thirdColumn, fieldsTopToBottom);

        private static void FillColumn(Panel column, Control[] fieldsTopToBottom)
        {
            column.Controls.Clear();
            column.Visible = fieldsTopToBottom != null && fieldsTopToBottom.Length > 0;
            if (fieldsTopToBottom == null) return;

            // Với các control con đều Dock=Top trong cùng 1 Panel, WinForms xếp control ADD SAU
            // CÙNG lên vị trí TRÊN CÙNG (đúng quy ước đang dùng xuyên suốt dự án ở
            // frmAddEmployee/frmEditUserAccount) - nên add theo thứ tự NGƯỢC LẠI với thứ tự hiển
            // thị mong muốn.
            for (int i = fieldsTopToBottom.Length - 1; i >= 0; i--)
            {
                if (fieldsTopToBottom[i] != null) column.Controls.Add(fieldsTopToBottom[i]);
            }
        }

        /// <summary>Tính lại vị trí/độ rộng 3 cột và co Height của cả hàng theo cột cao nhất.
        /// Form cha NÊN gọi lại hàm này ngay sau khi gán xong nội dung cột (không chỉ dựa vào sự
        /// kiện Resize), vì tại thời điểm khởi tạo control có thể chưa từng nhận Resize.</summary>
        public void Reflow()
        {
            int totalWidth = Width;

            // Bỏ qua các lần gọi khi Width rõ ràng CHƯA PHẢI kích thước cuối cùng (bé hơn cả 1
            // cột avatar + 1 khoảng cách cột) - đây là dấu hiệu control đang bị gọi Reflow() quá
            // sớm (trước khi Form cha có handle/ClientSize thật, xem BaseFormDialogForm.
            // OnContentReady). Nếu cứ layout với Width "rác" này, 2 cột nội dung sẽ bị ép xuống
            // mức tối thiểu 10px (xem LayoutColumn) và vỡ giao diện như popup Cập nhật tài khoản
            // từng gặp. Khi Width thật sự sẵn sàng, sự kiện Resize ở constructor sẽ tự gọi lại
            // Reflow() nên không cần lo bỏ sót lần layout hợp lệ.
            if (totalWidth < AvatarColumnWidth + ColumnGap) return;

            Avatar.Width = AvatarColumnWidth;
            Avatar.Location = new Point(0, 0);

            bool hasThirdColumn = _thirdColumn.Visible;
            int gapCount = hasThirdColumn ? 2 : 1;
            int fieldsWidth = Math.Max(0, totalWidth - AvatarColumnWidth - ColumnGap * gapCount);
            int columnWidth = hasThirdColumn ? fieldsWidth / 2 : fieldsWidth;

            _secondColumn.Location = new Point(AvatarColumnWidth + ColumnGap, 0);
            int secondHeight = LayoutColumn(_secondColumn, columnWidth);

            int thirdHeight = 0;
            if (hasThirdColumn)
            {
                _thirdColumn.Location = new Point(_secondColumn.Right + ColumnGap, 0);
                thirdHeight = LayoutColumn(_thirdColumn, Math.Max(10, totalWidth - _thirdColumn.Left));
            }

            int newHeight = Math.Max(Avatar.Height, Math.Max(secondHeight, thirdHeight));
            if (Height != newHeight) Height = newHeight;
        }

        private static int LayoutColumn(Panel column, int width)
        {
            column.Width = Math.Max(10, width);
            column.PerformLayout();

            int bottom = 0;
            foreach (Control child in column.Controls)
            {
                if (child.Visible) bottom = Math.Max(bottom, child.Bottom);
            }
            column.Height = bottom;
            return bottom;
        }
    }
}