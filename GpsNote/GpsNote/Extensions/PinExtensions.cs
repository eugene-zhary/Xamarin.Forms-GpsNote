using GpsNote.Models;
using GpsNote.ViewModels;
using Xamarin.Forms.Maps;

namespace GpsNote.Extensions
{
    public static class PinExtensions
    {
        #region -- Pin <=> UserPin --

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

        public static UserPin ToUserPin(this Pin pin)
        {
            UserPin output = null;

            if (pin != null)
            {
                output = new UserPin
                {
                    Label = pin.Label,
                    Address = pin.Address,
                    Latitude = pin.Position.Latitude,
                    Longitude = pin.Position.Longitude
                };
            }

            return output;
        }

        #endregion

        #region -- UserPin <=> PinViewModel --

        public static UserPin ToUsersPin(this PinViewModel pin_view_model)
        {
            UserPin output = null;

            if (pin_view_model != null)
            {
                output = new UserPin
                {
                    Address = pin_view_model.Address,
                    Label = pin_view_model.Label,
                    Latitude = pin_view_model.Position.Latitude,
                    Longitude = pin_view_model.Position.Longitude
                };
            }

            return output;
        }

        public static PinViewModel ToPinViewModel(this UserPin user_pin)
        {
            PinViewModel output = null;

            if (user_pin != null)
            {
                output = new PinViewModel
                {
                    Address = user_pin.Address,
                    Label = user_pin.Label,
                    Position = new Position(user_pin.Latitude, user_pin.Longitude)
                };
            }

            return output;
        }

        #endregion
    }
}
