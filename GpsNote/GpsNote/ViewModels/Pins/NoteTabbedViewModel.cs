﻿using GpsNote.Properties;
using GpsNote.Views;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GpsNote.ViewModels
{
    public class NoteTabbedViewModel : ViewModelBase
    {
        public NoteTabbedViewModel(INavigationService navigation) : base(navigation)
        {
            Title = AppResources.NoteTitle;
        }

        #region -- Public properties --

        public ICommand LogoutCommand => new Command(OnLogout);

        #endregion

        #region -- Private helpers --

        private async void OnLogout(object obj)
        {
            Preferences.Clear();

            await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}");
        }

        #endregion
    }
}
