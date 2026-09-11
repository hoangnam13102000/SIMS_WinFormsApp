using System;

namespace SIMS_WinFormsApp.UI.Controls.Pagination
{
    public interface IPaginationView
    {
        event EventHandler<int> PageRequested;

        event EventHandler<int> PageSizeRequested;


        void Render(PaginationState state);
    }

    public sealed class PaginationState
    {
        public int CurrentPage { get; }   // 1-based
        public int TotalPages { get; }
        public int TotalCount { get; }
        public int PageSize { get; }
        public int[] PageSizeOptions { get; }

        public PaginationState(int currentPage, int totalPages, int totalCount, int pageSize, int[] pageSizeOptions)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            TotalCount = totalCount;
            PageSize = pageSize;
            PageSizeOptions = pageSizeOptions;
        }
    }
}