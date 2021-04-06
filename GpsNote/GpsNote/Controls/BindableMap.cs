using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;
using System.Linq;
using Xamarin.Forms.GoogleMaps;
using System.Diagnostics;
using System.Collections;
using System.Windows.Input;

namespace GpsNote.Controls
{
    public class BindableMap : Map
    {
        public BindableMap()
        {
            PinsSource = Pins as ObservableCollection<Pin>;
            MapClicked += BindableMap_MapClicked;
        }

        #region -- Public properties --

        private static readonly BindablePropertyKey PinsSourcePropertyKey = BindableProperty.CreateReadOnly(nameof(PinsSource), typeof(ObservableCollection<Pin>), typeof(BindableMap), default(ObservableCollection<Pin>));

        public static readonly BindableProperty PinsSourceProperty = PinsSourcePropertyKey.BindableProperty;

        public static readonly BindableProperty MapSpanProperty
            = BindableProperty.Create(nameof(MapSpan), typeof(MapSpan), typeof(BindableMap), null, BindingMode.TwoWay, null, MapSpanPropertyChanged);

        public static readonly BindableProperty MapClickedCommandProperty
            = BindableProperty.Create(nameof(MapClickedCommand), typeof(ICommand), typeof(BindableMap), null);


        public ObservableCollection<Pin> PinsSource
        {
            get => (ObservableCollection<Pin>)GetValue(PinsSourceProperty);
            private set => SetValue(PinsSourcePropertyKey, value);
        }
        public MapSpan MapSpan
        {
            get => (MapSpan)GetValue(MapSpanProperty);
            set => SetValue(MapSpanProperty, value);
        }
        public ICommand MapClickedCommand
        {
            get => (ICommand)GetValue(MapClickedCommandProperty);
            set => SetValue(MapClickedCommandProperty, value);
        }

        #endregion

        #region -- Private helpers --

        private static void MapSpanPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisInstance = bindable as BindableMap;
            var newMapSpan = newValue as MapSpan;

            thisInstance?.MoveToRegion(newMapSpan);
        }

        private void BindableMap_MapClicked(object sender, MapClickedEventArgs e)
        {
            MapClickedCommand.Execute(e);
        }

        #endregion
    }
}
