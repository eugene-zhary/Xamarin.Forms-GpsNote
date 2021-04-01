using GpsNote.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace GpsNote.ViewModels
{
    public class UsersPinViewModel : BindableBase
    {
        #region -- Public properties --

        private UsersPin _pin;
        public UsersPin Pin
        {
            get => _pin;
            set => SetProperty(ref _pin, value, nameof(Pin));
        }

        public Position PinPosition => new Position(_pin.Latitude, _pin.Longitude);

        #endregion
    }
}
