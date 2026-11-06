using System;
using SIMS_WinFormsApp.Models.DTOs.Promotions;

namespace SIMS_WinFormsApp.Views.Interfaces
{
    public interface IPromotionManagementView
    {
        event EventHandler ReloadRequested;
        event EventHandler NewPromotionRequested;
        event EventHandler ViewPromotionRequested;
        event EventHandler EditPromotionRequested;
        event EventHandler ToggleActiveRequested;
        event EventHandler DeletePromotionRequested;
        event EventHandler CancelEditorRequested;
        event EventHandler<PromotionSaveRequestedEventArgs> SaveRequested;

        string Keyword { get; }
        bool IncludeDeleted { get; }
        int PageIndex { get; }
        int PageSize { get; }
        int SelectedPromotionId { get; }

        bool ShowItems(PromotionPage page);
        void ShowDetails(PromotionDto promotion);
        void ShowEditor(PromotionDto promotion);
        void CloseEditor();
        bool ConfirmDelete();
        void ClearSelection();
        void ShowInfo(string message);
        void ShowError(string message);
    }

    public sealed class PromotionEditorInput
    {
        public int PromotionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DiscountType { get; set; }
        public string DiscountValue { get; set; }
        public string MaximumDiscount { get; set; }
        public string MinimumOrder { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UsageLimit { get; set; }
        public bool IsActive { get; set; }
        public bool ShowOnBanner { get; set; }
    }

    public sealed class PromotionSaveRequestedEventArgs : EventArgs
    {
        public PromotionSaveRequestedEventArgs(PromotionEditorInput input)
        {
            Input = input ?? throw new ArgumentNullException(nameof(input));
        }

        public PromotionEditorInput Input { get; }
    }
}
