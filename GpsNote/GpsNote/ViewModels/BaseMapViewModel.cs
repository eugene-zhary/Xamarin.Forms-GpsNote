using GpsNote.Extensions;
using GpsNote.Resources;
using GpsNote.Services.Map;
using GpsNote.Services.Permissions;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.ViewModels
{
    public abstract class BaseMapViewModel : BaseViewModel
    {
        public BaseMapViewModel(
            INavigationService navigationService,
            IPinService pinService,
            IPermissionService permissionService,
            IPageDialogService pageDialogService)
            : base(navigationService)
        {
            PinService = pinService;
            PermissionService = permissionService;
            PageDialogService = pageDialogService;

            PinsCollection = new ObservableCollection<Pin>();
        }

        #region -- Public properties --

        protected IPinService PinService { get; private set; }
        protected IPermissionService PermissionService { get; private set; }
        protected IPageDialogService PageDialogService { get; private set; }


        private bool isMyLocationEnabled;
        public bool IsMyLocationEnabled
        {
            get => isMyLocationEnabled;
            set => SetProperty(ref isMyLocationEnabled, value, nameof(IsMyLocationEnabled));
        }

        private ObservableCollection<Pin> _pinsCollection;
        public ObservableCollection<Pin> PinsCollection
        {
            get => _pinsCollection;
            set => SetProperty(ref _pinsCollection, value, nameof(PinsCollection));
        }

        private CameraPosition _mapCamera;
        public CameraPosition MapCamera
        {
            get => _mapCamera;
            set => SetProperty(ref _mapCamera, value, nameof(MapCamera));
        }

        private IAsyncCommand _myLocationCommand;
        public IAsyncCommand MyLocationCommand => _myLocationCommand ??= new AsyncCommand(OnMyLocationAsync, allowsMultipleExecutions: false);

        #endregion

        #region -- Public methods --

        protected async Task UpdatePinsCollectionAsync()
        {
            try
            {
                var pinModels = await PinService.GetPinsAsync();

                PinsCollection.Clear();

                foreach (var pin in pinModels)
                {
                    PinsCollection.Add(pin.ToPin());
                }
            }
            catch (Exception ex)
            {
                await PageDialogService.DisplayAlertAsync(Title, ex.Message, Strings.Cancel);
            }
        }

        protected void NavigateCamera(Position pos)
        {
            if (pos != null)
            {
                MapCamera = new CameraPosition(pos, 15);
            }
        }

        #endregion

        #region -- Private helpers --

        private async Task OnMyLocationAsync()
        {
            bool isGranted = await PermissionService.RequestLocationPermissionAsync();

            if (isGranted)
            {
                IsMyLocationEnabled = true;

                var location = await Geolocation.GetLastKnownLocationAsync();

                NavigateCamera(new Position(location.Latitude, location.Longitude));
            }
            else
            {
                IsMyLocationEnabled = false;

                await AskForSettingsAsync();
            }
        }

        private async Task AskForSettingsAsync()
        {
            bool isAgree = await PageDialogService.DisplayAlertAsync(Strings.Location, Strings.AskForLocation, Strings.Settings, Strings.Ok);

            if (isAgree)
            {
                PermissionService.GoToAppSettings();
            }
        }

        #endregion
    }
}