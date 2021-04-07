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

        private CameraPosition _mapCamera;
        public CameraPosition MapCamera
        {
            get => _mapCamera;
            set => SetProperty(ref _mapCamera, value, nameof(MapCamera));
        }

        #endregion

        #region -- Overrides --

        public override async void OnAppearing()
        {
            base.OnAppearing();

            await UpdatePins();
        }

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
                MapCamera = new CameraPosition(selectedPin.Position, 15);
            }
        }

        #endregion
    }
}
