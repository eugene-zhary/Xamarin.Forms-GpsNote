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
    public class SignInViewModel : ViewModelBase
    {
        #region --- Properties ---

        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value, nameof(Email));
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value, nameof(Password));
        }

        #endregion

        public SignInViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Sign In";
        }

        public ICommand SignUpCommand => new Command(async() =>
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpView)}");
        });

        public ICommand SignInCommand => new Command(() =>
        {
            //TODO: verify account
        });
    }
}
