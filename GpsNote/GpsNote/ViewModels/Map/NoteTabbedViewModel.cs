using GpsNote.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GpsNote.ViewModels
{
    public class NoteTabbedViewModel : ViewModelBase
    {
        public NoteTabbedViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.Title = "Gps Note";
        }

        #region -- Public properties --

        public ICommand LogoutCommand => new Command(OnLogout);


        #endregion

        #region -- Private helpers --

        private async void OnLogout()
        {
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}");
            Preferences.Clear();
        }

        #endregion
    }
}
