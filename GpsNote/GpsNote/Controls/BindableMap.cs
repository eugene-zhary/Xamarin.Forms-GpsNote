using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GpsNote.Controls
{
    public class BindableMap : Map
    {
        #region -- Public properties --

        public static readonly BindableProperty MapSpanProperty =
            BindableProperty.Create(nameof(MapSpan), typeof(MapSpan), typeof(BindableMap), null, BindingMode.TwoWay, null, MapSpanPropertyChanged);

        public MapSpan MapSpan
        {
            get => (MapSpan)GetValue(MapSpanProperty);
            set => SetValue(MapSpanProperty, value);
        }

        #endregion

        #region -- Private helpers --

        private static void MapSpanPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisInstance = bindable as BindableMap;
            var newMapSpan = newValue as MapSpan;

            thisInstance?.MoveToRegion(newMapSpan);
        }

        #endregion
    }
}
