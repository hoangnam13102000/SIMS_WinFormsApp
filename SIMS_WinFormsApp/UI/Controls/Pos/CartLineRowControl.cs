using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Pos;
using SIMS_WinFormsApp.MVP.Presenters.Pos;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Pos
{
    /// <summary>1 dòng trong danh sách giỏ hàng POS: tên + đơn giá, nút +/- số lượng, thành
    /// tiền dòng, nút xóa. Chỉ phát sự kiện - PosPresenter quyết định thay đổi giỏ hàng.</summary>
    internal sealed class CartLineRowControl : UserControl
    {
        public int ProductId { get; }

        /// <summary>Số lượng mới sau khi bấm +/- (đã tính sẵn, còn >= 0; 0 nghĩa là nên xóa dòng).</summary>
        public event EventHandler<int> QuantityChanged;
        public event EventHandler RemoveClicked;

        private readonly Label _qtyLabel;
        private int _quantity;

        public CartLineRowControl(CartLineDto line)
        {
            if (line == null) throw new ArgumentNullException(nameof(line));
            ProductId = line.ProductId;
            _quantity = line.Quantity;

            Dock = DockStyle.Top;
            Height = 76;
            Margin = new Padding(0, 0, 0, 8);
            BackColor = Color.Transparent;
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);

            var nameLabel = new Label
            {
                Text = line.ProductName,
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                Location = new Point(12, 6),
                Size = new Size(Width - 104, 28),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            };
            Controls.Add(nameLabel);

            var unitLabel = new Label
            {
                Text = PosFormat.Vnd(line.UnitPrice) + " / sp",
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                Location = new Point(12, 28),
                Size = new Size(Width - 104, 26),
                TextAlign = ContentAlignment.MiddleLeft
            };
            Controls.Add(unitLabel);

            var removeIcon = new IconPictureBox
            {
                IconChar = IconChar.TrashCan,
                IconColor = AppColors.TextMuted,
                IconSize = 15,
                Size = new Size(24, 24),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent
            };
            removeIcon.Click += (s, e) => RemoveClicked?.Invoke(this, EventArgs.Empty);
            Controls.Add(removeIcon);

            var plusButton = CreateStepButton(IconChar.Plus);
            plusButton.Click += (s, e) => ChangeQuantity(+1);

            var minusButton = CreateStepButton(IconChar.Minus);
            minusButton.Click += (s, e) => ChangeQuantity(-1);

            _qtyLabel = new Label
            {
                Text = _quantity.ToString(),
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(28, 24),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            Controls.Add(plusButton);
            Controls.Add(_qtyLabel);
            Controls.Add(minusButton);

            PositionStepper(minusButton, _qtyLabel, plusButton, removeIcon);
            Resize += (s, e) =>
            {
                nameLabel.Width = Math.Max(100, Width - 104);
                unitLabel.Width = Math.Max(100, Width - 104);
                PositionStepper(minusButton, _qtyLabel, plusButton, removeIcon);
            };
        }

        private static IconPictureBox CreateStepButton(IconChar icon)
        {
            return new IconPictureBox
            {
                IconChar = icon,
                IconColor = AppColors.TextPrimary,
                IconSize = 12,
                Size = new Size(22, 22),
                Cursor = Cursors.Hand,
                BackColor = AppColors.BgLighter,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
        }

        private void PositionStepper(Control minus, Control qty, Control plus, Control remove)
        {
            remove.Location = new Point(Width - 12 - remove.Width, 8);
            plus.Location = new Point(Width - 12 - plus.Width, 46);
            qty.Location = new Point(plus.Left - qty.Width, 44);
            minus.Location = new Point(qty.Left - minus.Width, 46);
        }

        private void ChangeQuantity(int delta)
        {
            int next = _quantity + delta;
            _quantity = Math.Max(0, next);
            _qtyLabel.Text = _quantity.ToString();
            QuantityChanged?.Invoke(this, _quantity);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Medium))
            using (var brush = new SolidBrush(AppColors.BgLighter))
                e.Graphics.FillPath(brush, path);
        }
    }
}