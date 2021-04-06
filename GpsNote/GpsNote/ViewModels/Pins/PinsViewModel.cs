using GpsNote.Properties;
using GpsNote.Services.Map;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation.TabbedPages;
using Xamarin.Forms.GoogleMaps;
using System.Threading.Tasks;
using GpsNote.Views.Pins;

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

                await NavigationService.SelectTabAsync($"{nameof(MapView)}", nav_params);
            }
        }

        #endregion
    }
}
