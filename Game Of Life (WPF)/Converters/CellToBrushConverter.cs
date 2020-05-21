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
    class CellToBrushConverter : IValueConverter
    {

        private static readonly object aliveCell = Brushes.Black;
        private static readonly object deadCell = Brushes.White;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {

                Cell convertVal = (Cell)value;

                //    if (convertVal.State)
                //    {
                //        return Brushes.Black;
                //    }
                //    else
                //    {
                //        return Brushes.White;
                //    }
                //} else
                //{
                //    return Brushes.White;
                //}

                if (convertVal.State)
                    return aliveCell;

            }

            return deadCell;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
