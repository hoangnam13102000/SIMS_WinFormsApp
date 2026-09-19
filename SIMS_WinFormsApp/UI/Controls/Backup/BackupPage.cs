using System.Windows.Forms;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.MVP.Presenters;

namespace SIMS_WinFormsApp.UI.Controls.Backup
{
    public static class BackupPage
    {
        public static Control Create()
        {
            var view = new BackupRecoveryPageControl();

            var backupManager = AppComposition.CreateBackupManager();
            var cloudUploadListener = AppComposition.CreateCloudinaryBackupUploadListener();
            if (cloudUploadListener != null) backupManager.AddListener(cloudUploadListener);

            var presenter = new BackupRecoveryPresenter(view, backupManager, cloudUploadListener);
            view.Disposed += (s, e) => presenter.Dispose();

            return view;
        }
    }
}