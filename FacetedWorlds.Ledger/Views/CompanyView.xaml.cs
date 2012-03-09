using System;
using System.Windows;
using System.Windows.Controls;
using FacetedWorlds.Ledger.Models;
using FacetedWorlds.Ledger.ViewModels;
using UpdateControls.XAML;

namespace FacetedWorlds.Ledger.Views
{
    public partial class CompanyView : UserControl
    {
        public CompanyView()
        {
            InitializeComponent();
        }

        private void NewAccount_Click(object sender, RoutedEventArgs e)
        {
            var window = new NewAccountWindow();
            var newAccount = new NewAccountModel();
            window.DataContext = ForView.Wrap(newAccount);
            window.Closed += NewAccountWindow_Closed;
            window.Show();
        }

        void NewAccountWindow_Closed(object sender, EventArgs e)
        {
            NewAccountWindow window = (NewAccountWindow)sender;
            if (window.DialogResult ?? false)
            {
                var viewModel = ForView.Unwrap<CompanyViewModel>(DataContext);
                var newAccount = ForView.Unwrap<NewAccountModel>(((FrameworkElement)sender).DataContext);
                viewModel.NewAccount(newAccount);
            }
            window.Closed -= NewAccountWindow_Closed;
        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = ForView.Unwrap<CompanyViewModel>(((FrameworkElement)sender).DataContext);
            MessageBoxResult result = MessageBox.Show(String.Format("Delete account {0}?", viewModel.SelectedAccount.Name), "Delete account", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                viewModel.DeleteAccount();
            }
        }
    }
}
