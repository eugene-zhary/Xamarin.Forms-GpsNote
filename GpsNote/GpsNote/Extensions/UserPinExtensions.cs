using GpsNote.Models;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.Extensions
{
    public static class UserPinExtensions
    {
        public static Pin AsPin(this UserPin user_pin)
        {
            Pin output = null;

            if (user_pin != null)
            {
                output = new Pin
                {
                    Label = user_pin.Label,
                    Address = user_pin.Address,
                    Position = new Position(user_pin.Latitude, user_pin.Longitude),
                    IsVisible = user_pin.IsFavorite
                };
            }

            return output;
        }

        public static INavigationParameters AsNavigationParameters(this UserPin pin)
        {
            var nav_params = new NavigationParameters
            {
                { nameof(UserPin), pin }
            };
            return nav_params;
        }

        public static IDialogParameters AsDialogParams(this UserPin pin)
        {
            var nav_params = new DialogParameters
            {
                { nameof(UserPin), pin }
            };
            return nav_params;
        }
    }
}
