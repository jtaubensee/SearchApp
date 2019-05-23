using System;
using System.Globalization;
using Xamarin.Forms;

namespace SearchApp.Components.Converters
{
    public class GreaterThanZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue > 0;
            }
            else if (value is double doubleValue)
            {
                return doubleValue > 0;
            }
            else if (value is decimal decimalValue)
            {
                return decimalValue > 0;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
