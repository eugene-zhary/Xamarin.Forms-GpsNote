using GpsNote.Properties;
using GpsNote.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
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
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}");
        }

        #endregion
    }
}
