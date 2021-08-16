using GpsNote.Models;
using GpsNote.Resources;
using GpsNote.Services.Map;
using GpsNote.Services.Permissions;
using Prism.Navigation;
using Prism.Services;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
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

        private IAsyncCommand<MapClickedEventArgs> _mapClickedCommand;
        public IAsyncCommand<MapClickedEventArgs> MapClickedCommand => _mapClickedCommand ??= new AsyncCommand<MapClickedEventArgs>(OnMapClickedAsync, allowsMultipleExecutions: false);

        private IAsyncCommand _completeCommand;
        public IAsyncCommand CompleteCommand => _completeCommand ??= new AsyncCommand(OnCompleteAsync, allowsMultipleExecutions: false);

        #endregion

        #region -- Overrides --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(Constants.Navigation.SELECTED_PIN, out PinModel pin))
            {
                SetEditPin(pin);
            }
            else
            {
                SetAddPin();
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(Latitude) || args.PropertyName == nameof(Longitude))
            {
                UpdatePin();
            }
        }

        #endregion

        #region -- Private helpers --

        private Task OnMapClickedAsync(MapClickedEventArgs ev)
        {
            Latitude = ev.Point.Latitude;
            Longitude = ev.Point.Longitude;

            return Task.CompletedTask;
        }

        private async Task OnCompleteAsync()
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

            try
            {
                await PinService.AddOrUpdatePinAsync(_currentPin);
            }
            catch (Exception ex)
            {
                await PageDialogService.DisplayAlertAsync(Title, ex.Message, Strings.Cancel);
            }
        }

        private void SetAddPin()
        {
            _currentPin = new PinModel();

            Title = Strings.AddPinTitle;
        }

        private void SetEditPin(PinModel pin)
        {
            _currentPin = pin;
            Label = pin.Label;
            Details = pin.Address;
            Latitude = pin.Latitude;
            Longitude = pin.Longitude;

            MapCamera = new CameraPosition(new Position(Latitude, Longitude), 15);

            Title = Strings.EditPinTitle;
        }

        #endregion
    }
}
