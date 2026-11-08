using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Mapping;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.I18n;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Dialogs;

namespace SIMS_WinFormsApp.Views.SystemManagement
{
    /// <summary>Dialog "Xem" 1 dòng nhật ký - thuần hiển thị dữ liệu đã có sẵn, không có
    /// nghiệp vụ/validate nào nên không cần Presenter riêng (YAGNI).</summary>
    public sealed partial class frmAuditLogDetail : BaseFormDialogForm
    {
        public frmAuditLogDetail()
        {
            InitializeComponent();
            SetHeaderIcon(IconChar.Eye, AppColors.Accent);
        }

        private frmAuditLogDetail(IWin32Window owner, AuditLogDetailDto detail) : base(owner)
        {
            InitializeComponent();
            ContentHost.Controls.Clear();
            _closeButton.Text = Lang.Get("common.close");
            BuildContent(detail);

            CloseRequested += CloseRequestedHandler;
        }

        private void CloseRequestedHandler(object sender, EventArgs e) => Close();

        private void CloseButton_Click(object sender, EventArgs e) => RaiseCloseRequested();

        public static DialogResult Show(IWin32Window owner, AuditLogDetailDto detail)
        {
            using (var dialog = new frmAuditLogDetail(owner, detail))
            {
                return dialog.ShowDialog(owner);
            }
        }

        protected override void OnContentReady()
        {
            base.OnContentReady();
            ContentHost.PerformLayout();
            // Chiều cao phụ thuộc số lượng và độ dài các trường audit được nạp lúc chạy.
            FitHeightToContent();
        }

        private void BuildContent(AuditLogDetailDto detail)
        {
            HeaderTitle = Lang.Get("audit.detail.title", detail.LogId);
            SetHeaderIcon(IconChar.Eye, AppColors.Accent);

            ContentHost.Controls.Clear();
            _previewTimeLabel.Text = Lang.Get("audit.detail.time");
            _previewTimeValue.Text = detail.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss");
            _previewUserLabel.Text = Lang.Get("audit.detail.user");
            _previewUserValue.Text = detail.Username;
            _previewActionLabel.Text = Lang.Get("audit.detail.action");
            _previewActionValue.Text = AuditLogDisplayMapper.GetActionLabel(detail.Action);
            _previewTargetLabel.Text = Lang.Get("audit.detail.target");
            _previewTargetValue.Text = AuditLogDisplayMapper.GetTableLabel(detail.TableName)
                + (detail.RecordId.HasValue ? " #" + detail.RecordId.Value : "");
            _previewIpLabel.Text = Lang.Get("audit.detail.ip");
            _previewIpValue.Text = string.IsNullOrWhiteSpace(detail.IPAddress) ? "-" : detail.IPAddress;
            _previewDescriptionLabel.Text = Lang.Get("audit.detail.description");
            _previewDescriptionValue.Text = string.IsNullOrWhiteSpace(detail.Detail) ? "-" : detail.Detail;

            _detailDiffHeader.Text = Lang.Get("audit.detail.diff");

            bool hasDiff = !string.IsNullOrWhiteSpace(detail.OldValue) || !string.IsNullOrWhiteSpace(detail.NewValue);
            Control diffContent;
            if (hasDiff)
            {
                var diffGrid = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 1
                };
                diffGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                diffGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                diffGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

                diffGrid.Controls.Add(CreateDiffPanel(Lang.Get("audit.detail.before"), detail.OldValue, AppColors.ErrorBg, AppColors.Error), 0, 0);
                diffGrid.Controls.Add(CreateDiffPanel(Lang.Get("audit.detail.after"), detail.NewValue, AppColors.SuccessBg, AppColors.Success), 1, 0);
                diffContent = diffGrid;
            }
            else
            {
                diffContent = new Label
                {
                    Dock = DockStyle.Fill,
                    Text = Lang.Get("audit.detail.noDiff"),
                    Font = AppFonts.Body,
                    ForeColor = AppColors.TextMuted,
                    BackColor = Color.Transparent,
                    TextAlign = ContentAlignment.MiddleCenter
                };
            }

            _detailDiffHost.Controls.Clear();
            _detailDiffHost.Controls.Add(diffContent);

            ContentHost.Controls.Add(_detailDiffHost);
            ContentHost.Controls.Add(_detailDiffHeader);
            ContentHost.Controls.Add(_detailInfoGrid);
        }

        // Comparison panels are built from the audit record's runtime old/new values.
        private static Control CreateDiffPanel(string title, string value, Color badgeBg, Color badgeFg)
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 0, 8, 0) };

            var badge = new Label
            {
                Dock = DockStyle.Top,
                Height = AppFonts.SmallBold.Height + 10,
                Text = title,
                Font = AppFonts.SmallBold,
                ForeColor = badgeFg,
                BackColor = badgeBg,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 8)
            };

            var textBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextPrimary,
                BackColor = AppColors.BgLighter,
                BorderStyle = BorderStyle.FixedSingle,
                Text = string.IsNullOrWhiteSpace(value) ? Lang.Get("audit.detail.empty") : value
            };

            panel.Controls.Add(textBox);
            panel.Controls.Add(badge);
            return panel;
        }
    }
}