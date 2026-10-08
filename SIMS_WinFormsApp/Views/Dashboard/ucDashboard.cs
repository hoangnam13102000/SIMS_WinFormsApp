using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Views.Dashboard
{
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

        private void _titleLabel_Click(object sender, System.EventArgs e)
        {

        }

        private void _activityTitle_Click(object sender, System.EventArgs e)
        {

        }
    }
}
