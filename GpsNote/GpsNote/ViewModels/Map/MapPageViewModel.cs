using GpsNote.Services.Map;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using Prism.Commands;

namespace GpsNote.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private readonly IMapManager _mapManager;
        public MapPageViewModel(INavigationService navigationService, IMapManager mapManager) : base(navigationService)
        {
            _mapManager = mapManager;

            Title = "Map";
            Pins = new ObservableCollection<Pin>();
            MapSpan = new MapSpan(new Position(48.445532, 35.066219), 0.12, 0.12);
        }

        #region -- Public properties --

        public ObservableCollection<Pin> Pins { get; set; }

        private MapSpan _mapSpan;
        public MapSpan MapSpan
        {
            get => _mapSpan;
            set => SetProperty(ref _mapSpan, value, nameof(MapSpan));
        }

        public DelegateCommand<MapSpan> AddPinCommand => new DelegateCommand<MapSpan>(OnAddPin);

        #endregion

        #region -- Private helpers --

        private void OnAddPin(MapSpan span)
        {
            Pins.Add(new Pin
            {
                Label = "test",
                Address = "test",
                Position =span.Center
            });
            //Pins.Add(pin);

        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var pins = await _mapManager.GetPins();
            pins.ToList().ForEach(Pins.Add);

        }

        #endregion
    }


}
