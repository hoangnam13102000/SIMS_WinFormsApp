using System.Windows.Forms;
using SIMS_WinFormsApp.UI.I18n;

namespace SIMS_WinFormsApp.Views.Reports
{
    public partial class ucInventoryReport : UserControl
    {
        public ucInventoryReport()
        {
            InitializeComponent();
            ApplyLabels();
            LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
        }

        private void OnLanguageChanged(object sender, System.EventArgs e) => ApplyLabels();

        private void ApplyLabels()
        {
            _title.Text = Lang.Get("placeholder.reportInventory.title");
            _subtitle.Text = Lang.Get("placeholder.reportInventory.description");
        }
    }
}
