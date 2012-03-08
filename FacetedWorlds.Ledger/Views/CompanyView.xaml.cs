﻿using System;
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
            NewAccountWindow window = new NewAccountWindow();
            window.Show();
        }

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }
    }
}
