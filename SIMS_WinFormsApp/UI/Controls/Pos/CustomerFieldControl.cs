using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using SIMS_WinFormsApp.UI.Controls;
using SIMS_WinFormsApp.UI.Theme;

namespace SIMS_WinFormsApp.UI.Controls.Pos
{
    internal sealed class CustomerFieldControl : IconRoundedTextBox
    {
        private const int TrailingArea = 66;

        private readonly IconPictureBox _pickIcon;
        private readonly IconPictureBox _searchIcon;

        public event EventHandler PickRequested;
        public event EventHandler SearchRequested;

        public CustomerFieldControl()
        {
            Icon = IconChar.User;
            Height = 42;

            _pickIcon = new IconPictureBox
            {
                IconChar = IconChar.AddressBook,
                IconColor = AppColors.TextMutedAlt,
                IconSize = 15,
                Size = new Size(24, 24),
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent
            };
            _pickIcon.Click += (s, e) => PickRequested?.Invoke(this, EventArgs.Empty);
            Controls.Add(_pickIcon);

            _searchIcon = new IconPictureBox
            {
                IconChar = IconChar.MagnifyingGlass,
                IconColor = AppColors.TextMutedAlt,
                IconSize = 15,
                Size = new Size(24, 24),
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent
            };
            _searchIcon.Click += (s, e) => RaiseSearch();
            Controls.Add(_searchIcon);

            InputControl.KeyDown += (s, e) =>
            {
                if (e.KeyCode != Keys.Enter) return;
                e.SuppressKeyPress = true;
                RaiseSearch();
            };

            Resize += (s, e) => PositionTrailingIcons();
            PositionTrailingIcons();
        }

        protected override int TrailingWidth => TrailingArea;

        private void RaiseSearch()
        {
            if (!string.IsNullOrWhiteSpace(Text)) SearchRequested?.Invoke(this, EventArgs.Empty);
        }

        private void PositionTrailingIcons()
        {
            if (_searchIcon == null || _pickIcon == null) return;
            _searchIcon.Location = new Point(Width - 14 - _searchIcon.Width, (Height - _searchIcon.Height) / 2);
            _pickIcon.Location = new Point(_searchIcon.Left - 4 - _pickIcon.Width, (Height - _pickIcon.Height) / 2);
        }
    }
}