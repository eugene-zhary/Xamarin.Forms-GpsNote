using GpsNote.Extensions;
using GpsNote.Models;
using GpsNote.Services.Map;
using GpsNote.Services.Permissions;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.ViewModels
{
    public abstract class BaseMapViewModel : ViewModelBase
    {
        protected IPinManager _pinManager { get; }
        protected IPermissionManager _permissions { get; }

        public BaseMapViewModel(INavigationService navigation, IPinManager pinManager, IPermissionManager permissions) : base(navigation)
        {
            _pinManager = pinManager;
            _permissions = permissions;

            PinsCollection = new ObservableCollection<Pin>();
        }

        #region -- Public properties --

        private bool isMyLocationEnabled;
        public bool IsMyLocationEnabled
        {
            get => isMyLocationEnabled;
            set => SetProperty(ref isMyLocationEnabled, value, nameof(IsMyLocationEnabled));
        }

        public ObservableCollection<Pin> PinsCollection { get; set; }

        private CameraPosition _mapCamera;
        public CameraPosition MapCamera
        {
            get => _mapCamera;
            set => SetProperty(ref _mapCamera, value, nameof(MapCamera));
        }

        public ICommand MyLocationCommand => new Command<MyLocationButtonClickedEventArgs>(OnMyLocation);

        #endregion

        #region -- Protected implementaiton --

        protected async Task UpdatePinsCollectionAsync(string searchText = null)
        {
            PinsCollection.Clear();
            IEnumerable<UserPin> pins = null;

            if(searchText == null)
            {
                pins = await _pinManager.GetPinsAsync();
            }
            else
            {
                pins = await _pinManager.SearchPinsByLabelAsync(searchText);
            }

            pins.ToList().ForEach(p => PinsCollection.Add(p.AsPin()));
        }

        protected void NavigateCamera(Position pos)
        {
            if(pos != null)
            {
                MapCamera = new CameraPosition(pos, 15);
            }
        }

        #endregion

        #region -- Private helpers --

        private async void OnMyLocation(MyLocationButtonClickedEventArgs obj)
        {
            var status = await _permissions.RequestLocationPermissionAsync();

            if(status)
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                NavigateCamera(new Position(location.Latitude, location.Longitude));

                IsMyLocationEnabled = true;
            }
            else
            {
                IsMyLocationEnabled = false;
            }
        }

        #endregion
    }
}