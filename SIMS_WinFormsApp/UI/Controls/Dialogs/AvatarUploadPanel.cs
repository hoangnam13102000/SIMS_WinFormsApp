using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls
{
    /// <summary>
    /// Cột chọn ảnh đại diện dùng chung cho các popup thêm/sửa tài khoản (nhân viên, khách
    /// hàng...): vòng tròn xem trước ảnh (hoặc chữ cái đại diện khi chưa chọn ảnh) + nút
    /// "Chọn ảnh" + dòng gợi ý định dạng/kích thước tối đa.
    ///
    /// Chỉ đảm nhiệm việc CHỌN và XEM TRƯỚC ảnh ở phía client (đúng SRP) - việc thật sự gửi ảnh
    /// này lên server/lưu trữ là việc của tầng Service/Repository và cần được nối dây riêng khi
    /// backend hỗ trợ lưu ảnh đại diện (hiện DTO/Service quản lý người dùng chưa có trường này,
    /// nên control tự chạy độc lập, không đụng tới presenter/service hiện có).
    /// </summary>
    [ToolboxItem(false)]
    [DesignerCategory("Code")]
    public class AvatarUploadPanel : Panel
    {
        /// <summary>Giới hạn kích thước ảnh - khớp với dòng gợi ý hiển thị trên UI ("tối đa 5MB").</summary>
        public const long MaxFileSizeBytes = 5 * 1024 * 1024;

        private const int AvatarSize = 96;
        private const int SpacingAfterAvatar = 12;
        private const int SpacingAfterButton = 8;
        private const int ButtonHeight = 34;
        private const int MinHintHeight = 16;

        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".bmp" };

        private readonly Panel _avatarCircle;
        private readonly Label _lblInitial;
        private readonly PrimaryButton _btnChoose;
        private readonly Label _lblHint;

        private Image _avatarImage;
        private string _selectedFilePath;
        private Color _placeholderColor = AppColors.Accent;

        /// <summary>Phát sinh mỗi khi ảnh đại diện được chọn hoặc xoá thành công.</summary>
        public event EventHandler ImageChanged;

        public AvatarUploadPanel()
        {
            BackColor = Color.Transparent;
            Size = new Size(160, 190);

            _avatarCircle = new Panel
            {
                Size = new Size(AvatarSize, AvatarSize),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            _avatarCircle.Paint += AvatarCircle_Paint;
            _avatarCircle.Click += (s, e) => ChooseImage();
            Controls.Add(_avatarCircle);

            _lblInitial = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                Text = string.Empty,
                Font = new Font(AppFonts.Title.FontFamily, 28f, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand,
                UseMnemonic = false
            };
            _lblInitial.Click += (s, e) => ChooseImage();
            _avatarCircle.Controls.Add(_lblInitial);

            _btnChoose = new PrimaryButton
            {
                Text = "Chọn ảnh",
                IsPrimary = false,
                Height = ButtonHeight,
                CornerRadius = AppRadius.Small,
                Font = AppFonts.Small
            };
            _btnChoose.Click += (s, e) => ChooseImage();
            Controls.Add(_btnChoose);

            _lblHint = new Label
            {
                AutoSize = false,
                Text = "Tuỳ chọn · tối đa 5MB",
                Font = AppFonts.Small,
                ForeColor = AppColors.TextMuted,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.TopCenter
            };
            Controls.Add(_lblHint);

            Resize += (s, e) => LayoutChildren();
            LayoutChildren();
        }

        /// <summary>Chữ cái đại diện hiển thị khi chưa chọn ảnh (thường là ký tự đầu họ tên).</summary>
        public string Initial
        {
            get => _lblInitial.Text;
            set { _lblInitial.Text = value ?? string.Empty; _avatarCircle.Invalidate(); }
        }

        /// <summary>Màu nền vòng tròn khi hiển thị chữ cái đại diện (chưa có ảnh).</summary>
        public Color PlaceholderColor
        {
            get => _placeholderColor;
            set { _placeholderColor = value; _avatarCircle.Invalidate(); }
        }

        /// <summary>Dòng gợi ý bên dưới nút chọn ảnh (định dạng/kích thước cho phép).</summary>
        public string HintText
        {
            get => _lblHint.Text;
            set { _lblHint.Text = value ?? string.Empty; LayoutChildren(); }
        }

        /// <summary>Ảnh đại diện người dùng vừa chọn; null nếu chưa chọn ảnh nào (đang hiển thị
        /// chữ cái đại diện mặc định).</summary>
        public Image AvatarImage => _avatarImage;

        /// <summary>Đường dẫn file gốc trên máy của ảnh vừa chọn; null nếu chưa chọn ảnh.</summary>
        public string SelectedFilePath => _selectedFilePath;

        private void ChooseImage()
        {
            using (var dialog = new OpenFileDialog
            {
                Title = "Chọn ảnh đại diện",
                Filter = "Tệp ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
                CheckFileExists = true
            })
            {
                if (dialog.ShowDialog(FindForm()) != DialogResult.OK) return;

                string extension = Path.GetExtension(dialog.FileName)?.ToLowerInvariant() ?? string.Empty;
                if (Array.IndexOf(AllowedExtensions, extension) < 0)
                {
                    DialogHelper.ShowWarning(FindForm(), "Ảnh không hợp lệ",
                        "Chỉ chấp nhận các định dạng ảnh: JPG, PNG, BMP.");
                    return;
                }

                var fileInfo = new FileInfo(dialog.FileName);
                if (fileInfo.Length > MaxFileSizeBytes)
                {
                    DialogHelper.ShowWarning(FindForm(), "Ảnh quá lớn",
                        "Kích thước ảnh vượt quá giới hạn cho phép (tối đa 5MB).");
                    return;
                }

                try
                {
                    // Đọc toàn bộ ảnh vào bộ nhớ trước khi tạo Image, tránh khoá file gốc trên đĩa
                    // (Image.FromFile giữ nguyên file handle mở cho tới khi Image bị Dispose).
                    byte[] bytes = File.ReadAllBytes(dialog.FileName);
                    using (var stream = new MemoryStream(bytes))
                    {
                        using (var source = Image.FromStream(stream))
                        {
                            // Image.FromStream cần giữ stream sống suốt vòng đời ảnh;
                            // clone sang Bitmap để có thể đóng stream an toàn sau khi chọn.
                            Image loaded = new Bitmap(source);
                            SetImage(loaded, dialog.FileName);
                        }
                    }
                }
                catch (Exception)
                {
                    DialogHelper.ShowError(FindForm(), "Lỗi đọc ảnh",
                        "Không thể đọc ảnh đã chọn, vui lòng thử lại với ảnh khác.");
                }
            }
        }

        /// <summary>Gán trực tiếp 1 ảnh đại diện (ví dụ khi mở form sửa và đã có sẵn ảnh) mà
        /// không cần đi qua hộp thoại chọn file. Ảnh cũ (nếu có) sẽ được Dispose để tránh rò rỉ
        /// GDI handle.</summary>
        public void SetImage(Image image, string sourceFilePath)
        {
            if (!ReferenceEquals(_avatarImage, image)) _avatarImage?.Dispose();
            _avatarImage = image;
            _selectedFilePath = sourceFilePath;
            _lblInitial.Visible = image == null;
            _avatarCircle.Invalidate();
            ImageChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>Xoá ảnh đã chọn, quay về hiển thị chữ cái đại diện.</summary>
        public void ClearImage() => SetImage(null, null);

        private void AvatarCircle_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var ellipse = new Rectangle(0, 0, AvatarSize - 1, AvatarSize - 1);
            using (var clipPath = new GraphicsPath())
            {
                clipPath.AddEllipse(ellipse);

                if (_avatarImage != null)
                {
                    Region oldClip = g.Clip;
                    g.SetClip(clipPath, CombineMode.Replace);
                    g.DrawImage(_avatarImage, ellipse);
                    g.Clip = oldClip;
                }
                else
                {
                    using (var brush = new SolidBrush(_placeholderColor))
                        g.FillPath(brush, clipPath);
                }

                using (var pen = new Pen(AppColors.Border, 1.4f))
                    g.DrawPath(pen, clipPath);
            }
        }

        private void LayoutChildren()
        {
            if (_btnChoose == null) return;

            int centerX = Width / 2;
            _avatarCircle.Location = new Point(centerX - AvatarSize / 2, 0);

            _btnChoose.Width = Math.Min(120, Math.Max(60, Width));
            _btnChoose.Location = new Point(centerX - _btnChoose.Width / 2, _avatarCircle.Bottom + SpacingAfterAvatar);

            _lblHint.Width = Width;
           
            int hintHeight = TextRenderer.MeasureText(
                _lblHint.Text, _lblHint.Font, new Size(Math.Max(10, Width), int.MaxValue),
                TextFormatFlags.WordBreak | TextFormatFlags.NoPadding | TextFormatFlags.HorizontalCenter).Height;
            _lblHint.Height = Math.Max(MinHintHeight, hintHeight);
            _lblHint.Location = new Point(0, _btnChoose.Bottom + SpacingAfterButton);

            int preferredHeight = _lblHint.Bottom;
            if (Height != preferredHeight) Height = preferredHeight;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _avatarImage?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}