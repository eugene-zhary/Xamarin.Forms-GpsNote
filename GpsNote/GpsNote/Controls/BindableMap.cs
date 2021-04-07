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

            UiSettings.MyLocationButtonEnabled = true;
        }


        #region -- Public properties --

        private static readonly BindablePropertyKey PinsSourcePropertyKey = BindableProperty.CreateReadOnly(nameof(PinsSource), typeof(ObservableCollection<Pin>), typeof(BindableMap), default(ObservableCollection<Pin>));

        public static readonly BindableProperty PinsSourceProperty = PinsSourcePropertyKey.BindableProperty;

        public static readonly BindableProperty CurrentPositionProperty
            = BindableProperty.Create(nameof(CurrentPosition), typeof(CameraPosition), typeof(BindableMap), null, BindingMode.TwoWay, null, OnCurrentPositionChanged);

        public static readonly BindableProperty MapClickedCommandProperty
            = BindableProperty.Create(nameof(MapClickedCommand), typeof(ICommand), typeof(BindableMap), null);


        public ObservableCollection<Pin> PinsSource
        {
            get => (ObservableCollection<Pin>)GetValue(PinsSourceProperty);
            private set => SetValue(PinsSourcePropertyKey, value);
        }
        public CameraPosition CurrentPosition
        {
            get => (CameraPosition)GetValue(CurrentPositionProperty);
            set => SetValue(CurrentPositionProperty, value);
        }
        public ICommand MapClickedCommand
        {
            get => (ICommand)GetValue(MapClickedCommandProperty);
            set => SetValue(MapClickedCommandProperty, value);
        }

        #endregion

        #region -- Private helpers --

        private static void OnCurrentPositionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as BindableMap;
            var newCamPos = newValue as CameraPosition;

            if (instance != null && newCamPos != null)
            {
                var camUpdate = CameraUpdateFactory.NewPositionZoom(newCamPos.Target, newCamPos.Zoom);
                instance.MoveCamera(camUpdate);
            }
        }

        private void BindableMap_MapClicked(object sender, MapClickedEventArgs e)
        {
            MapClickedCommand.Execute(e);
        }

        #endregion
    }
}
