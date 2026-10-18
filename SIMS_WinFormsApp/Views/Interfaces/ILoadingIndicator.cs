namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface ILoadingIndicator
    {
        void ShowLoading(string message = null);
        void HideLoading();
    }
}
