using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Views.Dashboard
{
    internal class BufferedPanel : Panel
    {
        public BufferedPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);
            UpdateStyles();
        }
    }

    public partial class ucDashboard : UserControl, IDashboardView
    {
        private readonly DashboardPresenter _presenter;

        public ucDashboard() : this(null)
        {
        }

        public ucDashboard(string displayName = null)
        {
            InitializeComponent();
            BackColor = AppColors.PageBg;
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                _presenter = null;
                return;
            }

            _presenter = new DashboardPresenter(this, displayName);
            _presenter.Load();
        }

        public void ShowDashboardUser(string displayName)
        {
            _welcomeLabel.Text = "Chào mừng bạn quay trở lại, " +
                (string.IsNullOrWhiteSpace(displayName) ? "bạn" : displayName);
        }
    }
}
