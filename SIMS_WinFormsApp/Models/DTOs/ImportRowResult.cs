namespace SIMS_WinFormsApp.Models.DTOs
{
    public sealed class ImportRowResult
    {
        private static readonly ImportRowResult SuccessResult = new ImportRowResult(true, null);

        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        private ImportRowResult(bool isSuccess, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static ImportRowResult Success() => SuccessResult;
        public static ImportRowResult Failure(string message) => new ImportRowResult(false, message);
    }
}