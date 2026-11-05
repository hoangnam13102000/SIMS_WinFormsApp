using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.UI.I18n;

namespace SIMS_WinFormsApp.Views.Sales
{
    public partial class ucOrderManagement : UserControl
    {
        public ucOrderManagement()
        {
            InitializeComponent();
            DataGridViewCodeCopy.Attach(_grid, "Code");
            ApplyLabels();
            LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
        }

        private void OnLanguageChanged(object sender, System.EventArgs e) => ApplyLabels();

        private void ApplyLabels()
        {
            _title.Text = Lang.Get("placeholder.orders.title");
            _subtitle.Text = Lang.Get("placeholder.orders.description");
        }
    }
}
