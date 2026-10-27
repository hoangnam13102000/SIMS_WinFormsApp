using System.Windows.Forms;
using SIMS_WinFormsApp.UI.I18n;

namespace SIMS_WinFormsApp.Views.Sales
{
    public partial class ucReturnManagement : UserControl
    {
        public ucReturnManagement()
        {
            InitializeComponent();
            ApplyLabels();
            LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
        }

        private void OnLanguageChanged(object sender, System.EventArgs e) => ApplyLabels();

        private void ApplyLabels()
        {
            _title.Text = Lang.Get("placeholder.returns.title");
            _subtitle.Text = Lang.Get("placeholder.returns.description");
        }
    }
}
