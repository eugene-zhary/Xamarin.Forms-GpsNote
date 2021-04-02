using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GpsNote.Converters
{
    public class MapClickedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as MapClickedEventArgs;
            if (eventArgs == null)
            {
                throw new ArgumentException("Expected TappedEventArgs as value", "value");
            }

            return eventArgs.Position;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
