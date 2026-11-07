using System;
using System.ComponentModel;
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
        private static readonly Size DialogSize = new Size(760, 620);

        public frmAuditLogDetail()
        {
            InitializeComponent();
            HeaderTitle = "Chi tiết nhật ký";
            SetHeaderIcon(IconChar.Eye, AppColors.Accent);
            BuildDesignPreviewContent();
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }
        }

        private frmAuditLogDetail(IWin32Window owner, AuditLogDetailDto detail) : base(owner)
        {
            InitializeComponent();
            ContentHost.Controls.Clear();
            Size = DialogSize;
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
            FitHeightToContent();
        }

        private void BuildContent(AuditLogDetailDto detail)
        {
            HeaderTitle = Lang.Get("audit.detail.title", detail.LogId);
            SetHeaderIcon(IconChar.Eye, AppColors.Accent);

            ContentHost.Controls.Clear();
            _detailInfoGrid.Controls.Clear();
            _detailInfoGrid.RowCount = 0;
            _detailInfoGrid.RowStyles.Clear();

            AddInfoField(_detailInfoGrid, Lang.Get("audit.detail.time"), detail.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"));
            AddInfoField(_detailInfoGrid, Lang.Get("audit.detail.user"), detail.Username);
            AddInfoField(_detailInfoGrid, Lang.Get("audit.detail.action"), AuditLogDisplayMapper.GetActionLabel(detail.Action));
            AddInfoField(_detailInfoGrid, Lang.Get("audit.detail.target"), AuditLogDisplayMapper.GetTableLabel(detail.TableName)
                + (detail.RecordId.HasValue ? " #" + detail.RecordId.Value : ""));
            AddInfoField(_detailInfoGrid, Lang.Get("audit.detail.ip"), string.IsNullOrWhiteSpace(detail.IPAddress) ? "-" : detail.IPAddress);
            AddInfoField(_detailInfoGrid, Lang.Get("audit.detail.description"), string.IsNullOrWhiteSpace(detail.Detail) ? "-" : detail.Detail);

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

        private void BuildDesignPreviewContent()
        {
            var root = new TableLayoutPanel
            {
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                Name = "auditPreviewRoot",
                Padding = new Padding(0),
                RowCount = 3
            };
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 228F));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var infoGrid = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                Name = "auditInfoGrid",
                RowCount = 3
            };
            infoGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            infoGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            for (int row = 0; row < 3; row++)
                infoGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));

            AddPreviewField(infoGrid, 0, 0, "Thời gian", "01/01/2026 12:00:00");
            AddPreviewField(infoGrid, 1, 0, "Người dùng", "Tên người dùng");
            AddPreviewField(infoGrid, 0, 1, "Hành động", "Cập nhật");
            AddPreviewField(infoGrid, 1, 1, "Đối tượng", "Bản ghi");
            AddPreviewField(infoGrid, 0, 2, "Địa chỉ IP", "-");
            AddPreviewField(infoGrid, 1, 2, "Mô tả", "Nội dung thay đổi");
            root.Controls.Add(infoGrid, 0, 0);

            var diffTitle = new Label
            {
                Dock = DockStyle.Fill,
                Name = "diffTitle",
                Text = "Thay đổi",
                TextAlign = ContentAlignment.MiddleLeft
            };
            root.Controls.Add(diffTitle, 0, 1);

            var diffGrid = new TableLayoutPanel
            {
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                Name = "diffPreview",
                RowCount = 1
            };
            diffGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            diffGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            diffGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            diffGrid.Controls.Add(CreatePreviewDiffPanel("Giá trị trước", "Dữ liệu cũ"), 0, 0);
            diffGrid.Controls.Add(CreatePreviewDiffPanel("Giá trị sau", "Dữ liệu mới"), 1, 0);
            root.Controls.Add(diffGrid, 0, 2);

            SetDesignPreviewContent(root);
            ContentHost.PerformLayout();
        }

        private static void AddPreviewField(
            TableLayoutPanel grid,
            int column,
            int row,
            string labelText,
            string valueText)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(8)
            };
            panel.Controls.Add(new Label
            {
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Text = valueText
            });
            panel.Controls.Add(new Label
            {
                Dock = DockStyle.Top,
                Text = labelText
            });
            grid.Controls.Add(panel, column, row);
        }

        private static Control CreatePreviewDiffPanel(string title, string value)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(8)
            };
            panel.Controls.Add(new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Text = value
            });
            panel.Controls.Add(new Label
            {
                Dock = DockStyle.Top,
                Height = 28,
                Text = title,
                TextAlign = ContentAlignment.MiddleCenter
            });
            return panel;
        }

        private static void AddInfoField(TableLayoutPanel grid, string label, string value)
        {
            var panel = new Panel { Dock = DockStyle.Fill, AutoSize = true, Margin = new Padding(0, 0, 12, 12) };

            var lblLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = AppFonts.Small.Height + 6,
                Text = label,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent
            };
            var lblValue = new Label
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                MaximumSize = new Size(340, 0),
                Text = string.IsNullOrWhiteSpace(value) ? "-" : value,
                Font = AppFonts.BodyBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent
            };

            panel.Controls.Add(lblValue);
            panel.Controls.Add(lblLabel);

            int row = grid.RowStyles.Count;
            grid.RowCount = row + 1;
            grid.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            grid.Controls.Add(panel, row % 2, row / 2);
        }

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