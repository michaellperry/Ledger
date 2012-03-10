using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FacetedWorlds.Ledger.ViewModels;
using UpdateControls.XAML;
using System.Text;
using System.Linq;

namespace FacetedWorlds.Ledger.Views
{
    public partial class LedgerControl : UserControl
    {
        public LedgerControl()
        {
            InitializeComponent();
        }

        private void NewEntry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox textBox = (TextBox)sender;
                textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                HandleEnter(e.Key);
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void AccountComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (HandleEnter(e.Key))
                return;

            char c;
            if (KeyToChar(e.Key, out c))
            {
                var viewModel = ForView.Unwrap<BookViewModel>(DataContext);
                if (viewModel != null)
                {
                    var options = viewModel.OtherAccountOptions
                        .Where(option => option.Name.ToLower()[0] == c)
                        .ToList();
                    if (options.Count == 1)
                        viewModel.OtherAccount = options[0];
                    else if (options.Count > 1)
                    {
                        int index = options.IndexOf(viewModel.OtherAccount);
                        viewModel.OtherAccount = options[(index + 1) % options.Count];
                    }
                }
            }
        }

        private bool HandleEnter(Key key)
        {
            if (key == Key.Enter)
            {
                var viewModel = ForView.Unwrap<BookViewModel>(DataContext);
                if (viewModel != null)
                {
                    if (viewModel.EnterRow())
                        DateTextBox.Focus();
                    return true;
                }
            }
            return false;
        }

        private bool KeyToChar(Key key, out char c)
        {
            // Here's a bad idea.
            if (Key.A <= key && key <= Key.Z)
            {
                c = (char)(key - Key.A + (int)'a');
                return true;
            }
            else
            {
                c = default(char);
                return false;
            }
        }
    }
}
