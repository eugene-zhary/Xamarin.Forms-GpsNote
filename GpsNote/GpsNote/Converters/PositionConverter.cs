using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.Converters
{
    public class PositionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Position output = new Position();

            if(values[0] != null && values[1] != null)
            {
                output = new Position((double)values[0], (double)values[1]);
            }

            return output;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] output = null;

            if(value is Position pos)
            {
                output = new object[] { pos.Latitude, pos.Longitude };
            }

            return output;
        }
    }
}
