using GpsNote.Models;
using GpsNote.ViewModels;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.Extensions
{
    public static class PinExtensions
    {
        public static UserPin AsUserPin(this Pin pin, int user_id = 0)
        {
            UserPin output = null;

            if (pin != null)
            {
                output = new UserPin
                {
                    Label = pin.Label,
                    Address = pin.Address,
                    Latitude = pin.Position.Latitude,
                    Longitude = pin.Position.Longitude,
                    UserId = user_id
                };
            }

            return output;
        }

        public static INavigationParameters AsNavigationParameters(this Pin pin)
        {
            var nav_params = new NavigationParameters
            {
                { nameof(Pin), pin }
            };
            return nav_params;
        }

        public static IDialogParameters AsDialogParams(this Pin pin)
        {
            var nav_params = new DialogParameters
            {
                { nameof(Pin), pin }
            };
            return nav_params;
        }
    }
}
