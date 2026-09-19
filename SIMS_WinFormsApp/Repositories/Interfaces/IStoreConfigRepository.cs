using SIMS_WinFormsApp.Models.DTOs;

namespace SIMS_WinFormsApp.Repositories.Interfaces
{
    public interface IStoreConfigRepository
    {
        StoreSettingsDto GetSettings();
        void SaveSettings(StoreSettingsDto settings);
    }
}