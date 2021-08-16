using GpsNote.Models;
using GpsNote.Services.Weather;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace GpsNote.ViewModels
{
    public class PinInfoDialogViewModel : BindableBase, IDialogAware
    {
        private readonly IWeatherService _weatherService;

        public PinInfoDialogViewModel(IWeatherService weatherService)
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

        #region -- IDialogAware implementation --

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public async void OnDialogOpened(IDialogParameters parameters)
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
