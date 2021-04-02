using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace GpsNote.Services
{
    public class SettingManager : ISettingManager
    {
        public int UserId
        {
            get => Preferences.Get(nameof(UserId), 0);
            set => Preferences.Set(nameof(UserId), value);
        }
    }
}
