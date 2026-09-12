using System;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Views.Interfaces;
using SIMS_WinFormsApp.Services.Session;
using SIMS_WinFormsApp.UI.Controls;
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

            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
            UpdateStyles();

            _presenter = new MainPresenter(
                this,
                () => UserSession.Instance.CurrentUser?.FullName ?? UserSession.Instance.CurrentUser?.Username ?? "Admin",
                () => UserSession.Instance.CurrentUser?.Email ?? string.Empty);

            Load += OnFormLoad;
            FormClosed += OnFormClosed;
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {

            FormClosed -= OnFormClosed;
            Load -= OnFormLoad;
            _presenter?.Dispose();
            _presenter = null;
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
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
            DialogType dialogType = MapMessageBoxIconToDialogType(icon);
            DialogHelper.ShowCustom(this, caption, message, dialogType, DialogButtons.OK, DialogResult.OK);
        }

        public bool Confirm(string message, string caption)
        {
            return DialogHelper.Confirm(this, caption, message);
        }

        public bool ConfirmLogout(string message, string caption, string confirmText, string cancelText)
        {
            using (var dialog = new BaseDialog()
            {
                Title = caption,
                Message = message,
                IconType = DialogType.Danger,
                Buttons = DialogButtons.YesNo,
                DefaultButton = DialogResult.No,
                StartPosition = FormStartPosition.CenterScreen
            })
            {
                
                return dialog.ShowDialog(this) == DialogResult.Yes;
            }
        }

        public void CloseView()
        {
            if (IsDisposed) return;
            Close();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _mainLayout?.PerformLayout();
        }

        #region Helper - Map MessageBoxIcon → DialogType
        private static DialogType MapMessageBoxIconToDialogType(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Information: return DialogType.Info;
                case MessageBoxIcon.Warning: return DialogType.Warning;
                case MessageBoxIcon.Error: return DialogType.Error;
                case MessageBoxIcon.Question: return DialogType.Question;
                default: return DialogType.Info;
            }
        }
        #endregion
    }
}