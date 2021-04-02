using GpsNote.Services.Map;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;

namespace GpsNote.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private readonly IMapManager _mapManager;
        public MapPageViewModel(INavigationService navigationService, IMapManager mapManager) : base(navigationService)
        {
            _mapSpan = new MapSpan(new Position(48.445532, 35.066219), 0.12, 0.12);

            Pins = new ObservableCollection<Pin>();


            Title = "Map";
            _mapManager = mapManager;
        }

        #region -- Public properties --

        public ObservableCollection<Pin> Pins { get; set; }

        private MapSpan _mapSpan;
        public MapSpan MapSpan
        {
            get => _mapSpan;
            set => SetProperty(ref _mapSpan, value, nameof(MapSpan));
        }

        public ICommand AddPinCommand => new Command(OnAddPin);

        #endregion

        #region -- Private helpers --

        private void OnAddPin(object obj)
        {
            // TODO: add pins
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
