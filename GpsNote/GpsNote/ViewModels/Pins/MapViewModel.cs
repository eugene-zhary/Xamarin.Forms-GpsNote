using GpsNote.Properties;
using GpsNote.Services.Map;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using System.Linq;
using Xamarin.Essentials;

namespace GpsNote.ViewModels
{
    public class MapViewModel : BasePinsViewModel
    {
        public MapViewModel(INavigationService navigation, IPinManager pinManager) : base(navigation, pinManager)
        {
            Title = AppResources.MapTitle;
        }

        #region -- Public properties --

        private MapSpan _mapSpan;
        public MapSpan MapSpan
        {
            get => _mapSpan;
            set => SetProperty(ref _mapSpan, value, nameof(MapSpan));
        }

        public ICommand MapClickedCommand => new Command<MapClickedEventArgs>(OnMapClicked);

        #endregion

        #region -- Overrides --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);


            if (parameters.ContainsKey(nameof(Pin)))
            {
                var selectedPin = parameters.GetValue<Pin>(nameof(Pin));
                MapSpan = new MapSpan(selectedPin.Position, 0.01, 0.01);
            }
            else
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                MapSpan = new MapSpan(new Position(location.Latitude, location.Longitude), 0.15, 0.15);
            }
        }

        #endregion

        #region -- Private helpers --

        private void OnMapClicked(MapClickedEventArgs pos)
        {

        }

        #endregion

    }
}
