using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using System.Windows.Input;

namespace GpsNote.Controls
{
    public class BindableMap : Map
    {
        public BindableMap()
        {
            PinsSource = Pins as ObservableCollection<Pin>;
            MapClicked += BindableMap_MapClicked;
            PinClicked += BindableMap_PinClicked;
        }

        #region -- Public properties --

        private static readonly BindablePropertyKey PinsSourcePropertyKey = BindableProperty.CreateReadOnly(
            propertyName: nameof(PinsSource),
            returnType: typeof(ObservableCollection<Pin>),
            declaringType: typeof(BindableMap),
            defaultValue: default(ObservableCollection<Pin>));

        public static readonly BindableProperty PinsSourceProperty = PinsSourcePropertyKey.BindableProperty;
        
        public ObservableCollection<Pin> PinsSource
        {
            get => (ObservableCollection<Pin>)GetValue(PinsSourceProperty);
            private set => SetValue(PinsSourcePropertyKey, value);
        }

        public static readonly BindableProperty CurrentPositionProperty = BindableProperty.Create(
            propertyName: nameof(CurrentPosition),
            returnType: typeof(CameraPosition),
            declaringType: typeof(BindableMap),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            validateValue: null,
            propertyChanged: OnCurrentPositionChanged);
        
        public CameraPosition CurrentPosition
        {
            get => (CameraPosition)GetValue(CurrentPositionProperty);
            set => SetValue(CurrentPositionProperty, value);
        }

        public static readonly BindableProperty MapClickedCommandProperty = BindableProperty.Create(
            propertyName: nameof(MapClickedCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(BindableMap));

        public ICommand MapClickedCommand
        {
            get => (ICommand)GetValue(MapClickedCommandProperty);
            set => SetValue(MapClickedCommandProperty, value);
        }

        public static readonly BindableProperty PinClickedCommandProperty = BindableProperty.Create(
            propertyName: nameof(PinClickedCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(BindableMap));

        public ICommand PinClickedCommand
        {
            get => (ICommand)GetValue(PinClickedCommandProperty);
            set => SetValue(PinClickedCommandProperty, value);
        }

        #endregion

        #region -- Private helpers --

        private static void OnCurrentPositionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var instance = bindable as BindableMap;
            var newCamPos = newValue as CameraPosition;

            if(instance != null && newCamPos != null)
            {
                var camUpdate = CameraUpdateFactory.NewPositionZoom(newCamPos.Target, newCamPos.Zoom);
                instance.MoveCamera(camUpdate);
            }
        }

        private void BindableMap_MapClicked(object sender, MapClickedEventArgs e)
        {
            MapClickedCommand?.Execute(e);
        }

        private void BindableMap_PinClicked(object sender, PinClickedEventArgs e)
        {
            e.Handled = true;
            PinClickedCommand?.Execute(e);
        }

        #endregion
    }
}
