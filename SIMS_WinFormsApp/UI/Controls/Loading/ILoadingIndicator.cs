namespace SIMS_WinFormsApp.UI.Controls.Loading
{
    public interface ILoadingIndicator
    {
        /// <summary>Hiện lớp phủ đang tải.</summary>
        /// <param name="message">Thông điệp hiển thị kèm spinner; null/rỗng dùng thông điệp mặc định.</param>
        void ShowLoading(string message = null);

        /// <summary>Ẩn lớp phủ đang tải.</summary>
        void HideLoading();
    }
}