namespace SIMS_WinFormsApp.Models.DTOs
{
    public sealed class StoreSettingsDto
    {
        public string StoreName { get; set; }
        public string DefaultUnit { get; set; }
        public decimal VatRate { get; set; }
        public decimal DefaultMargin { get; set; }
        public int ReturnPolicyDays { get; set; }
        public decimal ApprovalThreshold { get; set; }
    }
}