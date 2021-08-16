using GpsNote.Models;
using GpsNote.Services.Weather;
using Prism.Navigation;

namespace GpsNote.ViewModels
{
    public class PinInfoPopupViewModel : BaseViewModel
    {
        private readonly IWeatherService _weatherService;

        public PinInfoPopupViewModel(
            INavigationService navigationService,
            IWeatherService weatherService)
            : base(navigationService)
        {
            _weatherService = weatherService;
        }

        #region -- Public properties --

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value, nameof(IsBusy));
        }

        private PinModel _pin;
        public PinModel Pin
        {
            get => _pin;
            set => SetProperty(ref _pin, value, nameof(Pin));
        }

        private WeatherModel _forecast;
        public WeatherModel Forecast
        {
            get => _forecast;
            set => SetProperty(ref _forecast, value, nameof(Forecast));
        }

        #endregion

        #region -- Overrides --

        public override async void Initialize(INavigationParameters parameters)
        {
            if (parameters.TryGetValue(Constants.Navigation.SELECTED_PIN, out PinModel pin))
            {
                IsBusy = true;

                Pin = pin;
                Forecast = await _weatherService.GetForecastAsync(Pin.Latitude, Pin.Longitude);

                IsBusy = false;
            }
        }

        #endregion
    }
}
