using GpsNote.Services.Map;
using Prism.Navigation;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.ViewModels
{
    public class AddEditPinViewModel : BasePinsViewModel
    {
        public AddEditPinViewModel(INavigationService navigation, IPinManager pinManager) : base(navigation, pinManager)
        {
            Title = "Add Pin";
            Label = String.Empty;
            Details = String.Empty;
        }

        #region -- Public properties --

        private string _label;
        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value, nameof(Label));
        }
        private string _details;
        public string Details
        {
            get => _details;
            set => SetProperty(ref _details, value, nameof(Details));
        }
        private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set => SetProperty(ref _latitude, value, nameof(Latitude));
        }
        private double _longitude;
        public double Longitude
        {
            get => _longitude;
            set => SetProperty(ref _longitude, value, nameof(Longitude));
        }

        private CameraPosition _mapCamera;
        public CameraPosition MapCamera
        {
            get => _mapCamera;
            set => SetProperty(ref _mapCamera, value, nameof(MapCamera));
        }

        public ICommand MapClickedCommand => new Command<MapClickedEventArgs>(OnMapClicked);
        public ICommand CompleteCommand => new Command(OnComplete);

        #endregion

        #region -- Private helpers --

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch(args.PropertyName)
            {
                case nameof(Label):
                case nameof(Details):
                case nameof(Latitude):
                case nameof(Longitude):
                    UpdatePin();
                    break;
            }
        }

        private void OnMapClicked(MapClickedEventArgs ev)
        {
            Latitude = ev.Point.Latitude;
            Longitude = ev.Point.Longitude;
        }

        private async void OnComplete(object obj)
        {
            await SavePins();
            await NavigationService.GoBackAsync();
        }

        private void UpdatePin()
        {
            PinsCollection.Clear();
            PinsCollection.Add(new Pin
            {
                Label = Label,
                Address = Details,
                Position = new Position(Latitude, Longitude)
            });
        }

        #endregion
    }
}
