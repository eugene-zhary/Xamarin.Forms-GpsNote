using GpsNote.Models;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.Extensions
{
    public static class PinExtensions
    {
        public static PinModel ToPinModel(this Pin pin, int user_id = 0)
        {
            return new PinModel
            {
                Label = pin.Label,
                Address = pin.Address,
                Latitude = pin.Position.Latitude,
                Longitude = pin.Position.Longitude,
                IsFavorite = pin.IsVisible,
                UserId = user_id
            };
        }

        public static Pin ToPin(this PinModel user_pin)
        {
            return new Pin
            {
                Label = user_pin.Label,
                Address = user_pin.Address,
                Position = new Position(user_pin.Latitude, user_pin.Longitude),
                IsVisible = user_pin.IsFavorite
            };
        }
    }
}
