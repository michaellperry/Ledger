using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using FacetedWorlds.Ledger.Models;
using UpdateControls.XAML;
using FacetedWorlds.Ledger.ViewModels.Main;

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
            newAccount.Name = "<New Account>";
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
            MessageBoxResult result = MessageBox.Show("Delete account n?", "Delete account", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var viewModel = ForView.Unwrap<CompanyViewModel>(((FrameworkElement)sender).DataContext);
                viewModel.DeleteAccount();
            }
        }
    }
}
