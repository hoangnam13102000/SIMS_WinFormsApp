using System;
using System.Windows.Forms;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.UI.Controls.Loading;
using SIMS_WinFormsApp.UI.I18n;
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
            _loadingIndicator?.ShowLoading(Lang.Get("audit.loading.page"));
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
                _view.ShowError(Lang.Get("audit.error.load", ex.Message));
            }
            finally
            {
                _loadingIndicator?.HideLoading();
            }
        }

        private void LoadPage()
        {
            _loadingIndicator?.ShowLoading(Lang.Get("audit.loading.list"));
            Application.DoEvents();
            try
            {
                LoadPageCore();
            }
            catch (Exception ex)
            {
                _view.ShowError(Lang.Get("audit.error.page", ex.Message));
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
                else _view.ShowError(Lang.Get("audit.error.missing"));
            }
            catch (Exception ex)
            {
                _view.ShowError(Lang.Get("audit.error.detail", ex.Message));
            }
        }
    }
}