using GpsNote.Services.Map;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using Prism.Commands;
using System.Threading.Tasks;
using System;
using GpsNote.Extensions;

namespace GpsNote.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private readonly IMapManager _mapManager;
        public MapPageViewModel(INavigationService navigationService, IMapManager mapManager) : base(navigationService)
        {
            _mapManager = mapManager;

            MyMap = new Map();
            Title = "Map";
        }

        #region -- Public properties --

        private Map _myMap;
        public Map MyMap
        {
            get => _myMap;
            set => SetProperty(ref _myMap, value, nameof(MyMap));
        }

        public ICommand AddPinCommand => new Command(OnAddPin);

        #endregion

        #region -- Private helpers --

        private void OnAddPin()
        {

        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await SetCurrentPosition();

            var pins = await _mapManager.GetPins();
            pins.ToList().ForEach(MyMap.Pins.Add);
        }

        private async Task SetCurrentPosition()
        {
            var location = await Xamarin.Essentials.Geolocation.GetLastKnownLocationAsync();

            try
            {
                if(location != null)
                {
                    var pos = new Position(location.Latitude, location.Longitude);
                    MyMap.MoveToRegion(new MapSpan(pos, 0.15, 0.15));
                }
            }
            catch(Exception)
            {

            }
        }

        #endregion
    }


}
