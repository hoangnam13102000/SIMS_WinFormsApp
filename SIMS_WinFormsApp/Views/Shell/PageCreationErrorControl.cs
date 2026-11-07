using System;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.Views.Shell
{
    public partial class PageCreationErrorControl : UserControl
    {
        public event EventHandler RetryRequested;

        public PageCreationErrorControl()
        {
            InitializeComponent();
        }

        public void SetException(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            _errorLabel.Text = "Không thể mở trang này.\r\n" + exception.Message;
            _errorDetails.Text = exception.ToString();
        }

        private void RetryButton_Click(object sender, EventArgs e)
        {
            RetryRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
