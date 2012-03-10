using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FacetedWorlds.Ledger.ViewModels;
using UpdateControls.XAML;

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
                var viewModel = ForView.Unwrap<BookViewModel>(DataContext);
                viewModel.EnterRow();
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}
