using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Forms.Dashboard;
using SIMS_WinFormsApp.Infrastructure.Composition;
using SIMS_WinFormsApp.MVP.Presenters;
using SIMS_WinFormsApp.Repositories.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Controls.Loading;
using SIMS_WinFormsApp.UI.Controls.Toast;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Interfaces;

namespace SIMS_WinFormsApp.Forms.SystemMgmt
{
    public sealed class ucSystemSettings : UserControl, ISystemSettingsView
    {
        private readonly IStoreConfigRepository _repository;
        private readonly SystemSettingsPresenter _presenter;
        private LoadingOverlayHost _overlayHost;
        private PrimaryButton _btnSave;

        private LabeledIconField _fieldStoreName;
        private LabeledIconField _fieldDefaultUnit;
        private LabeledIconField _fieldVatRate;
        private LabeledIconField _fieldDefaultMargin;
        private LabeledIconField _fieldReturnDays;
        private LabeledIconField _fieldApprovalThreshold;

        private bool _dataLoadedOnce;

        public ucSystemSettings(IStoreConfigRepository repository = null)
        {
            AutoScaleMode = AutoScaleMode.None;
            Font = new Font("Segoe UI", 9f);
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            BackColor = AppColors.PageBg;
            Padding = new Padding(20, 16, 20, 20);

            _repository = repository ?? AppComposition.CreateStoreConfigRepository();

            BuildUI();

            _presenter = new SystemSettingsPresenter(this, _repository, _overlayHost);

       
            VisibleChanged += (s, e) =>
            {
                if (Visible && !_dataLoadedOnce)
                {
                    _dataLoadedOnce = true;
                    _presenter.Load();
                }
            };
        }

        #region ISystemSettingsView
        public string StoreName
        {
            get => _fieldStoreName.Value;
            set => _fieldStoreName.Value = value;
        }

        public string DefaultUnit
        {
            get => _fieldDefaultUnit.Value;
            set => _fieldDefaultUnit.Value = value;
        }

        public string VatRateText
        {
            get => _fieldVatRate.Value;
            set => _fieldVatRate.Value = value;
        }

        public string DefaultMarginText
        {
            get => _fieldDefaultMargin.Value;
            set => _fieldDefaultMargin.Value = value;
        }

        public string ReturnPolicyDaysText
        {
            get => _fieldReturnDays.Value;
            set => _fieldReturnDays.Value = value;
        }

        public string ApprovalThresholdText
        {
            get => _fieldApprovalThreshold.Value;
            set => _fieldApprovalThreshold.Value = value;
        }

        public event EventHandler SaveRequested;

        public void SetSaving(bool isSaving)
        {
            _fieldStoreName.Enabled = !isSaving;
            _fieldDefaultUnit.Enabled = !isSaving;
            _fieldVatRate.Enabled = !isSaving;
            _fieldDefaultMargin.Enabled = !isSaving;
            _fieldReturnDays.Enabled = !isSaving;
            _fieldApprovalThreshold.Enabled = !isSaving;
            _btnSave.Enabled = !isSaving;
            _btnSave.Text = isSaving ? "Đang lưu..." : "Lưu thay đổi";
        }

        public void ShowError(string message) => AppToast.Error(this, message);

        public void ShowSuccess(string message) => AppToast.Success(this, message);
        #endregion

        #region Dựng giao diện (chỉ chạy 1 lần lúc khởi tạo)
        private void BuildUI()
        {
            SuspendLayout();

            var root = new Panel { Dock = DockStyle.Fill, BackColor = AppColors.PageBg };
            var headerHost = BuildHeaderRow();

            var scrollHost = new BufferedPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = AppColors.PageBg
            };

            scrollHost.Controls.Add(CreateTwoColumnRow(BuildPricingCard(), BuildReturnPolicyCard()));
            scrollHost.Controls.Add(BuildStoreInfoCard());

            _overlayHost = new LoadingOverlayHost(scrollHost) { Dock = DockStyle.Fill };

            root.Controls.Add(_overlayHost);
            root.Controls.Add(headerHost);

            Controls.Add(root);
            ResumeLayout(true);
        }

        private Control BuildHeaderRow()
        {
            var header = new HeaderSection
            {
                Title = "Cài đặt hệ thống",
                Subtitle = "Cấu hình chung áp dụng cho toàn bộ cửa hàng",
                Icon = IconChar.Gear,
                Dock = DockStyle.Fill
            };

            var host = new Panel
            {
                Dock = DockStyle.Top,
                Height = 108,
                // KHÔNG dùng Color.Transparent khi lồng 1 control tự vẽ (HeaderSection) vào Panel -
                // cùng lý do đã ghi chú ở BaseTable.WrapHeaderWithAddButton (tránh viền/góc đen).
                BackColor = AppColors.PageBg,
                Margin = new Padding(0, 0, 0, 16)
            };
            host.Controls.Add(header);

            const string saveText = "Lưu thay đổi";
            int textWidth;
            using (var g = host.CreateGraphics())
                textWidth = TextRenderer.MeasureText(g, saveText, AppFonts.Button).Width;

            _btnSave = new PrimaryButton
            {
                Text = saveText,
                IsPrimary = true,
                Size = new Size(textWidth + 48, 42),
                CornerRadius = AppRadius.Medium,
                Font = AppFonts.Button,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            _btnSave.Click += (s, e) => SaveRequested?.Invoke(this, EventArgs.Empty);
            host.Controls.Add(_btnSave);
            _btnSave.BringToFront();

            void Reposition() => _btnSave.Location = new Point(
                host.ClientSize.Width - _btnSave.Width - 24,
                (host.ClientSize.Height - _btnSave.Height) / 2);
            host.Resize += (s, e) => Reposition();
            Reposition();

            return host;
        }

        private Panel BuildStoreInfoCard()
        {
            var card = new Panel { Dock = DockStyle.Top, BackColor = Color.Transparent, Padding = new Padding(24), Margin = new Padding(0, 0, 0, 16) };
            card.Paint += (s, e) => PaintCard(card, e);

            var header = CreateSectionHeader(
                "Thông tin cửa hàng",
                "Thông tin cơ bản hiển thị trên hoá đơn, báo cáo và giao diện",
                IconChar.Shop, AppColors.Accent, AppColors.AccentBgSoft);
            header.Dock = DockStyle.Top;
            header.Margin = new Padding(0, 0, 0, 8);

            _fieldStoreName = new LabeledIconField
            {
                LabelText = "Tên cửa hàng",
                Icon = IconChar.Shop,
                IsRequired = true,
                PlaceholderText = "Nhập tên cửa hàng",
                HintText = "Tên hiển thị trên hoá đơn, màn hình POS và các báo cáo.",
                MaxLength = 150
            };
            _fieldDefaultUnit = new LabeledIconField
            {
                LabelText = "Đơn vị tính mặc định",
                Icon = IconChar.Box,
                PlaceholderText = "VD: cái, hộp, chai...",
                HintText = "Dùng khi thêm SP mới (vd: cái, hộp, chai...).",
                MaxLength = 30
            };

            var fieldsRow = CreateTwoColumnRow(_fieldStoreName, _fieldDefaultUnit);
            fieldsRow.Dock = DockStyle.Top;

            card.Controls.Add(fieldsRow);
            card.Controls.Add(header);

            card.Resize += (s, e) => ReflowCard(card);
            ReflowCard(card);
            return card;
        }

        private Panel BuildPricingCard()
        {
            var card = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent, Padding = new Padding(24) };
            card.Paint += (s, e) => PaintCard(card, e);

            var header = CreateSectionHeader(
                "Thuế & chính sách giá",
                "Cấu hình thuế và quy tắc tính giá bán",
                IconChar.Percent, AppColors.Success, AppColors.SuccessBg);
            header.Dock = DockStyle.Top;
            header.Margin = new Padding(0, 0, 0, 8);

            _fieldVatRate = new LabeledIconField
            {
                LabelText = "Thuế GTGT - VAT (%)",
                Icon = IconChar.Percent,
                PlaceholderText = "0",
                HintText = "Áp dụng cho hoá đơn POS và đơn hàng online.",
                MaxLength = 6
            };
            _fieldDefaultMargin = new LabeledIconField
            {
                LabelText = "Chênh lệch giá bán (VNĐ)",
                Icon = IconChar.Coins,
                PlaceholderText = "0",
                HintText = "Giá bán = Giá nhập + số này.",
                MaxLength = 12
            };

            var fieldsRow = CreateTwoColumnRow(_fieldVatRate, _fieldDefaultMargin);
            fieldsRow.Dock = DockStyle.Top;

            card.Controls.Add(fieldsRow);
            card.Controls.Add(header);

            card.Resize += (s, e) => ReflowCard(card);
            ReflowCard(card);
            return card;
        }

        private Panel BuildReturnPolicyCard()
        {
            var card = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent, Padding = new Padding(24) };
            card.Paint += (s, e) => PaintCard(card, e);

            var header = CreateSectionHeader(
                "Chính sách đổi trả",
                "Quy định thời gian và quy trình duyệt phiếu",
                IconChar.RightLeft, AppColors.Warning, AppColors.WarningBg);
            header.Dock = DockStyle.Top;
            header.Margin = new Padding(0, 0, 0, 8);

            _fieldReturnDays = new LabeledIconField
            {
                LabelText = "Số ngày đổi/trả",
                Icon = IconChar.CalendarDays,
                PlaceholderText = "0",
                HintText = "Số ngày kể từ ngày mua.",
                MaxLength = 5
            };
            _fieldApprovalThreshold = new LabeledIconField
            {
                LabelText = "Ngưỡng cần duyệt (VNĐ)",
                Icon = IconChar.UserShield,
                PlaceholderText = "0",
                HintText = "Lớn hơn số này ở trạng thái Chờ duyệt.",
                MaxLength = 12
            };

            var fieldsRow = CreateTwoColumnRow(_fieldReturnDays, _fieldApprovalThreshold);
            fieldsRow.Dock = DockStyle.Top;

            card.Controls.Add(fieldsRow);
            card.Controls.Add(header);

            card.Resize += (s, e) => ReflowCard(card);
            ReflowCard(card);
            return card;
        }

        private static Control CreateSectionHeader(string title, string subtitle, IconChar icon, Color iconColor, Color iconBg)
        {
            const int minHeaderHeight = 80;
            var row = new Panel { Height = minHeaderHeight, BackColor = Color.Transparent };

            var iconCircle = new Panel { Size = new Size(40, 40), Location = new Point(0, 2), BackColor = Color.Transparent };
            iconCircle.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(iconBg))
                    e.Graphics.FillEllipse(brush, 0, 0, iconCircle.Width - 1, iconCircle.Height - 1);
            };
            var icon2 = new IconPictureBox
            {
                IconChar = icon,
                IconColor = iconColor,
                IconSize = 18,
                Size = new Size(18, 18),
                Location = new Point(11, 11),
                BackColor = Color.Transparent
            };
            iconCircle.Controls.Add(icon2);

            var lblTitle = new Label
            {
                AutoSize = false,
                Text = title,
                Font = AppFonts.Subtitle,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                Location = new Point(52, 0),
                AutoEllipsis = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0)
            };
            var lblSubtitle = new Label
            {
                AutoSize = false,
                Text = subtitle,
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                Location = new Point(52, 32),
                AutoEllipsis = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0)
            };

            void UpdateLabelSizes()
            {
                int contentWidth = Math.Max(140, row.Width - 92);

                Func<Label, string, Font, int, Font> fitFont = (label, text, original, maxWidth) =>
                {
                    float size = original.Size;
                    while (size > 8.5f)
                    {
                        using (var probe = new Font(original.FontFamily, size, original.Style, GraphicsUnit.Point))
                        {
                            if (TextRenderer.MeasureText(text, probe).Width <= maxWidth)
                                return probe;
                        }
                        size -= 0.5f;
                    }
                    return new Font(original.FontFamily, 8.5f, original.Style, GraphicsUnit.Point);
                };

                lblTitle.Font = fitFont(lblTitle, title, lblTitle.Font, contentWidth);
                lblSubtitle.Font = fitFont(lblSubtitle, subtitle, lblSubtitle.Font, contentWidth);

                lblTitle.Width = Math.Min(contentWidth, TextRenderer.MeasureText(title, lblTitle.Font).Width + 8);
                lblSubtitle.Width = Math.Min(contentWidth, TextRenderer.MeasureText(subtitle, lblSubtitle.Font).Width + 8);
                int titleHeight = lblTitle.GetPreferredSize(new Size(lblTitle.Width, 0)).Height;
                int subtitleHeight = lblSubtitle.GetPreferredSize(new Size(lblSubtitle.Width, 0)).Height;
                row.Height = Math.Max(minHeaderHeight, titleHeight + subtitleHeight + 18);
            }

            row.Resize += (_, __) => UpdateLabelSizes();
            UpdateLabelSizes();

            row.Controls.Add(lblSubtitle);
            row.Controls.Add(lblTitle);
            row.Controls.Add(iconCircle);
            return row;
        }

        private static Panel CreateTwoColumnRow(Control left, Control right)
        {
            var row = new Panel { Dock = DockStyle.Top, BackColor = Color.Transparent };
            left.Dock = DockStyle.None;
            right.Dock = DockStyle.None;

            void Layout()
            {
                const int gap = 24;
                int half = Math.Max(10, (row.Width - gap) / 2);
                left.Width = half;
                left.Location = new Point(0, 0);
                right.Width = Math.Max(10, row.Width - half - gap);
                right.Location = new Point(left.Right + gap, 0);
                row.Height = Math.Max(left.Height, right.Height);
            }

            row.Controls.Add(right);
            row.Controls.Add(left);
            row.Resize += (s, e) => Layout();
            Layout();

            return row;
        }

        private static void PaintCard(Control card, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
            using (var path = AppRadius.GetRoundedPath(rect, AppRadius.Large))
            using (var brush = new SolidBrush(AppColors.White))
            using (var pen = new Pen(AppColors.Border, 1f))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }
        }

        private static void ReflowCard(Panel card)
        {
            if (card == null || card.Width <= card.Padding.Horizontal) return;

            card.PerformLayout();
            int bottom = 0;
            foreach (Control child in card.Controls)
            {
                if (child.Visible) bottom = Math.Max(bottom, child.Bottom);
            }

            int newHeight = bottom + card.Padding.Bottom;
            if (card.Height != newHeight) card.Height = newHeight;
        }
        #endregion
    }
}