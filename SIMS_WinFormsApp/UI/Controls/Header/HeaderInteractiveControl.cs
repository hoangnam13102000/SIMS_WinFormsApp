using System;
using System.Drawing;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>
    /// Lớp cha cho các nút tương tác trên Header: nền trong suốt, trạng thái hover ổn định
    /// (kể cả khi con trỏ đi qua control con) và chuyển tiếp Click của control con lên chính nó.
    /// Lớp con chỉ cần vẽ hình hover trong OnPaint dựa vào <see cref="IsHover"/>.
    /// </summary>
    public abstract class HeaderInteractiveControl : Control
    {
        private bool _hover;

        protected HeaderInteractiveControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Selectable, false);

            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
        }

        protected bool IsHover => _hover;

        /// <summary>Đăng ký control con để Click/hover của nó được tính cho control này.</summary>
        protected void WireChild(Control child)
        {
            if (child == null) return;
            child.Click += (_, __) => OnClick(EventArgs.Empty);
            child.MouseEnter += (_, __) => UpdateHover();
            child.MouseLeave += (_, __) => UpdateHover();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            UpdateHover();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            UpdateHover();
        }

        private void UpdateHover()
        {
            if (IsDisposed || !IsHandleCreated) return;

            bool inside = ClientRectangle.Contains(PointToClient(Cursor.Position));
            if (inside == _hover) return;

            _hover = inside;
            Invalidate(true);
        }
    }
}