using System;
using System.Windows.Forms;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.UI.Controls.Loading;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.MVP.Presenters
{
    public sealed class AuditLogPresenter
    {
        private readonly IAuditLogView _view;
        private readonly IAuditLogRepository _repository;
        private readonly ILoadingIndicator _loadingIndicator;

        public AuditLogPresenter(
            IAuditLogView view,
            IAuditLogRepository repository,
            ILoadingIndicator loadingIndicator = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _loadingIndicator = loadingIndicator;

            _view.QueryChanged += (s, e) => LoadPage();
            _view.DetailRequested += (s, logId) => LoadDetail(logId);
        }

        public void Load()
        {
            _loadingIndicator?.ShowLoading("Đang tải nhật ký hệ thống...");
            Application.DoEvents();
            try
            {
                _view.DisplayActionOptions(_repository.GetDistinctActions());
                _view.DisplayTableOptions(_repository.GetDistinctTables());
                _view.DisplayStats(_repository.GetStats());
                LoadPageCore();
            }
            catch (Exception ex)
            {
                _view.ShowError("Không thể tải nhật ký hệ thống: " + ex.Message);
            }
            finally
            {
                _loadingIndicator?.HideLoading();
            }
        }

        private void LoadPage()
        {
            _loadingIndicator?.ShowLoading("Đang tải nhật ký...");
            Application.DoEvents();
            try
            {
                LoadPageCore();
            }
            catch (Exception ex)
            {
                _view.ShowError("Không thể tải nhật ký: " + ex.Message);
            }
            finally
            {
                _loadingIndicator?.HideLoading();
            }
        }

        private void LoadPageCore()
        {
            var result = _repository.GetPage(_view.CurrentQuery);
            _view.DisplayRows(result.Rows, result.TotalCount);
        }

        private void LoadDetail(long logId)
        {
            try
            {
                var detail = _repository.GetById(logId);
                if (detail != null) _view.ShowDetail(detail);
                else _view.ShowError("Không tìm thấy nhật ký này (có thể đã bị xóa).");
            }
            catch (Exception ex)
            {
                _view.ShowError("Không thể tải chi tiết nhật ký: " + ex.Message);
            }
        }
    }
}