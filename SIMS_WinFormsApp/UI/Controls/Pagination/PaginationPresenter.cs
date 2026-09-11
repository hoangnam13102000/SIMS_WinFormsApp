using System;

namespace SIMS_WinFormsApp.UI.Controls.Pagination
{
    public sealed class PaginationPresenter
    {
        private static readonly int[] DefaultPageSizeOptions = { 10, 20, 50, 100 };

        private readonly IPaginationView _view;
        private readonly int[] _pageSizeOptions;
        private int _pageIndex;   
        private int _pageSize;
        private int _totalCount;

        public event EventHandler PageChanged;

        public int PageIndex => _pageIndex;   
        public int PageSize => _pageSize;
        public int PageCount => Math.Max(1, (int)Math.Ceiling(_totalCount / (double)_pageSize));

        public PaginationPresenter(IPaginationView view, int pageSize, int[] pageSizeOptions = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _pageSize = Math.Max(1, pageSize);
            _pageSizeOptions = NormalizeOptions(pageSizeOptions, _pageSize);

            _view.PageRequested += (_, page) => GoToPage(page);
            _view.PageSizeRequested += (_, size) => ChangePageSize(size);
        }

        private static int[] NormalizeOptions(int[] options, int currentPageSize)
        {
            if (options == null || options.Length == 0) options = DefaultPageSizeOptions;
            if (Array.IndexOf(options, currentPageSize) < 0)
            {
                Array.Resize(ref options, options.Length + 1);
                options[options.Length - 1] = currentPageSize;
                Array.Sort(options);
            }
            return options;
        }

        public void ResetToFirstPage()
        {
            _pageIndex = 0;
            PageChanged?.Invoke(this, EventArgs.Empty);
        }

        private void GoToPage(int page1Based)
        {
            int target = Math.Max(0, page1Based - 1);
            if (target == _pageIndex) return;
            _pageIndex = target;
            PageChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ChangePageSize(int newPageSize)
        {
            if (newPageSize <= 0 || newPageSize == _pageSize) return;
            _pageSize = newPageSize;
            _pageIndex = 0;
            PageChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ApplyResult(int totalCount)
        {
            _totalCount = Math.Max(0, totalCount);
            _pageIndex = Math.Min(_pageIndex, PageCount - 1);
            if (_pageIndex < 0) _pageIndex = 0;

            _view.Render(new PaginationState(_pageIndex + 1, PageCount, _totalCount, _pageSize, _pageSizeOptions));
        }
    }
}