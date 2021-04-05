using GpsNote.Models;
using GpsNote.ViewModels;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.Extensions
{
    public static class PinExtensions
    {
        public static Pin ToPin(this UserPin user_pin)
        {
            Pin output = null;

            if (user_pin != null)
            {
                output = new Pin
                {
                    Label = user_pin.Label,
                    Address = user_pin.Address,
                    Position = new Position(user_pin.Latitude, user_pin.Longitude)
                };
            }

            return output;
        }

        public static UserPin ToUserPin(this Pin pin, int user_id)
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
    }
}
