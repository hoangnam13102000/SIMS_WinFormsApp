using System.Threading;
using System.Threading.Tasks;

namespace SIMS_WinFormsApp.Services.Backup
{

    public interface ICloudinaryUploader
    {
        Task<string> UploadBackupFileAsync(string filePath, CancellationToken cancellationToken = default(CancellationToken));
    }
}