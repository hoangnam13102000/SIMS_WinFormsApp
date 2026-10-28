using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.Models.DTOs.Promotions;
using SIMS_WinFormsApp.Services.Interfaces;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;
using SIMS_WinFormsApp.Views.Toast;

namespace SIMS_WinFormsApp.Views.Sales
{
    public sealed class PromotionManagementPage : UserControl
    {
        private readonly IPromotionManagementService _service;
        private readonly TextBox _search = new TextBox();
        private readonly CheckBox _includeDeleted = new CheckBox();
        private readonly DataGridView _grid = new DataGridView();
        private readonly UserManagementPager _pager = new UserManagementPager();
        private readonly List<PromotionDto> _currentRows = new List<PromotionDto>();
        private TextBox _code;
        private TextBox _name;
        private ComboBox _type;
        private TextBox _value;
        private TextBox _max;
        private TextBox _minimum;
        private DateTimePicker _start;
        private DateTimePicker _end;
        private TextBox _limit;
        private CheckBox _active;
        private CheckBox _banner;
        private Form _editorDialog;
        private int _selectedId;

        public PromotionManagementPage(IPromotionManagementService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(244, 247, 250);
            Dock = DockStyle.Fill;
            BuildLayout();
            Load += (sender, args) => Reload();
        }

        private void BuildLayout()
        {
            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24),
                BackColor = AppColors.PageBg,
                ColumnCount = 1,
                RowCount = 4
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 92));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
            Controls.Add(root);

            var header = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent, Margin = Padding.Empty };
            header.Controls.Add(new Label
            {
                Text = "Quản lý mã giảm giá",
                AutoSize = true,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = AppColors.TextTitle,
                Location = new Point(0, 0)
            });
            header.Controls.Add(new Label
            {
                Text = "Tạo, cập nhật và theo dõi các chương trình khuyến mãi",
                AutoSize = true,
                Font = new Font("Segoe UI", 10),
                ForeColor = AppColors.TextMuted,
                Location = new Point(2, 54)
            });
            root.Controls.Add(header, 0, 0);

            var toolbar = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                WrapContents = true,
                Margin = Padding.Empty,
                Padding = new Padding(0, 5, 0, 5)
            };
            _search.Width = 340;
            _search.Font = new Font("Segoe UI", 10);
            _search.Margin = new Padding(0, 3, 0, 3);
            toolbar.Controls.Add(_search);
            _includeDeleted.Text = "Hiện mã đã xóa";
            _includeDeleted.AutoSize = true;
            _includeDeleted.Font = new Font("Segoe UI", 9);
            _includeDeleted.Margin = new Padding(12, 9, 8, 0);
            toolbar.Controls.Add(_includeDeleted);
            Button reload = ButtonOf("Tải lại");
            reload.Dock = DockStyle.None;
            reload.Size = new Size(100, 34);
            reload.Click += (sender, args) => Reload();
            toolbar.Controls.Add(reload);
            Button clear = ButtonOf("+ Tạo mã mới", true);
            clear.Dock = DockStyle.None;
            clear.Size = new Size(150, 34);
            clear.Click += (sender, args) => OpenEditor(null);
            toolbar.Controls.Add(clear);
            root.Controls.Add(toolbar, 0, 1);

            var gridPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppColors.White,
                Margin = new Padding(0, 4, 0, 10),
                Padding = new Padding(1)
            };
            _grid.Dock = DockStyle.Fill;
            _grid.ReadOnly = true;
            _grid.MultiSelect = false;
            _grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _grid.AutoGenerateColumns = false;
            _grid.AllowUserToAddRows = false;
            _grid.AllowUserToDeleteRows = false;
            _grid.RowHeadersVisible = false;
            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _grid.BackgroundColor = AppColors.White;
            _grid.BorderStyle = BorderStyle.None;
            _grid.GridColor = AppColors.TableGrid;
            _grid.ColumnHeadersHeight = 40;
            _grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            _grid.EnableHeadersVisualStyles = false;
            _grid.ColumnHeadersDefaultCellStyle.BackColor = AppColors.TableHeaderBg;
            _grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            _grid.DefaultCellStyle.BackColor = AppColors.White;
            _grid.DefaultCellStyle.ForeColor = AppColors.TableRowText;
            _grid.DefaultCellStyle.SelectionBackColor = AppColors.AccentSelectionBg;
            _grid.DefaultCellStyle.SelectionForeColor = AppColors.TextPrimary;
            _grid.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            _grid.AlternatingRowsDefaultCellStyle.BackColor = AppColors.TableRowOdd;
            _grid.RowTemplate.Height = 38;
            _grid.Margin = Padding.Empty;
            _grid.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PromotionId", Name = "PromotionId", Visible = false });
            AddColumn("Code", "Mã giảm giá", 85);
            AddColumn("Name", "Tên chương trình", 145);
            AddColumn("Discount", "Mức giảm", 75);
            AddColumn("Period", "Thời hạn áp dụng", 120);
            AddColumn("Usage", "Lượt sử dụng", 75);
            AddColumn("IsActive", "Trạng thái", 70);
            _grid.Columns.Add(new DataGridViewImageColumn
            {
                DataPropertyName = "Actions",
                Name = "Actions",
                HeaderText = "Thao tác",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 120,
                ImageLayout = DataGridViewImageCellLayout.Normal,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            _grid.SelectionChanged += (sender, args) => LoadSelected();
            _grid.CellDoubleClick += (sender, args) =>
            {
                if (args.RowIndex >= 0)
                {
                    _grid.CurrentCell = _grid.Rows[args.RowIndex].Cells["Code"];
                    ViewSelected();
                }
            };
            _grid.CellMouseClick += Grid_CellMouseClick;
            gridPanel.Controls.Add(_grid);
            root.Controls.Add(gridPanel, 0, 2);
            _pager.PageChanged += (sender, args) => Reload();
            root.Controls.Add(_pager, 0, 3);
            _search.TextChanged += (sender, args) => Reload(true);
            _includeDeleted.CheckedChanged += (sender, args) => Reload(true);
        }

        private void AddColumn(string property, string header, int width)
        {
            _grid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = property,
                Name = property,
                HeaderText = header,
                FillWeight = width,
                MinimumWidth = 70,
                ReadOnly = true
            });
        }

        private static Button ButtonOf(string text, bool primary = false)
        {
            var button = new Button
            {
                Text = text,
                Dock = DockStyle.Fill,
                Height = 34,
                Margin = new Padding(4, 3, 4, 3),
                Font = new Font("Segoe UI", 9, primary ? FontStyle.Bold : FontStyle.Regular),
                FlatStyle = FlatStyle.Flat,
                BackColor = primary ? AppColors.Accent : AppColors.CancelBg,
                ForeColor = primary ? Color.White : AppColors.TextPrimary,
                UseVisualStyleBackColor = false
            };
            button.FlatAppearance.BorderColor = primary ? AppColors.Accent : AppColors.Border;
            button.FlatAppearance.MouseOverBackColor = primary ? AppColors.AccentHover : AppColors.CancelHover;
            return button;
        }

        private static void AddField(TableLayoutPanel panel, string label, Control field, int column, int row)
        {
            var wrap = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(4, 2, 8, 2),
                BackColor = AppColors.White
            };
            wrap.RowStyles.Add(new RowStyle(SizeType.Absolute, 19));
            wrap.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            wrap.Controls.Add(new Label
            {
                Text = label,
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = AppColors.TextMuted,
                Margin = Padding.Empty
            }, 0, 0);
            field.Dock = DockStyle.Fill;
            field.Font = new Font("Segoe UI", 9);
            field.Margin = Padding.Empty;
            if (field is TextBox)
            {
                field.BackColor = AppColors.White;
                field.ForeColor = AppColors.TextPrimary;
            }
            wrap.Controls.Add(field, 0, 1);
            panel.Controls.Add(wrap, column, row);
            panel.SetColumnSpan(wrap, 2);
        }

        private void Reload(bool resetPage = false)
        {
            if (resetPage)
                _pager.ResetPage();
            try
            {
                PromotionPage page = _service.GetPage(
                    _search.Text.Trim(),
                    _includeDeleted.Checked,
                    _pager.PageIndex,
                    _pager.PageSize);
                if (_pager.SetTotalCount(page.TotalCount))
                {
                    Reload();
                    return;
                }

                _currentRows.Clear();
                _currentRows.AddRange(page.Rows);
                var rows = _currentRows.Select(ToRow).ToList();
                _grid.DataSource = rows;
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        private static object ToRow(PromotionDto item) => new
        {
            item.PromotionId, item.Code, item.Name,
            Discount = item.DiscountType == "PERCENT" ? item.DiscountValue.ToString("0") + "%" : item.DiscountValue.ToString("N0") + "đ",
            Period = item.StartDate.ToString("dd/MM/yyyy") + " - " + item.EndDate.ToString("dd/MM/yyyy"),
            Usage = item.UsageLimit.HasValue ? item.UsedCount + "/" + item.UsageLimit.Value : item.UsedCount + "/∞",
            IsActive = item.IsDeleted ? "Đã xóa" : (item.IsActive ? "Có" : "Không"),
            Actions = IconBitmapCache.GetActionStrip(IconChar.Trash, AppColors.Error)
        };

        private void LoadSelected()
        {
            int rowIndex = _grid.CurrentRow == null ? -1 : _grid.CurrentRow.Index;
            _selectedId = rowIndex >= 0 && rowIndex < _currentRows.Count
                ? _currentRows[rowIndex].PromotionId
                : 0;
        }

        private void Grid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.RowIndex < 0 ||
                e.RowIndex >= _currentRows.Count || e.ColumnIndex < 0 ||
                _grid.Columns[e.ColumnIndex].Name != "Actions")
                return;

            _grid.CurrentCell = _grid.Rows[e.RowIndex].Cells["Code"];
            const int iconSize = 24;
            const int gap = 8;
            int stripWidth = iconSize * 3 + gap * 2;
            int offset = e.X - (_grid.Columns[e.ColumnIndex].Width - stripWidth) / 2;
            int actionIndex = offset / (iconSize + gap);
            if (offset < 0 || actionIndex > 2 || offset % (iconSize + gap) >= iconSize)
                return;

            if (actionIndex == 0)
                ViewSelected();
            else if (actionIndex == 1)
                EditSelected();
            else
                Delete();
        }

        private void ViewSelected()
        {
            if (_selectedId <= 0)
            {
                ShowError("Chọn mã giảm giá cần xem.");
                return;
            }

            PromotionDto promotion = _service.GetById(_selectedId);
            if (promotion == null)
            {
                ShowError("Không tìm thấy mã giảm giá đã chọn.");
                return;
            }
            ShowDetails(promotion);
        }

        private void ShowDetails(PromotionDto promotion)
        {
            var dialog = new Form
            {
                Text = "Chi tiết mã giảm giá",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowInTaskbar = false,
                ClientSize = new Size(620, 480),
                BackColor = AppColors.PageBg,
                Font = new Font("Segoe UI", 9)
            };
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24),
                BackColor = AppColors.White,
                ColumnCount = 2,
                RowCount = 14
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 62));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
            var title = new Label
            {
                Text = "Chi tiết mã giảm giá",
                AutoSize = true,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = AppColors.TextTitle,
                Margin = new Padding(0, 0, 0, 8)
            };
            layout.Controls.Add(title, 0, 0);
            layout.SetColumnSpan(title, 2);

            string discount = promotion.DiscountType == "PERCENT"
                ? promotion.DiscountValue.ToString("0") + "%"
                : promotion.DiscountValue.ToString("N0") + " đ";
            string[] labels =
            {
                "Mã giảm giá", "Tên chương trình", "Loại giảm", "Giá trị giảm",
                "Giảm tối đa", "Đơn tối thiểu", "Ngày bắt đầu", "Ngày kết thúc",
                "Giới hạn lượt", "Đã sử dụng", "Trạng thái", "Hiển thị banner"
            };
            string[] values =
            {
                promotion.Code, promotion.Name, promotion.DiscountType, discount,
                promotion.MaxDiscountAmount.HasValue ? promotion.MaxDiscountAmount.Value.ToString("N0") + " đ" : "Không giới hạn",
                promotion.MinOrderAmount.ToString("N0") + " đ",
                promotion.StartDate.ToString("dd/MM/yyyy"), promotion.EndDate.ToString("dd/MM/yyyy"),
                promotion.UsageLimit.HasValue ? promotion.UsageLimit.Value.ToString() : "Không giới hạn",
                promotion.UsedCount.ToString(),
                promotion.IsDeleted ? "Đã xóa" : promotion.IsActive ? "Đang áp dụng" : "Đã tắt",
                promotion.ShowOnBanner ? "Có" : "Không"
            };
            for (int i = 0; i < labels.Length; i++)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 7.5F));
                layout.Controls.Add(new Label
                {
                    Text = labels[i],
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    ForeColor = AppColors.TextMuted,
                    Font = new Font("Segoe UI", 9)
                }, 0, i + 1);
                layout.Controls.Add(new Label
                {
                    Text = values[i],
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    ForeColor = AppColors.TextPrimary,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                }, 1, i + 1);
            }
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            var close = ButtonOf("Đóng");
            close.Dock = DockStyle.None;
            close.Size = new Size(100, 34);
            close.Anchor = AnchorStyles.Right;
            close.Click += (sender, args) => dialog.Close();
            layout.Controls.Add(close, 1, 13);
            dialog.Controls.Add(layout);
            Form owner = FindForm();
            if (owner == null) dialog.ShowDialog();
            else dialog.ShowDialog(owner);
            dialog.Dispose();
        }

        private void EditSelected()
        {
            if (_selectedId <= 0)
            {
                ShowError("Chọn mã giảm giá cần sửa.");
                return;
            }

            PromotionDto dto = _service.GetById(_selectedId);
            if (dto == null)
            {
                ShowError("Không tìm thấy mã giảm giá đã chọn.");
                return;
            }
            OpenEditor(dto);
        }

        private void OpenEditor(PromotionDto promotion)
        {
            _selectedId = promotion == null ? 0 : promotion.PromotionId;
            _code = new TextBox();
            _name = new TextBox();
            _type = new ComboBox();
            _value = new TextBox();
            _max = new TextBox();
            _minimum = new TextBox();
            _start = new DateTimePicker();
            _end = new DateTimePicker();
            _limit = new TextBox();
            _active = new CheckBox();
            _banner = new CheckBox();

            _type.Items.AddRange(new object[] { "PERCENT", "AMOUNT" });
            _type.DropDownStyle = ComboBoxStyle.DropDownList;
            _type.SelectedIndex = 0;
            _start.Format = _end.Format = DateTimePickerFormat.Short;
            _start.Value = DateTime.Today;
            _end.Value = DateTime.Today.AddMonths(1);
            _minimum.Text = "0";
            _active.Text = "Đang áp dụng";
            _active.AutoSize = true;
            _banner.Text = "Hiện trên banner";
            _banner.AutoSize = true;

            if (promotion != null)
            {
                _code.Text = promotion.Code;
                _name.Text = promotion.Name;
                _type.SelectedItem = promotion.DiscountType;
                _value.Text = promotion.DiscountValue.ToString("0", CultureInfo.InvariantCulture);
                _max.Text = promotion.MaxDiscountAmount?.ToString("0", CultureInfo.InvariantCulture) ?? string.Empty;
                _minimum.Text = promotion.MinOrderAmount.ToString("0", CultureInfo.InvariantCulture);
                _start.Value = promotion.StartDate;
                _end.Value = promotion.EndDate;
                _limit.Text = promotion.UsageLimit?.ToString() ?? string.Empty;
                _active.Checked = promotion.IsActive;
                _banner.Checked = promotion.ShowOnBanner;
            }

            Form dialog = BuildEditorDialog(promotion == null);
            _editorDialog = dialog;
            Form owner = FindForm();
            if (owner == null)
                dialog.ShowDialog();
            else
                dialog.ShowDialog(owner);
            _editorDialog = null;
            dialog.Dispose();
        }

        private Form BuildEditorDialog(bool isNew)
        {
            var dialog = new Form
            {
                Text = isNew ? "Tạo mã giảm giá" : "Cập nhật mã giảm giá",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowInTaskbar = false,
                ClientSize = new Size(850, 540),
                BackColor = AppColors.PageBg,
                Font = new Font("Segoe UI", 9)
            };
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppColors.White,
                Padding = new Padding(20),
                Margin = new Padding(20)
            };
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 7,
                BackColor = AppColors.White
            };
            for (int i = 0; i < 4; i++)
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48));

            var titlePanel = new Panel { Dock = DockStyle.Fill, Margin = Padding.Empty };
            titlePanel.Controls.Add(new Label
            {
                Text = isNew ? "Tạo mã giảm giá mới" : "Cập nhật mã giảm giá",
                AutoSize = true,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = AppColors.TextTitle,
                Location = new Point(0, 0)
            });
            titlePanel.Controls.Add(new Label
            {
                Text = "Nhập thông tin chương trình khuyến mãi",
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = AppColors.TextMuted,
                Location = new Point(2, 31)
            });
            layout.Controls.Add(titlePanel, 0, 0);
            layout.SetColumnSpan(titlePanel, 4);

            AddField(layout, "Mã giảm giá", _code, 0, 1);
            AddField(layout, "Tên chương trình", _name, 2, 1);
            AddField(layout, "Loại giảm", _type, 0, 2);
            AddField(layout, "Giá trị giảm", _value, 2, 2);
            AddField(layout, "Giảm tối đa (đ)", _max, 0, 3);
            AddField(layout, "Đơn tối thiểu (đ)", _minimum, 2, 3);
            AddField(layout, "Ngày bắt đầu", _start, 0, 4);
            AddField(layout, "Ngày kết thúc", _end, 2, 4);
            AddField(layout, "Giới hạn lượt", _limit, 0, 5);
            _active.Margin = new Padding(8, 8, 0, 0);
            layout.Controls.Add(_active, 1, 5);
            _banner.Margin = new Padding(8, 8, 0, 0);
            layout.Controls.Add(_banner, 2, 5);

            var buttons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                Margin = Padding.Empty
            };
            Button save = ButtonOf(isNew ? "Tạo mã" : "Lưu thay đổi", true);
            save.Dock = DockStyle.None;
            save.Size = new Size(145, 36);
            save.Click += (sender, args) => Save();
            Button cancel = ButtonOf("Hủy");
            cancel.Dock = DockStyle.None;
            cancel.Size = new Size(100, 36);
            cancel.Click += (sender, args) => dialog.Close();
            buttons.Controls.Add(save);
            buttons.Controls.Add(cancel);
            layout.Controls.Add(buttons, 0, 6);
            layout.SetColumnSpan(buttons, 4);
            card.Controls.Add(layout);
            dialog.Controls.Add(card);
            dialog.AcceptButton = save;
            dialog.CancelButton = cancel;
            return dialog;
        }

        private void Save()
        {
            decimal value, max, minimum;
            int limit;
            if (!decimal.TryParse(_value.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out value)) { ShowError("Giá trị giảm không hợp lệ."); return; }
            if (!TryOptionalDecimal(_max.Text, out max) || !decimal.TryParse(_minimum.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out minimum) || !TryOptionalInt(_limit.Text, out limit)) { ShowError("Thông tin tiền hoặc giới hạn lượt không hợp lệ."); return; }
            var dto = new PromotionEditDto { PromotionId = _selectedId, Code = _code.Text, Name = _name.Text, DiscountType = Convert.ToString(_type.SelectedItem), DiscountValue = value, MaxDiscountAmount = string.IsNullOrWhiteSpace(_max.Text) ? (decimal?)null : max, MinOrderAmount = minimum, StartDate = _start.Value.Date, EndDate = _end.Value.Date, UsageLimit = string.IsNullOrWhiteSpace(_limit.Text) ? (int?)null : limit, IsActive = _active.Checked, ShowOnBanner = _banner.Checked };
            PromotionSaveResult result = _service.Save(dto); if (!result.IsSuccess) { ShowError(result.Message); return; }
            ShowInfo(result.Message);
            Reload();
            _editorDialog?.Close();
        }

        private void Toggle()
        {
            if (_selectedId <= 0) { ShowError("Chọn mã cần bật hoặc tắt."); return; }
            PromotionDto dto = _service.GetById(_selectedId); if (dto == null) return;
            PromotionSaveResult result = _service.SetActive(_selectedId, !dto.IsActive); if (!result.IsSuccess) ShowError(result.Message); else { ShowInfo(result.Message); Reload(); }
        }

        private void Delete()
        {
            if (_selectedId <= 0) { ShowError("Chọn mã cần xóa."); return; }
            DialogResult confirm = MessageBox.Show(
                this,
                "Bạn có chắc muốn xóa mã giảm giá đã chọn?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes)
                return;
            PromotionSaveResult result = _service.Delete(_selectedId);
            if (!result.IsSuccess) { ShowError(result.Message); return; }
            ShowInfo(result.Message);
            _selectedId = 0;
            Reload();
        }

        private static bool TryOptionalDecimal(string text, out decimal value)
        {
            if (string.IsNullOrWhiteSpace(text)) { value = 0; return true; }
            return decimal.TryParse(text, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
        }

        private static bool TryOptionalInt(string text, out int value)
        {
            if (string.IsNullOrWhiteSpace(text)) { value = 0; return true; }
            return int.TryParse(text, out value);
        }
        private void ShowInfo(string message) => AppToast.Info(this, message);
        private void ShowError(string message) => AppToast.Error(this, "Mã giảm giá", message);
    }
}
