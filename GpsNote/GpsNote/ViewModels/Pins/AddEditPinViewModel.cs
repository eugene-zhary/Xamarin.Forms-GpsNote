using GpsNote.Models;
using GpsNote.Properties;
using GpsNote.Services.Map;
using GpsNote.Services.Permissions;
using Prism.Navigation;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.ViewModels
{
    public class AddEditPinViewModel : BaseMapViewModel
    {
        private UserPin _currentPin;

        public AddEditPinViewModel(INavigationService navigation, IPinManager pinManager, IPermissionManager permissions) : base(navigation, pinManager, permissions)
        {
            Title = AppResources.AddPinTitle;
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

        public ICommand MapClickedCommand => new Command<MapClickedEventArgs>(OnMapClicked);
        public ICommand CompleteCommand => new Command(OnComplete);

        #endregion

        #region -- Private helpers --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if(parameters.ContainsKey(nameof(UserPin)))
            {
                var pin = parameters.GetValue<UserPin>(nameof(UserPin));

                _currentPin = pin;
                Label = pin.Label;
                Details = pin.Address;
                Latitude = pin.Latitude;
                Longitude = pin.Longitude;

                MapCamera = new CameraPosition(new Position(Latitude, Longitude), 15);
            }
            else
            {
                _currentPin = new UserPin();
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch(args.PropertyName)
            {
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
            await SavePinChanges();
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

        private async Task SavePinChanges()
        {
            _currentPin.Label = Label;
            _currentPin.Address = Details;
            _currentPin.Longitude = Longitude;
            _currentPin.Latitude = Latitude;

            await PinManager.AddOrUpdatePinAsync(_currentPin);
        }

        #endregion
    }
}
