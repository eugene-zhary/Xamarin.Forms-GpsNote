using GpsNote.Properties;
using GpsNote.Services.Map;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;
using GpsNote.Views.Pins;
using System.Windows.Input;
using Xamarin.Forms;
using GpsNote.Extensions;

namespace GpsNote.ViewModels
{
    public class PinsViewModel : BasePinsViewModel
    {
        public PinsViewModel(INavigationService navigation, IPinManager pinsManager) : base(navigation, pinsManager)
        {
            Title = AppResources.PinsTitle;
        }

        #region -- Public properties --

        private Pin _selectedPin;
        public Pin SelectedPin
        {
            get => _selectedPin;
            set => NavigateToPin(value);
        }

        public ICommand AddPinCommand => new Command(OnAddPin);

        #endregion

        #region -- Overrides --

        public override async void OnAppearing()
        {
            base.OnAppearing();

            await UpdatePins();
        }

        #endregion

        #region -- Private helpers --

        private async void NavigateToPin(Pin pin)
        {
            SetProperty(ref _selectedPin, pin, nameof(SelectedPin));

            if (pin != null)
            {
                var nav_params = new NavigationParameters
                {
                    { nameof(Pin), pin }
                };

                await _navigationService.SelectTabAsync($"{nameof(MapView)}", nav_params);
            }
        }

        private async void OnAddPin()
        {
            await NavigationService.NavigateAsync($"{nameof(AddPinView)}");
        }

        #endregion
    }
}
