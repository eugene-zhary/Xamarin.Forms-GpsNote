using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;

namespace GpsNote.Controls
{
    public class BindableMap : Map
    {
        public BindableMap() : base()
        {
            PinsSource = new ObservableCollection<Pin>();
            PinsSource.CollectionChanged += PinsSourceOnCollectionChanged;
        }

        #region -- Public properties --

        public static readonly BindableProperty PinsSourceProperty
            = BindableProperty.Create(nameof(PinsSource), typeof(ObservableCollection<Pin>), typeof(BindableMap), null, BindingMode.TwoWay, null, PinsSourcePropertyChanged);

        public static readonly BindableProperty MapSpanProperty
            = BindableProperty.Create(nameof(MapSpan), typeof(MapSpan), typeof(BindableMap), null, BindingMode.TwoWay, null, MapSpanPropertyChanged);

        public static readonly BindableProperty SelectedPinProperty
            = BindableProperty.Create(nameof(MapSpan), typeof(Pin), typeof(BindableMap), null, BindingMode.TwoWay, null, SelectedPinPropertyChanged);


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

        public Pin SelectedPin
        {
            get => (Pin)GetValue(SelectedPinProperty);
            set => SetValue(SelectedPinProperty, value);
        }

        #endregion

        #region -- Private helpers --

        private static void PinsSourcePropertyChanged(BindableObject bindable, object oldvalue, object newValue)
        {
            var thisInstance = bindable as BindableMap;
            var newPinsSource = newValue as ObservableCollection<Pin>;

            if(thisInstance != null && newPinsSource != null)
            {
                UpdatePinsSource(thisInstance, newPinsSource);
            }
        }

        private void PinsSourceOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdatePinsSource(this, sender as IEnumerable<Pin>);
        }

        private static void UpdatePinsSource(Map bindableMap, IEnumerable<Pin> newSource)
        {
            bindableMap.Pins.Clear();
            foreach(var pin in newSource)
            {
                bindableMap.Pins.Add(pin);
            }
        }

        private static void MapSpanPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisInstance = bindable as BindableMap;
            var newMapSpan = newValue as MapSpan;

            thisInstance?.MoveToRegion(newMapSpan);
        }

        private static void SelectedPinPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var thisInstance = bindable as BindableMap;
            var newSelectedPin = newValue as Pin;

            if(thisInstance != null && newSelectedPin != null)
            {
                thisInstance.SelectedPin = newSelectedPin;
            }
        }

        #endregion
    }
}
