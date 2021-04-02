using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GpsNote.Controls
{
    public class BindableMap : Map
    {
        public BindableMap()
        {
            PinsSource = new ObservableCollection<Pin>();
            PinsSource.CollectionChanged += PinsSourceOnCollectionChanged;
        }

        #region -- Public properties --

        public static readonly BindableProperty PinsSourceProperty =
            BindableProperty.Create(nameof(PinsSource), typeof(ObservableCollection<Pin>), typeof(BindableMap), null, BindingMode.TwoWay, null, PinsSourcePropertyChanged);

        public static readonly BindableProperty MapSpanProperty =
            BindableProperty.Create(nameof(MapSpan), typeof(MapSpan), typeof(BindableMap), null, BindingMode.TwoWay, null, MapSpanPropertyChanged);


        public ObservableCollection<Pin> PinsSource
        {
            get => (ObservableCollection<Pin>)GetValue(PinsSourceProperty);
            set => SetValue(PinsSourceProperty, value);
        }

        public MapSpan MapSpan
        {
            get => (MapSpan)GetValue(MapSpanProperty);
            set => SetValue(MapSpanProperty, value);
        }

        #endregion

        #region -- Private helpers --

        private static void PinsSourcePropertyChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            var thisInstance = bindable as BindableMap;
            var newPinsSource = newValue as ObservableCollection<Pin>;

            if (thisInstance == null || newPinsSource == null)
            {
                return;
            }

            UpdatePinsSource(thisInstance, newPinsSource);
        }

        private static void MapSpanPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisInstance = bindable as BindableMap;
            var newMapSpan = newValue as MapSpan;

            thisInstance?.MoveToRegion(newMapSpan);
        }

        private void PinsSourceOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdatePinsSource(this, sender as IEnumerable<Pin>);
        }

        private static void UpdatePinsSource(Map bindableMap, IEnumerable<Pin> newSource)
        {
            bindableMap.Pins.Clear();
            foreach (var pin in newSource)
            {
                bindableMap.Pins.Add(pin);
            }
        }

        #endregion
    }
}
