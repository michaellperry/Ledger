﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FacetedWorlds.Ledger.Converters
{
    public class AccountTypeBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Application.Current.Resources[String.Format("{0}Brush", value)];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
