using System;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.MVP.Views;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.Layouts;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    public partial class frmMain : Form, IMainView
    {
        private MainLayoutControl _mainLayout;
        private MainPresenter _presenter;

        public event EventHandler ViewReady;
        public event EventHandler LogoutRequested;
        public event EventHandler ProfileRequested;
        public event EventHandler<string> PageChanged;

        public frmMain()
        {
            InitializeComponent();

            AutoScaleMode = AutoScaleMode.None;
            Font = new Font("Segoe UI", 9f);
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(1024, 680);
            BackColor = Color.FromArgb(245, 247, 250);

            // KHÔNG set WindowState = Maximized ở đây (constructor).
            // Nguyên nhân gốc: khi set WindowState.Maximized TRƯỚC khi Form handle
            // được tạo (trước Show/Application.Run), WinForms lưu "restore bounds"
            // = Size/Location hiện tại (từ Designer ClientSize 1280x720 + Location mặc định).
            // Kết quả: title bar hiển thị icon Restore (coi như Maximized) nhưng
            // Bounds thực tế vẫn là kích thước nhỏ nằm góc trên-trái.
            // Giải pháp: chỉ set WindowState SAU khi handle đã tạo (trong Load).

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
            UpdateStyles();

            // ===== QUAN TRỌNG: Tạo Presenter =====
            _presenter = new MainPresenter(
                this,
                () => UserSession.Instance.CurrentUser?.FullName ?? UserSession.Instance.CurrentUser?.Username ?? "Admin",
                () => UserSession.Instance.CurrentUser?.Email ?? string.Empty);

            Load += OnFormLoad;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            // Set Maximized SAU khi handle đã được tạo → Windows tính đúng
            // WorkingArea của màn hình, Bounds thực tế = full screen, title bar
            // phản ánh đúng trạng thái Maximized. Restore sẽ trả về ClientSize
            // từ Designer (1280x720) tại vị trí CenterScreen.
            if (WindowState != FormWindowState.Maximized)
                WindowState = FormWindowState.Maximized;

            ViewReady?.Invoke(this, EventArgs.Empty);
        }

        public void AttachLayout(Control layout)
        {
            if (layout == null) return;

            Controls.Clear();
            layout.Dock = DockStyle.Fill;
            Controls.Add(layout);
            layout.BringToFront();

            if (layout is MainLayoutControl mlc)
            {
                _mainLayout = mlc;
                _mainLayout.LogoutRequested += (_, __) => LogoutRequested?.Invoke(this, EventArgs.Empty);
                _mainLayout.ProfileRequested += (_, __) => ProfileRequested?.Invoke(this, EventArgs.Empty);
                _mainLayout.PageChanged += (_, key) => PageChanged?.Invoke(this, key);
            }

            PerformLayout();
            Invalidate(true);
        }

        public void SetWindowTitle(string title)
        {
            if (!string.IsNullOrEmpty(title)) Text = title;
        }

        public void SetUserInfo(string displayName, string email, string avatarInitial = null)
        {
            _mainLayout?.SetUser(displayName, email, avatarInitial);
        }

        public void SetUnreadNotifications(int count)
        {
            _mainLayout?.SetUnreadCount(count);
        }

        public void SetSidebarBadge(string pageKey, int count)
        {
            _mainLayout?.SetBadge(pageKey, count);
        }

        public void NavigateTo(string pageKey)
        {
            _mainLayout?.ShowPage(pageKey);
        }

        public void ShowMessage(string message, string caption, MessageBoxIcon icon)
        {
            MessageBox.Show(this, message, caption, MessageBoxButtons.OK, icon);
        }

        public bool Confirm(string message, string caption)
        {
            return MessageBox.Show(this, message, caption,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public bool ConfirmLogout(string message, string caption, string confirmText, string cancelText)
        {
            return MessageBox.Show(this, message, caption,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        public void CloseView()
        {
            Close();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _mainLayout?.PerformLayout();
        }
    }
}