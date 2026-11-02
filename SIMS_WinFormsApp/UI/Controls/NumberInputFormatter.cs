using System;
using System.Text;
using System.Windows.Forms;

namespace SIMS_WinFormsApp.UI.Controls
{
    public sealed class NumberInputFormatter
    {
        public static void Attach(TextBox textBox)
        {
            Attach(textBox, false);
        }

        public static void AttachDecimal(TextBox textBox)
        {
            Attach(textBox, true);
        }

        public static void Detach(TextBox textBox)
        {
            if (textBox == null) throw new ArgumentNullException(nameof(textBox));
            if (!(textBox.Tag is NumberInputFormatter formatter)) return;

            textBox.KeyPress -= formatter.OnKeyPress;
            textBox.TextChanged -= formatter.OnTextChanged;
            textBox.MaxLength = formatter._originalMaxLength;
            textBox.Tag = null;
        }

        private static void Attach(TextBox textBox, bool allowDecimal)
        {
            if (textBox == null) throw new ArgumentNullException(nameof(textBox));
            if (textBox.Tag is NumberInputFormatter) return;

            int maxDigits = textBox.MaxLength;
            var formatter = new NumberInputFormatter(maxDigits, allowDecimal);
            textBox.Tag = formatter;
            textBox.MaxLength = 0;
            textBox.KeyPress += formatter.OnKeyPress;
            textBox.TextChanged += formatter.OnTextChanged;
            formatter.FormatText(textBox);
        }

        public static int GetMaximumDigits(TextBox textBox)
        {
            if (textBox == null) throw new ArgumentNullException(nameof(textBox));
            return textBox.Tag is NumberInputFormatter formatter
                ? formatter._maximumDigits
                : textBox.MaxLength;
        }

        public static void SetMaximumDigits(TextBox textBox, int maximumDigits)
        {
            if (textBox == null) throw new ArgumentNullException(nameof(textBox));
            if (textBox.Tag is NumberInputFormatter formatter)
            {
                formatter._maximumDigits = Math.Max(0, maximumDigits);
                formatter.FormatText(textBox);
                return;
            }

            textBox.MaxLength = Math.Max(0, maximumDigits);
        }

        public static string DigitsOnly(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            var digits = new StringBuilder(value.Length);
            foreach (char character in value)
                if (char.IsDigit(character))
                    digits.Append(character);
            return digits.ToString();
        }

        public static string FormatInteger(string value)
        {
            string digits = DigitsOnly(value);
            if (digits.Length <= 3) return digits;

            var formatted = new StringBuilder(digits.Length + (digits.Length - 1) / 3);
            int firstGroupLength = digits.Length % 3;
            if (firstGroupLength == 0) firstGroupLength = 3;
            formatted.Append(digits, 0, firstGroupLength);
            for (int i = firstGroupLength; i < digits.Length; i += 3)
            {
                formatted.Append(',');
                formatted.Append(digits, i, 3);
            }
            return formatted.ToString();
        }

        private bool _formatting;
        private int _maximumDigits;
        private readonly bool _allowDecimal;
        private readonly int _originalMaxLength;

        private NumberInputFormatter(int maximumDigits, bool allowDecimal)
        {
            _maximumDigits = Math.Max(0, maximumDigits);
            _allowDecimal = allowDecimal;
            _originalMaxLength = Math.Max(0, maximumDigits);
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            if (_allowDecimal && e.KeyChar == '.')
            {
                var decimalTextBox = (TextBox)sender;
                string textWithoutSelection = decimalTextBox.Text.Remove(
                    decimalTextBox.SelectionStart, decimalTextBox.SelectionLength);
                if (textWithoutSelection.IndexOf('.') >= 0)
                    e.Handled = true;
                return;
            }

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (_maximumDigits <= 0 || !(sender is TextBox textBox)) return;
            int selectedDigits = CountDigits(textBox.Text.Substring(
                textBox.SelectionStart, textBox.SelectionLength), textBox.SelectionLength);
            int currentDigits = DigitsOnly(textBox.Text).Length;
            if (currentDigits - selectedDigits >= _maximumDigits)
                e.Handled = true;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (_formatting || !(sender is TextBox textBox)) return;
            FormatText(textBox);
        }

        private void FormatText(TextBox textBox)
        {
            if (_formatting) return;

            string original = textBox.Text;
            int digitsBeforeCaret = CountDigits(original, Math.Min(textBox.SelectionStart, original.Length));
            bool decimalBeforeCaret = _allowDecimal &&
                original.IndexOf(".", StringComparison.Ordinal) >= 0 &&
                original.IndexOf(".", StringComparison.Ordinal) < textBox.SelectionStart;
            string digits = DigitsOnly(original);
            if (_maximumDigits > 0 && digits.Length > _maximumDigits)
                digits = digits.Substring(0, _maximumDigits);
            string formatted = _allowDecimal
                ? FormatDecimal(original, _maximumDigits)
                : FormatInteger(digits);
            if (string.Equals(original, formatted, StringComparison.Ordinal)) return;

            _formatting = true;
            try
            {
                textBox.Text = formatted;
                int caret = PositionAfterDigits(formatted, digitsBeforeCaret);
                if (decimalBeforeCaret)
                {
                    int decimalPosition = formatted.IndexOf(".", StringComparison.Ordinal);
                    if (decimalPosition >= 0)
                        caret = Math.Max(caret, decimalPosition + 1);
                }
                textBox.SelectionStart = Math.Min(caret, formatted.Length);
                textBox.SelectionLength = 0;
            }
            finally
            {
                _formatting = false;
            }
        }

        private static string FormatDecimal(string value, int maximumDigits)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            int decimalIndex = value.IndexOf(".", StringComparison.Ordinal);
            var integerDigits = new StringBuilder(value.Length);
            var fractionalDigits = new StringBuilder(value.Length);
            int digitCount = 0;
            for (int index = 0; index < value.Length; index++)
            {
                char character = value[index];
                if (!char.IsDigit(character)) continue;
                if (maximumDigits > 0 && digitCount >= maximumDigits) break;
                digitCount++;
                if (decimalIndex >= 0 && index > decimalIndex)
                    fractionalDigits.Append(character);
                else
                    integerDigits.Append(character);
            }

            string integer = FormatInteger(integerDigits.ToString());
            return decimalIndex >= 0
                ? integer + "." + fractionalDigits
                : integer;
        }

        private static int CountDigits(string value, int end)
        {
            int count = 0;
            int limit = Math.Min(end, value.Length);
            for (int i = 0; i < limit; i++)
                if (char.IsDigit(value[i]))
                    count++;
            return count;
        }

        private static int PositionAfterDigits(string value, int digitCount)
        {
            if (digitCount <= 0) return 0;

            int count = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsDigit(value[i]) && ++count == digitCount)
                    return i + 1;
            }
            return value.Length;
        }
    }
}
