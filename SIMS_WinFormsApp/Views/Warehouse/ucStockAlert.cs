using System.Windows.Forms;
using SIMS_WinFormsApp.UI.I18n;

namespace SIMS_WinFormsApp.Views.Warehouse
{
    public partial class ucStockAlert : UserControl
    {
        public ucStockAlert()
        {
            InitializeComponent();
            ApplyLabels();
            LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
        }

        private void OnLanguageChanged(object sender, System.EventArgs e) => ApplyLabels();

        private void ApplyLabels()
        {
            _title.Text = Lang.Get("placeholder.stockAlert.title");
            _subtitle.Text = Lang.Get("placeholder.stockAlert.description");
        }
    }
}
