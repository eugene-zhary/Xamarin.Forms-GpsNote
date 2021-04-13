using System;
using System.Globalization;
using Xamarin.Forms;

namespace GpsNote.Converters
{
    public class StringToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Double.TryParse(value.ToString(), out double result);
            return result;
        }
    }
}
