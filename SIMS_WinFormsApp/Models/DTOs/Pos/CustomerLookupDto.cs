namespace SIMS_WinFormsApp.Models.DTOs.Pos
{
    public sealed class CustomerLookupDto
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public int LoyaltyPoints { get; set; }

        public string DisplayText => string.IsNullOrEmpty(Phone) ? FullName : FullName + " - " + Phone;
    }
}