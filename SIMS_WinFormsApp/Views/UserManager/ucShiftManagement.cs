using System.Windows.Forms;
using SIMS_WinFormsApp.UI.I18n;

namespace SIMS_WinFormsApp.Views.UserManager
{
    public partial class ucShiftManagement : UserControl
    {
        public ucShiftManagement()
        {
            InitializeComponent();
            ApplyLabels();
            LanguageManager.Instance.LanguageChanged += OnLanguageChanged;
        }

        private void OnLanguageChanged(object sender, System.EventArgs e) => ApplyLabels();

        private void ApplyLabels()
        {
            _title.Text = Lang.Get("placeholder.shifts.title");
            _subtitle.Text = Lang.Get("placeholder.shifts.description");
        }
    }
}
