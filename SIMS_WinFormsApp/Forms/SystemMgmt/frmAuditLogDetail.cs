using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs;
using SIMS_WinFormsApp.Models.Mapping;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    /// <summary>Dialog "Xem" 1 dòng nhật ký - thuần hiển thị dữ liệu đã có sẵn, không có
    /// nghiệp vụ/validate nào nên không cần Presenter riêng (YAGNI).</summary>
    public sealed class frmAuditLogDetail : BaseFormDialogForm
    {
        private static readonly Size DialogSize = new Size(760, 620);

        private frmAuditLogDetail(IWin32Window owner, AuditLogDetailDto detail) : base(owner)
        {
            Size = DialogSize;
            BuildContent(detail);

            CloseRequested += (s, e) => Close();
            AddFooterButton("Đóng", isPrimary: true, onClick: (s, e) => RaiseCloseRequested());
        }

        public static DialogResult Show(IWin32Window owner, AuditLogDetailDto detail)
        {
            using (var dialog = new frmAuditLogDetail(owner, detail))
            {
                return dialog.ShowDialog(owner);
            }
        }

        private void BuildContent(AuditLogDetailDto detail)
        {
            HeaderTitle = "Chi tiết nhật ký #" + detail.LogId;
            SetHeaderIcon(IconChar.ClockRotateLeft, AppColors.Accent);

            var infoGrid = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Margin = new Padding(0, 0, 0, 16)
            };
            infoGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            infoGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

            AddInfoField(infoGrid, "Thời gian", detail.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss"));
            AddInfoField(infoGrid, "Người dùng", detail.Username);
            AddInfoField(infoGrid, "Hành động", AuditLogDisplayMapper.GetActionLabel(detail.Action));
            AddInfoField(infoGrid, "Đối tượng", AuditLogDisplayMapper.GetTableLabel(detail.TableName)
                + (detail.RecordId.HasValue ? " #" + detail.RecordId.Value : ""));
            AddInfoField(infoGrid, "Địa chỉ IP", string.IsNullOrWhiteSpace(detail.IPAddress) ? "-" : detail.IPAddress);
            AddInfoField(infoGrid, "Mô tả", string.IsNullOrWhiteSpace(detail.Detail) ? "-" : detail.Detail);

            var diffHeader = new Label
            {
                Dock = DockStyle.Top,
                Height = 24,
                Text = "So sánh dữ liệu",
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent
            };

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

                diffGrid.Controls.Add(CreateDiffPanel("Trước khi thay đổi", detail.OldValue, AppColors.ErrorBg, AppColors.Error), 0, 0);
                diffGrid.Controls.Add(CreateDiffPanel("Sau khi thay đổi", detail.NewValue, AppColors.SuccessBg, AppColors.Success), 1, 0);
                diffContent = diffGrid;
            }
            else
            {
                diffContent = new Label
                {
                    Dock = DockStyle.Fill,
                    Text = "Bản ghi này không có dữ liệu trước/sau để so sánh.",
                    Font = AppFonts.Body,
                    ForeColor = AppColors.TextMuted,
                    BackColor = Color.Transparent,
                    TextAlign = ContentAlignment.MiddleCenter
                };
            }

            var diffHost = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            diffHost.Controls.Add(diffContent);

            ContentHost.Controls.Add(diffHost);
            ContentHost.Controls.Add(diffHeader);
            ContentHost.Controls.Add(infoGrid);
        }

        private static void AddInfoField(TableLayoutPanel grid, string label, string value)
        {
            var panel = new Panel { Dock = DockStyle.Fill, AutoSize = true, Margin = new Padding(0, 0, 12, 12) };

            var lblLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 18,
                Text = label,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent
            };
            var lblValue = new Label
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                MaximumSize = new Size(320, 0),
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
                Height = 26,
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
                Text = string.IsNullOrWhiteSpace(value) ? "(trống)" : value
            };

            panel.Controls.Add(textBox);
            panel.Controls.Add(badge);
            return panel;
        }
    }
}