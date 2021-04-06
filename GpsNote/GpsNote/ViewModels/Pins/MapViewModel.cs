using GpsNote.Properties;
using GpsNote.Services.Map;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using System.Linq;
using Xamarin.Essentials;
using System.Threading.Tasks;
using GpsNote.Services.Permissions;
using Plugin.Permissions;
using Prism.Services;

namespace GpsNote.ViewModels
{
    public class MapViewModel : BasePinsViewModel
    {
        private readonly IPermissionManager _permissionManager;
        private readonly IPageDialogService _pageDialogService;

        public MapViewModel(INavigationService navigation, IPinManager pinManager, IPermissionManager permissions, IPageDialogService pageDialog) : base(navigation, pinManager)
        {
            _permissionManager = permissions;
            _pageDialogService = pageDialog;

            Title = AppResources.MapTitle;
        }

        #region -- Public properties --

        private bool _myLocationEnabled;
        public bool MyLocationEnabled
        {
            get => _myLocationEnabled;
            set => SetProperty(ref _myLocationEnabled, value, nameof(MyLocationEnabled));
        }

        private MapSpan _mapSpan;
        public MapSpan MapSpan
        {
            get => _mapSpan;
            set => SetProperty(ref _mapSpan, value, nameof(MapSpan));
        }

        public ICommand MapClickedCommand => new Command<MapClickedEventArgs>(OnMapClicked);

        #endregion

        #region -- Overrides --

        public async override void Initialize(INavigationParameters parameters)
        {
            var status = await _permissionManager.RequestLocationPermissionAsync();

            if(status)
            {
                MyLocationEnabled = true;
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if(parameters.ContainsKey(nameof(Pin)))
            {
                var selectedPin = parameters.GetValue<Pin>(nameof(Pin));
                MapSpan = new MapSpan(selectedPin.Position, 0.01, 0.01);
            }
        }

        #endregion

        #region -- Private helpers --

        private void OnMapClicked(MapClickedEventArgs pos)
        {

        }

        private async Task SetCurrentLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            MapSpan = new MapSpan(new Position(location.Latitude, location.Longitude), 0.15, 0.15);
        }

        #endregion

    }
}
