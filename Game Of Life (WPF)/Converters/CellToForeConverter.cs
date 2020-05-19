using Game_Of_Life__WPF_.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Game_Of_Life__WPF_.Converters
{
    class CellToForeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Cell convertVal = (Cell)value;

            if (convertVal.State)
            {
                return Brushes.White;
            }
            else
            {
                return Brushes.Black;
            }      

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
