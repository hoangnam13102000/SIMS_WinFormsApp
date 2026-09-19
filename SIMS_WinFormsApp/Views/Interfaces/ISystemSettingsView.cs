using System;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface ISystemSettingsView
    {
        string StoreName { get; set; }
        string DefaultUnit { get; set; }
        string VatRateText { get; set; }
        string DefaultMarginText { get; set; }
        string ReturnPolicyDaysText { get; set; }
        string ApprovalThresholdText { get; set; }

        event EventHandler SaveRequested;

        void SetSaving(bool isSaving);
        void ShowError(string message);
        void ShowSuccess(string message);
    }
}