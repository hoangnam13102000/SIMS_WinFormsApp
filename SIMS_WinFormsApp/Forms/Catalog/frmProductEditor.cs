using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Catalog;
using SIMS_WinFormsApp.MVP.Presenters.Catalog;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Forms.Catalog
{
    public sealed class frmProductEditor : BaseFormDialogForm
    {
        private readonly ProductEditDto _model;
        private readonly bool _readOnly;
        private readonly List<LookupItem> _categories;
        private readonly List<LookupItem> _suppliers;

        private readonly PictureBox _preview = new PictureBox { SizeMode = PictureBoxSizeMode.Zoom, BackColor = AppColors.BgLighter };

        // Giữ lại tham chiếu lưới đang dùng (nhập liệu hoặc xem chi tiết) để có thể gọi lại
        // Reflow() từ OnContentReady() - xem giải thích trong OnContentReady() bên dưới.
        private FieldGridPanel _grid;

        // ===== Control của form nhập liệu (chỉ được tạo khi readOnly = false) =====
        private LabeledIconField _name;
        private LabeledComboField _category;
        private LabeledComboField _supplier;
        private LabeledIconField _brand;
        private LabeledIconField _unit;
        private LabeledIconField _weight;
        private LabeledIconField _importPrice;
        private LabeledIconField _sellPrice;
        private LabeledIconField _stock;
        private LabeledIconField _minStock;
        private LabeledIconField _description;
        private LabeledComboField _status;

        private frmProductEditor(IWin32Window owner, ProductEditDto model, IReadOnlyList<LookupItem> categories, IReadOnlyList<LookupItem> suppliers, bool readOnly)
            : base(owner)
        {
            _model = model ?? new ProductEditDto();
            _readOnly = readOnly;
            _categories = (categories ?? Array.Empty<LookupItem>()).ToList();
            _suppliers = new List<LookupItem> { new LookupItem { Id = 0, Name = "(Không chọn)" } };
            _suppliers.AddRange(suppliers ?? Array.Empty<LookupItem>());

            HeaderTitle = readOnly ? "Chi tiết sản phẩm" : (_model.ProductId > 0 ? "Sửa sản phẩm" : "Thêm sản phẩm");
            SetHeaderIcon(IconChar.Box, AppColors.Accent);
            CloseRequested += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            if (readOnly)
            {
                Size = new Size(DialogSizing.FitWidthToScreen(owner, 960), 700);
                BuildDetailView();
                AddFooterButton("Đóng", false, (s, e) => RaiseCloseRequested());
            }
            else
            {
                Size = new Size(DialogSizing.FitWidthToScreen(owner, 1280), 780);
                BuildEditForm();
                AddFooterButton("Đóng", false, (s, e) => RaiseCloseRequested());
                AddFooterButton("Lưu thay đổi", true, (s, e) => SaveAndClose());
            }
        }

        /// <summary>
        /// BuildEditForm()/BuildDetailView() chạy trong constructor, tức là TRƯỚC KHI Form có
        /// handle (xem giải thích ở BaseFormDialogForm.OnContentReady). Tại thời điểm đó
        /// ContentHost/_grid có thể vẫn mang Width "rác" nhỏ hơn thực tế, khiến FieldGridPanel bỏ
        /// qua Reflow() (Width chưa đạt ngưỡng tối thiểu) hoặc các khối tự vẽ vị trí theo Bounds
        /// của control khác (badge trong BuildDetailHeader, pill trong BuildStockRow) tính toán
        /// sai một lần rồi giữ nguyên - đây chính là nguyên nhân chữ/badge chồng lên nhau ở cả 2
        /// popup thêm/sửa và xem chi tiết. Gọi lại Reflow() + PerformLayout() ở đây (SAU khi Form
        /// đã có handle) theo đúng mẫu đã dùng ở frmEditUserAccount/frmAddEmployee.
        /// </summary>
        protected override void OnContentReady()
        {
            base.OnContentReady();
            _grid?.Reflow();
            ContentHost.PerformLayout();
            FitHeightToContent();
        }

        public static bool TryEdit(IWin32Window owner, ProductEditDto model, IReadOnlyList<LookupItem> categories, IReadOnlyList<LookupItem> suppliers, bool readOnly, out ProductEditDto result)
        {
            result = null;
            using (var dialog = new frmProductEditor(owner, model.Copy(), categories, suppliers, readOnly))
            {
                if (dialog.ShowDialog(owner) != DialogResult.OK) return false;
                result = dialog._model;
                return true;
            }
        }

        #region ===== Nhánh 1: Form nhập liệu (Thêm / Sửa) =====

        private void BuildEditForm()
        {
            _name = Field("Tên sản phẩm", IconChar.Barcode, true);
            _category = Combo("Danh mục", true);
            _supplier = Combo("Nhà cung cấp", false);
            _brand = Field("Thương hiệu", IconChar.Copyright, false);
            _unit = Field("Đơn vị tính", IconChar.ScaleBalanced, false);
            _weight = Field("Khối lượng / dung tích", IconChar.WeightScale, false);
            _importPrice = Field("Giá nhập", IconChar.MoneyBill, true);
            _sellPrice = Field("Giá bán", IconChar.Tags, true);
            _stock = Field("Tồn kho ban đầu", IconChar.BoxesStacked, false);
            _minStock = Field("Tồn kho tối thiểu", IconChar.TriangleExclamation, false);
            _description = Field("Mô tả sản phẩm", IconChar.AlignLeft, false);
            _status = Combo("Trạng thái", true);

            _category.SetItems(_categories);
            _supplier.SetItems(_suppliers);
            _status.SetItems(new[] { "Đang bán", "Ngừng bán" });

            LoadModel();
            if (_model.ProductId > 0) _stock.Enabled = false;

            var grid = new FieldGridPanel(3, 20, 16);
            grid.AddField(BuildSectionHeader(IconChar.CircleInfo, "THÔNG TIN SẢN PHẨM"), 3);
            grid.AddField(_name, 3);
            grid.AddField(_category);
            grid.AddField(_supplier);
            grid.AddField(_brand);
            grid.AddField(_unit);
            grid.AddField(_weight);
            grid.AddField(_description);
            grid.AddField(BuildSectionHeader(IconChar.Coins, "GIÁ & TỒN KHO"), 3, 36);
            grid.AddField(_importPrice);
            grid.AddField(_sellPrice);
            grid.AddField(_stock);
            grid.AddField(_minStock);
            grid.AddField(_status, 2);

            var imageRow = BuildImageEditorRow();
            var codeBar = BuildCodeBar();

            // ContentHost xếp theo kiểu Dock=Top: control thêm SAU sẽ hiển thị TRÊN control thêm
            // TRƯỚC (cùng quy ước đã dùng trong bản gốc của form này).
            ContentHost.Controls.Add(grid);
            ContentHost.Controls.Add(imageRow);
            ContentHost.Controls.Add(codeBar);
            _grid = grid;
        }

        private void LoadModel()
        {
            _name.Value = _model.Name;
            _brand.Value = _model.Brand;
            _unit.Value = _model.Unit;
            _weight.Value = _model.WeightVolume;
            _importPrice.Value = _model.ImportPrice.ToString("0");
            _sellPrice.Value = _model.SellPrice.ToString("0");
            _stock.Value = _model.Stock.ToString();
            _minStock.Value = _model.MinStock.ToString();
            _description.Value = _model.Description;
            Select(_category, _categories, _model.CategoryId);
            Select(_supplier, _suppliers, _model.SupplierId ?? 0);
            _status.SelectedItem = _model.IsActive ? "Đang bán" : "Ngừng bán";
            ShowPreview(_model.ImagePath);
        }

        private void SaveAndClose()
        {
            if (!decimal.TryParse((_importPrice.Value ?? string.Empty).Replace(".", string.Empty).Replace(",", string.Empty), out decimal importPrice) ||
                !decimal.TryParse((_sellPrice.Value ?? string.Empty).Replace(".", string.Empty).Replace(",", string.Empty), out decimal sellPrice))
            {
                DialogHelper.ShowWarning(this, "Giá nhập và giá bán phải là số.");
                return;
            }

            int stock = _model.Stock;
            int minStock = _model.MinStock;
            int.TryParse(_stock.Value, out stock);
            int.TryParse(_minStock.Value, out minStock);
            var category = _category.SelectedItem as LookupItem;
            var supplier = _supplier.SelectedItem as LookupItem;

            _model.Name = _name.Value;
            _model.CategoryId = category == null ? 0 : category.Id;
            _model.SupplierId = supplier == null || supplier.Id <= 0 ? (int?)null : supplier.Id;
            _model.Brand = _brand.Value;
            _model.Unit = _unit.Value;
            _model.WeightVolume = _weight.Value;
            _model.Description = _description.Value;
            _model.ImportPrice = importPrice;
            _model.SellPrice = sellPrice;
            _model.Stock = stock;
            _model.MinStock = minStock;
            _model.IsActive = !string.Equals(_status.SelectedItem as string, "Ngừng bán", StringComparison.Ordinal);
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>Thanh hiển thị mã sản phẩm ở đầu popup thêm/sửa (chỉ để xem, không phải input
        /// mới - <see cref="ProductEditDto.Code"/> đã có sẵn trong model, không phát sinh field mới).</summary>
        private Control BuildCodeBar()
        {
            var host = new CardPanel { Dock = DockStyle.Top, Height = 40, FillColor = AppColors.BgLighter, Margin = new Padding(0, 0, 0, 12) };
            var icon = new IconPictureBox
            {
                IconChar = IconChar.Hashtag,
                IconColor = AppColors.TextMuted,
                IconSize = 14,
                Size = new Size(14, 14),
                Location = new Point(16, 13),
                BackColor = Color.Transparent
            };
            var label = new Label
            {
                AutoSize = true,
                Text = string.IsNullOrWhiteSpace(_model.Code) ? "Mã sản phẩm" : _model.Code,
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                Location = new Point(38, 12)
            };
            host.Controls.Add(label);
            host.Controls.Add(icon);
            return host;
        }

        private Control BuildImageEditorRow()
        {
            var host = new Panel { Dock = DockStyle.Top, Height = 120, BackColor = Color.Transparent, Margin = new Padding(0, 0, 0, 12) };

            var caption = new Label
            {
                AutoSize = true,
                Text = "Hình ảnh",
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                Location = new Point(0, 2),
                Padding = new Padding(0, 2, 0, 2)
            };

            _preview.Size = new Size(72, 72);
            _preview.Location = new Point(0, 28);

            int uploadWidth = TextRenderer.MeasureText("Tải ảnh lên", AppFonts.Button).Width + 36;
            int clearWidth = TextRenderer.MeasureText("Xóa ảnh", AppFonts.Button).Width + 36;
            var upload = new PrimaryButton
            {
                Text = "Tải ảnh lên",
                IsPrimary = false,
                Size = new Size(uploadWidth, 40),
                Location = new Point(_preview.Right + 16, 30)
            };
            var clear = new PrimaryButton
            {
                Text = "Xóa ảnh",
                IsPrimary = false,
                Size = new Size(clearWidth, 40),
                Location = new Point(upload.Right + 12, 30)
            };
            var hint = new Label
            {
                AutoSize = true,
                Text = "Tùy chọn · JPG, PNG, BMP, GIF · tối đa 5MB",
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                Location = new Point(_preview.Right + 16, upload.Bottom + 8)
            };

            upload.Click += (s, e) => ChooseImage();
            clear.Click += (s, e) => { _model.ImagePath = null; ShowPreview(null); };

            host.Controls.Add(hint);
            host.Controls.Add(clear);
            host.Controls.Add(upload);
            host.Controls.Add(_preview);
            host.Controls.Add(caption);
            return host;
        }

        private void ChooseImage()
        {
            using (var dialog = new OpenFileDialog
            {
                Filter = "Ảnh|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Title = "Chọn ảnh sản phẩm"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                _model.ImagePath = dialog.FileName;
                ShowPreview(dialog.FileName);
            }
        }

        private void ShowPreview(string path)
        {
            Image previous = _preview.Image;
            _preview.Image = null;
            if (previous != null) previous.Dispose();
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return;
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                _preview.Image = Image.FromStream(stream);
        }

        private static LabeledIconField Field(string label, IconChar icon, bool required)
        {
            return new LabeledIconField { LabelText = label, Icon = icon, IsRequired = required, Dock = DockStyle.Top };
        }

        private static LabeledComboField Combo(string label, bool required)
        {
            return new LabeledComboField { LabelText = label, IsRequired = required, Dock = DockStyle.Top };
        }

        private static void Select(LabeledComboField combo, IList<LookupItem> items, int id)
        {
            combo.SelectedItem = items.FirstOrDefault(item => item.Id == id) ?? (items.Count > 0 ? items[0] : null);
        }

        #endregion

        #region ===== Nhánh 2: Popup xem chi tiết (chỉ đọc) =====

        private void BuildDetailView()
        {
            string categoryName = _categories.FirstOrDefault(c => c.Id == _model.CategoryId)?.Name;
            bool outOfStock = _model.Stock <= 0;
            bool lowStock = !outOfStock && _model.Stock <= _model.MinStock;

            var grid = new FieldGridPanel(3, 20, 8);
            grid.AddField(BuildPriceSummary(), 3);
            grid.AddField(new InfoItem(IconChar.Tags, "Danh mục") { Value = categoryName });
            grid.AddField(new InfoItem(IconChar.Hashtag, "Mã sản phẩm") { Value = _model.Code });
            grid.AddField(new InfoItem(IconChar.Copyright, "Thương hiệu") { Value = _model.Brand });
            grid.AddField(new InfoItem(IconChar.ScaleBalanced, "Đơn vị tính") { Value = _model.Unit });
            grid.AddField(new InfoItem(IconChar.WeightScale, "Khối lượng / Dung tích") { Value = _model.WeightVolume });
            grid.AddField(new InfoItem(IconChar.TriangleExclamation, "Tồn kho tối thiểu") { Value = $"{_model.MinStock} sản phẩm" });
            grid.AddField(BuildStockRow(outOfStock, lowStock), 3);
            if (!string.IsNullOrWhiteSpace(_model.Description))
                grid.AddField(BuildDescriptionBlock(), 3);

            var header = BuildDetailHeader(categoryName, outOfStock, lowStock);

            ContentHost.Controls.Add(grid);
            ContentHost.Controls.Add(header);
            _grid = grid;

            ShowPreview(_model.ImagePath);
        }

        private Control BuildDetailHeader(string categoryName, bool outOfStock, bool lowStock)
        {
            var host = new Panel { Dock = DockStyle.Top, Height = 152, BackColor = Color.Transparent, Margin = new Padding(0, 0, 0, 8) };

            _preview.Size = new Size(128, 128);
            _preview.Location = new Point(0, 4);

            var lblName = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(640, 0),
                Text = string.IsNullOrWhiteSpace(_model.Name) ? "(Chưa đặt tên)" : _model.Name,
                Font = AppFonts.HeadingLg,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                Location = new Point(_preview.Right + 24, 6)
            };

            var categoryPill = new Pill();
            categoryPill.SetColors(AppColors.InfoBg, AppColors.Info);
            categoryPill.SetText(string.IsNullOrWhiteSpace(categoryName) ? "Chưa phân loại" : categoryName);

            var statusPill = new Pill();
            statusPill.SetColors(_model.IsActive ? AppColors.SuccessBg : AppColors.BgLighter, _model.IsActive ? AppColors.Success : AppColors.TextMuted);
            statusPill.SetText(_model.IsActive ? "Đang bán" : "Ngừng bán");

            var stockPill = new Pill();
            stockPill.SetColors(
                outOfStock ? AppColors.ErrorBg : lowStock ? AppColors.WarningBg : AppColors.SuccessBg,
                outOfStock ? AppColors.Error : lowStock ? AppColors.Warning : AppColors.Success);
            stockPill.SetText(outOfStock ? "Hết hàng" : lowStock ? "Sắp hết hàng" : "Còn hàng");

            var pills = new[] { categoryPill, statusPill, stockPill };
            foreach (var pill in pills) host.Controls.Add(pill);
            host.Controls.Add(lblName);
            host.Controls.Add(_preview);

            // Cùng mẫu Reflow-theo-Resize đã dùng ở BuildPriceSummary/BuildStockRow bên dưới:
            // tính lại vị trí badge SAU KHI lblName đã có Bounds cuối cùng thay vì tính 1 lần duy
            // nhất ngay lúc dựng control (khi đó Bounds của các control khác trong host có thể
            // chưa "chốt" - xem OnContentReady()).
            bool isReflowing = false;
            void Reflow()
            {
                if (isReflowing) return;
                isReflowing = true;
                try
                {
                    lblName.MaximumSize = new Size(Math.Max(100, host.ClientSize.Width - lblName.Left - 20), 0);

                    int badgeX = lblName.Left;
                    int badgeY = lblName.Bottom + 10;
                    foreach (var pill in pills)
                    {
                        if (badgeX > lblName.Left && badgeX + pill.Width > host.ClientSize.Width - 4)
                        {
                            badgeX = lblName.Left;
                            badgeY += pill.Height + 8;
                        }
                        pill.Location = new Point(badgeX, badgeY);
                        badgeX += pill.Width + 8;
                    }
                    int desiredHeight = Math.Max(_preview.Bottom + 4, badgeY + pills[pills.Length - 1].Height + 8);
                    if (host.Height != desiredHeight) host.Height = desiredHeight;
                }
                finally
                {
                    isReflowing = false;
                }
            }
            host.Resize += (s, e) => Reflow();
            Reflow();

            return host;
        }

        private Control BuildPriceSummary()
        {
            var card = new CardPanel { Dock = DockStyle.Top, Height = 160, FillColor = AppColors.BgLighter };

            var lblImportLabel = InfoLabel("Giá nhập", false);
            var lblImportValue = InfoLabel($"{CatalogFormat.Money(_model.ImportPrice)} đ", true);
            var lblSellLabel = InfoLabel("Giá bán", false);
            var lblSellValue = InfoLabel($"{CatalogFormat.Money(_model.SellPrice)} đ", true);
            lblSellValue.ForeColor = AppColors.Accent;

            decimal profit = _model.SellPrice - _model.ImportPrice;
            double percent = _model.ImportPrice > 0 ? (double)(profit / _model.ImportPrice) * 100.0 : 0;
            bool positive = profit >= 0;

            var profitIcon = new IconPictureBox
            {
                IconChar = IconChar.ChartLine,
                IconColor = positive ? AppColors.Success : AppColors.Error,
                IconSize = 14,
                Size = new Size(14, 14),
                BackColor = Color.Transparent
            };
            var lblProfit = new Label
            {
                AutoSize = false,
                Font = AppFonts.SmallBold,
                ForeColor = positive ? AppColors.Success : AppColors.Error,
                BackColor = Color.Transparent,
                Text = $"Lợi nhuận mỗi sản phẩm: {(positive ? string.Empty : "-")}{CatalogFormat.Money(Math.Abs(profit))} đ ({percent:0.0}%)",
                Padding = new Padding(0, 4, 0, 4)
            };

            card.Controls.Add(lblProfit);
            card.Controls.Add(profitIcon);
            card.Controls.Add(lblSellValue);
            card.Controls.Add(lblSellLabel);
            card.Controls.Add(lblImportValue);
            card.Controls.Add(lblImportLabel);

            void Reflow()
            {
                int half = card.Width / 2;
                lblImportLabel.Location = new Point(20, 16);
                lblImportValue.Location = new Point(20, lblImportLabel.Bottom + 2);
                lblSellLabel.Location = new Point(half, 16);
                lblSellValue.Location = new Point(half, lblSellLabel.Bottom + 2);
                int lineY = Math.Max(lblImportValue.Bottom, lblSellValue.Bottom) + 8;
                profitIcon.Location = new Point(20, lineY + 3);
                int profitLeft = profitIcon.Right + 8;
                int profitWidth = Math.Max(40, card.ClientSize.Width - profitLeft - 20);
                Size profitSize = TextRenderer.MeasureText(
                    lblProfit.Text,
                    lblProfit.Font,
                    new Size(profitWidth - lblProfit.Padding.Horizontal, int.MaxValue),
                    TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix);
                int profitHeight = profitSize.Height + lblProfit.Padding.Vertical + 4;
                lblProfit.SetBounds(profitLeft, lineY, profitWidth, profitHeight);
                int desiredHeight = lblProfit.Bottom + 12;
                if (card.Height != desiredHeight) card.Height = desiredHeight;
            }
            card.Resize += (s, e) => Reflow();
            Reflow();

            return card;
        }

        private Control BuildStockRow(bool outOfStock, bool lowStock)
        {
            var row = new Panel { Dock = DockStyle.Top, Height = 100, BackColor = Color.Transparent };

            var iconBox = new IconBadge(IconChar.Warehouse);

            var lblLabel = new Label
            {
                AutoSize = false,
                Text = "Tồn kho",
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                UseMnemonic = false,
                Padding = new Padding(0, 5, 0, 5)
            };
            var lblValue = new Label
            {
                AutoSize = false,
                Text = $"{_model.Stock} sản phẩm",
                Font = AppFonts.HeadingMd,
                ForeColor = _model.Stock <= 0 ? AppColors.Error : AppColors.TextTitle,
                BackColor = Color.Transparent,
                UseMnemonic = false,
                Padding = new Padding(0, 5, 0, 7)
            };

            var pill = new Pill();
            pill.SetColors(
                outOfStock ? AppColors.ErrorBg : lowStock ? AppColors.WarningBg : AppColors.SuccessBg,
                outOfStock ? AppColors.Error : lowStock ? AppColors.Warning : AppColors.Success);
            pill.SetText(outOfStock ? "Hết hàng" : lowStock ? "Sắp hết hàng" : "Còn hàng");

            row.Controls.Add(pill);
            row.Controls.Add(lblValue);
            row.Controls.Add(lblLabel);
            row.Controls.Add(iconBox);

            bool isReflowing = false;
            void Reflow()
            {
                if (isReflowing) return;
                isReflowing = true;
                try
                {
                    int textWidth = Math.Max(80, row.ClientSize.Width - 68);
                    int labelHeight = MeasureDetailTextHeight(lblLabel.Text, lblLabel.Font, textWidth) + lblLabel.Padding.Vertical;
                    lblLabel.SetBounds(52, 8, textWidth, labelHeight);

                    int valueHeight = MeasureDetailTextHeight(lblValue.Text, lblValue.Font, textWidth) + lblValue.Padding.Vertical;
                    lblValue.SetBounds(52, lblLabel.Bottom + 2, textWidth, valueHeight);

                    int desiredHeight = Math.Max(96, lblValue.Bottom + 12);
                    desiredHeight = Math.Max(desiredHeight, pill.Height + 16);
                    if (row.Height != desiredHeight) row.Height = desiredHeight;

                    pill.Location = new Point(Math.Max(200, row.ClientSize.Width - pill.Width), (row.Height - pill.Height) / 2);
                }
                finally
                {
                    isReflowing = false;
                }
            }
            row.Resize += (s, e) => Reflow();
            Reflow();

            return row;
        }

        private static int MeasureDetailTextHeight(string text, Font font, int width)
        {
            return TextRenderer.MeasureText(
                text ?? string.Empty,
                font,
                new Size(Math.Max(1, width), int.MaxValue),
                TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix | TextFormatFlags.NoPadding).Height;
        }

        private Control BuildDescriptionBlock()
        {
            var host = new Panel { Dock = DockStyle.Top, Height = 76, BackColor = Color.Transparent, Padding = new Padding(0, 8, 0, 0) };
            var caption = new Label
            {
                AutoSize = true,
                Text = "Mô tả sản phẩm",
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.TextTitle,
                BackColor = Color.Transparent,
                Location = new Point(0, 8)
            };
            var body = new Label
            {
                AutoSize = true,
                Text = _model.Description,
                Font = AppFonts.Body,
                ForeColor = AppColors.TextSecondary,
                BackColor = Color.Transparent,
                Location = new Point(0, caption.Bottom + 4)
            };
            void Reflow()
            {
                body.MaximumSize = new Size(Math.Max(10, host.ClientSize.Width), 0);
                body.Location = new Point(0, caption.Bottom + 4);
                int desiredHeight = body.Bottom + 8;
                if (host.Height != desiredHeight) host.Height = desiredHeight;
            }
            host.Resize += (s, e) => Reflow();
            Reflow();

            host.Controls.Add(body);
            host.Controls.Add(caption);
            return host;
        }

        private static Label InfoLabel(string text, bool big)
        {
            return new Label
            {
                AutoSize = true,
                Text = text,
                Font = big ? AppFonts.HeadingMd : AppFonts.Small,
                ForeColor = big ? AppColors.TextTitle : AppColors.TextMuted,
                BackColor = Color.Transparent,
                Padding = big ? new Padding(0, 4, 0, 6) : new Padding(0, 5, 0, 3)
            };
        }

        #endregion

        #region ===== Thành phần dùng chung cho cả 2 nhánh =====

        private static Control BuildSectionHeader(IconChar icon, string text)
        {
            var host = new Panel { Dock = DockStyle.Top, Height = 30, BackColor = Color.Transparent, Margin = new Padding(0, 4, 0, 6) };
            var iconBox = new IconPictureBox
            {
                IconChar = icon,
                IconColor = AppColors.Accent,
                IconSize = 15,
                Size = new Size(15, 15),
                Location = new Point(0, 7),
                BackColor = Color.Transparent
            };
            var label = new Label
            {
                AutoSize = true,
                Text = text,
                Font = AppFonts.SmallBold,
                ForeColor = AppColors.Accent,
                BackColor = Color.Transparent,
                Location = new Point(22, 5)
            };
            var line = new Panel { BackColor = AppColors.Border };

            void Reflow()
            {
                line.Location = new Point(0, host.Height - 1);
                line.Size = new Size(Math.Max(0, host.Width), 1);
            }
            host.Resize += (s, e) => Reflow();
            Reflow();

            host.Controls.Add(line);
            host.Controls.Add(label);
            host.Controls.Add(iconBox);
            return host;
        }


        #endregion
    }
}