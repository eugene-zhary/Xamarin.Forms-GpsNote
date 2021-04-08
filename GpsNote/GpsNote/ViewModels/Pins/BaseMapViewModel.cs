using GpsNote.Controls;
using GpsNote.Extensions;
using GpsNote.Services;
using GpsNote.Services.Map;
using GpsNote.Services.Permissions;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.ViewModels
{
    public abstract class BaseMapViewModel : ViewModelBase, IViewActionsHandler
    {
        public BaseMapViewModel(INavigationService navigation, IPinManager pinManager, IPermissionManager permissions) : base(navigation)
        {
            PinManager = pinManager;
            Permissions = permissions;
        }

        #region -- Public properties --

        private bool _myLocationEnabled;
        public bool MyLocationEnabled
        {
            get => _myLocationEnabled;
            set => SetProperty(ref _myLocationEnabled, value, nameof(MyLocationEnabled));
        }

        protected IPinManager PinManager { get; }
        protected IPermissionManager Permissions { get; }

        public ObservableCollection<Pin> PinsCollection { get; set; }

        private CameraPosition _mapCamera;
        public CameraPosition MapCamera
        {
            get => _mapCamera;
            set => SetProperty(ref _mapCamera, value, nameof(MapCamera));
        }

        public ICommand MyLocationCommand => new Command<MyLocationButtonClickedEventArgs>(OnMyLocation);

        #endregion

        #region -- IViewActionsHandler implementation --
        public virtual void OnAppearing() { }
        public virtual void OnDisappearing() { }
        #endregion

        protected async Task UpdatePinsCollectionAsync()
        {
            PinsCollection.Clear();
            var pins = await PinManager.GetPinsAsync();
            pins.ToList().ForEach(p => PinsCollection.Add(p.AsPin()));
        }

        #region -- Private helpers --

        private async void OnMyLocation(MyLocationButtonClickedEventArgs obj)
        {
            var status = await Permissions.RequestLocationPermissionAsync();

            if(status)
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                MapCamera = new CameraPosition(new Position(location.Latitude, location.Longitude), 15);
                MyLocationEnabled = true;
            }
            else
            {
                MyLocationEnabled = false;
            }
        }

        #endregion
    }
}