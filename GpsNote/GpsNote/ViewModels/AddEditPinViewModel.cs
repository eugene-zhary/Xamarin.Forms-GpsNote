using GpsNote.Models;
using GpsNote.Resources;
using GpsNote.Services.Map;
using GpsNote.Services.Permissions;
using Prism.Navigation;
using Prism.Services;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.ViewModels
{
    public class AddEditPinViewModel : BaseMapViewModel
    {
        private PinModel _currentPin;

        public AddEditPinViewModel(
            INavigationService navigationService, 
            IPinService pinService, 
            IPermissionService permissionService,
            IPageDialogService pageDialogService) 
            : base(navigationService, pinService, permissionService, pageDialogService)
        {
            _label = string.Empty;
            _details = string.Empty;
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

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(nameof(PinModel)))
            {
                var pin = parameters.GetValue<PinModel>(nameof(PinModel));

                _currentPin = pin;
                Label = pin.Label;
                Details = pin.Address;
                Latitude = pin.Latitude;
                Longitude = pin.Longitude;

                MapCamera = new CameraPosition(new Position(Latitude, Longitude), 15);

                Title = Strings.EditPinTitle;
            }
            else
            {
                _currentPin = new PinModel();

                Title = Strings.AddPinTitle;
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Latitude)
                || args.PropertyName == nameof(Longitude))
            {
                UpdatePin();
            }
        }

        #endregion

        #region -- Private helpers --

        private void OnMapClicked(MapClickedEventArgs ev)
        {
            Latitude = ev.Point.Latitude;
            Longitude = ev.Point.Longitude;
        }

        private async void OnComplete(object obj)
        {
            await SavePinChanges();
            await _navigationService.GoBackAsync();
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

            await PinService.AddOrUpdatePinAsync(_currentPin);
        }

        #endregion
    }
}
