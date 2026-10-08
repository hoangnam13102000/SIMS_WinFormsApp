using System;
using System.ComponentModel;
using System.Windows.Forms;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.UI.Controls.Toast;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Views.SystemManagement
{
    public sealed partial class ucSystemSettings : UserControl, ISystemSettingsView
    {
        private readonly IStoreConfigRepository _repository;
        private readonly SystemSettingsPresenter _presenter;
        private bool _dataLoadedOnce;

        public ucSystemSettings() : this(null)
        {
        }

        public ucSystemSettings(IStoreConfigRepository repository)
        {
            InitializeComponent();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;

            _repository = repository ?? AppComposition.CreateStoreConfigRepository();
            _presenter = new SystemSettingsPresenter(this, _repository);
            _saveButton.Click += (sender, args) => SaveRequested?.Invoke(this, EventArgs.Empty);
            VisibleChanged += OnVisibleChanged;
        }

        public string StoreName
        {
            get => _storeName.Text;
            set => _storeName.Text = value ?? string.Empty;
        }

        public string DefaultUnit
        {
            get => _defaultUnit.Text;
            set => _defaultUnit.Text = value ?? string.Empty;
        }

        public string VatRateText
        {
            get => _vatRate.Text;
            set => _vatRate.Text = value ?? string.Empty;
        }

        public string DefaultMarginText
        {
            get => _defaultMargin.Text;
            set => _defaultMargin.Text = value ?? string.Empty;
        }

        public string ReturnPolicyDaysText
        {
            get => _returnDays.Text;
            set => _returnDays.Text = value ?? string.Empty;
        }

        public string ApprovalThresholdText
        {
            get => _approvalThreshold.Text;
            set => _approvalThreshold.Text = value ?? string.Empty;
        }

        public event EventHandler SaveRequested;

        public void SetSaving(bool isSaving)
        {
            _storeName.Enabled = !isSaving;
            _defaultUnit.Enabled = !isSaving;
            _vatRate.Enabled = !isSaving;
            _defaultMargin.Enabled = !isSaving;
            _returnDays.Enabled = !isSaving;
            _approvalThreshold.Enabled = !isSaving;
            _saveButton.Enabled = !isSaving;
            _saveButton.Text = isSaving ? "Đang lưu..." : "Lưu thay đổi";
        }

        public void ShowError(string message) => AppToast.Error(this, message);

        public void ShowSuccess(string message) => AppToast.Success(this, message);

        private void OnVisibleChanged(object sender, EventArgs e)
        {
            if (Visible && !_dataLoadedOnce)
            {
                _dataLoadedOnce = true;
                _presenter.Load();
            }
        }
    }
}
