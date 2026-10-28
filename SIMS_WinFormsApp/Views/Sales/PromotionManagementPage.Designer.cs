using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.Views.Sales
{
    partial class PromotionManagementPage
    {
        private IContainer components = null;
        private TableLayoutPanel _root;
        private Panel _header;
        private Label _title;
        private Label _subtitle;
        private FlowLayoutPanel _toolbar;
        private Button _reloadButton;
        private Button _newPromotionButton;
        private Panel _gridPanel;
        private TextBox _search;
        private CheckBox _includeDeleted;
        private DataGridView _grid;
        private UserManagementPager _pager;
        private DataGridViewTextBoxColumn _promotionIdColumn;
        private DataGridViewTextBoxColumn _codeColumn;
        private DataGridViewTextBoxColumn _nameColumn;
        private DataGridViewTextBoxColumn _discountColumn;
        private DataGridViewTextBoxColumn _periodColumn;
        private DataGridViewTextBoxColumn _usageColumn;
        private DataGridViewTextBoxColumn _activeColumn;
        private DataGridViewImageColumn _actionsColumn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && _themeSubscribed)
            {
                ThemeManager.Instance.ThemeChanged -= OnThemeChanged;
                _themeSubscribed = false;
            }
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this._root = new System.Windows.Forms.TableLayoutPanel();
            this._header = new System.Windows.Forms.Panel();
            this._title = new System.Windows.Forms.Label();
            this._subtitle = new System.Windows.Forms.Label();
            this._toolbar = new System.Windows.Forms.FlowLayoutPanel();
            this._search = new System.Windows.Forms.TextBox();
            this._includeDeleted = new System.Windows.Forms.CheckBox();
            this._reloadButton = new System.Windows.Forms.Button();
            this._newPromotionButton = new System.Windows.Forms.Button();
            this._gridPanel = new System.Windows.Forms.Panel();
            this._grid = new System.Windows.Forms.DataGridView();
            this._pager = new SIMS_WinFormsApp.UI.Controls.UserManagementPager();
            this._root.SuspendLayout();
            this._header.SuspendLayout();
            this._toolbar.SuspendLayout();
            this._gridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._grid)).BeginInit();
            this.SuspendLayout();
            // 
            // _root
            // 
            this._root.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this._root.ColumnCount = 1;
            this._root.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.Controls.Add(this._header, 0, 0);
            this._root.Controls.Add(this._toolbar, 0, 1);
            this._root.Controls.Add(this._gridPanel, 0, 2);
            this._root.Controls.Add(this._pager, 0, 3);
            this._root.Dock = System.Windows.Forms.DockStyle.Fill;
            this._root.Location = new System.Drawing.Point(0, 0);
            this._root.Name = "_root";
            this._root.Padding = new System.Windows.Forms.Padding(24);
            this._root.RowCount = 4;
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._root.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this._root.Size = new System.Drawing.Size(1294, 857);
            this._root.TabIndex = 0;
            // 
            // _header
            // 
            this._header.BackColor = System.Drawing.Color.Transparent;
            this._header.Controls.Add(this._title);
            this._header.Controls.Add(this._subtitle);
            this._header.Dock = System.Windows.Forms.DockStyle.Fill;
            this._header.Location = new System.Drawing.Point(24, 24);
            this._header.Margin = new System.Windows.Forms.Padding(0);
            this._header.Name = "_header";
            this._header.Size = new System.Drawing.Size(1246, 92);
            this._header.TabIndex = 0;
            // 
            // _title
            // 
            this._title.AutoSize = true;
            this._title.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this._title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this._title.Location = new System.Drawing.Point(0, 0);
            this._title.Name = "_title";
            this._title.Size = new System.Drawing.Size(494, 65);
            this._title.TabIndex = 0;
            this._title.Text = "Quản lý mã giảm giá";
            // 
            // _subtitle
            // 
            this._subtitle.AutoSize = true;
            this._subtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._subtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this._subtitle.Location = new System.Drawing.Point(2, 54);
            this._subtitle.Name = "_subtitle";
            this._subtitle.Size = new System.Drawing.Size(488, 28);
            this._subtitle.TabIndex = 1;
            this._subtitle.Text = "Tạo, cập nhật và theo dõi các chương trình khuyến mãi";
            // 
            // _toolbar
            // 
            this._toolbar.AutoSize = true;
            this._toolbar.Controls.Add(this._search);
            this._toolbar.Controls.Add(this._includeDeleted);
            this._toolbar.Controls.Add(this._reloadButton);
            this._toolbar.Controls.Add(this._newPromotionButton);
            this._toolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this._toolbar.Location = new System.Drawing.Point(24, 116);
            this._toolbar.Margin = new System.Windows.Forms.Padding(0);
            this._toolbar.Name = "_toolbar";
            this._toolbar.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._toolbar.Size = new System.Drawing.Size(1246, 48);
            this._toolbar.TabIndex = 1;
            // 
            // _search
            // 
            this._search.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._search.Location = new System.Drawing.Point(0, 8);
            this._search.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this._search.Name = "_search";
            this._search.Size = new System.Drawing.Size(340, 34);
            this._search.TabIndex = 0;
            // 
            // _includeDeleted
            // 
            this._includeDeleted.AutoSize = true;
            this._includeDeleted.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._includeDeleted.Location = new System.Drawing.Point(352, 14);
            this._includeDeleted.Margin = new System.Windows.Forms.Padding(12, 9, 8, 0);
            this._includeDeleted.Name = "_includeDeleted";
            this._includeDeleted.Size = new System.Drawing.Size(162, 29);
            this._includeDeleted.TabIndex = 1;
            this._includeDeleted.Text = "Hiện mã đã xóa";
            // 
            // _reloadButton
            // 
            this._reloadButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._reloadButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._reloadButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this._reloadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._reloadButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._reloadButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this._reloadButton.Location = new System.Drawing.Point(526, 8);
            this._reloadButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._reloadButton.Name = "_reloadButton";
            this._reloadButton.Size = new System.Drawing.Size(100, 34);
            this._reloadButton.TabIndex = 2;
            this._reloadButton.Text = "Tải lại";
            this._reloadButton.UseVisualStyleBackColor = false;
            // 
            // _newPromotionButton
            // 
            this._newPromotionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._newPromotionButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this._newPromotionButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this._newPromotionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._newPromotionButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._newPromotionButton.ForeColor = System.Drawing.Color.White;
            this._newPromotionButton.Location = new System.Drawing.Point(634, 8);
            this._newPromotionButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this._newPromotionButton.Name = "_newPromotionButton";
            this._newPromotionButton.Size = new System.Drawing.Size(150, 34);
            this._newPromotionButton.TabIndex = 3;
            this._newPromotionButton.Text = "+ Tạo mã mới";
            this._newPromotionButton.UseVisualStyleBackColor = false;
            // 
            // _gridPanel
            // 
            this._gridPanel.BackColor = System.Drawing.Color.White;
            this._gridPanel.Controls.Add(this._grid);
            this._gridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridPanel.Location = new System.Drawing.Point(24, 168);
            this._gridPanel.Margin = new System.Windows.Forms.Padding(0, 4, 0, 10);
            this._gridPanel.Name = "_gridPanel";
            this._gridPanel.Padding = new System.Windows.Forms.Padding(1);
            this._gridPanel.Size = new System.Drawing.Size(1246, 611);
            this._gridPanel.TabIndex = 2;
            // 
            // _grid
            // 
            this._grid.AllowUserToAddRows = false;
            this._grid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this._grid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this._grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._grid.BackgroundColor = System.Drawing.Color.White;
            this._grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this._grid.ColumnHeadersHeight = 40;
            this._grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._grid.DefaultCellStyle = dataGridViewCellStyle3;
            this._grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grid.EnableHeadersVisualStyles = false;
            this._grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this._grid.Location = new System.Drawing.Point(1, 1);
            this._grid.Margin = new System.Windows.Forms.Padding(0);
            this._grid.MultiSelect = false;
            this._grid.Name = "_grid";
            this._grid.ReadOnly = true;
            this._grid.RowHeadersVisible = false;
            this._grid.RowHeadersWidth = 62;
            this._grid.RowTemplate.Height = 38;
            this._grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._grid.Size = new System.Drawing.Size(1244, 609);
            this._grid.TabIndex = 0;
            // 
            // _pager
            // 
            this._pager.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pager.Location = new System.Drawing.Point(27, 792);
            this._pager.MinimumSize = new System.Drawing.Size(0, 42);
            this._pager.Name = "_pager";
            this._pager.Size = new System.Drawing.Size(1240, 42);
            this._pager.TabIndex = 3;
            // 
            // PromotionManagementPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Controls.Add(this._root);
            this.Name = "PromotionManagementPage";
            this.Size = new System.Drawing.Size(1294, 857);
            this._root.ResumeLayout(false);
            this._root.PerformLayout();
            this._header.ResumeLayout(false);
            this._header.PerformLayout();
            this._toolbar.ResumeLayout(false);
            this._toolbar.PerformLayout();
            this._gridPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._grid)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
