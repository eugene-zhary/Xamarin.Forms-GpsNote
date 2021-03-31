using GpsNote.Helpers;
using GpsNote.Services.Auth;
using GpsNote.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNote.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private IPageDialogService _dialogService;
        private IAuthorizationManager _authManager;

        public SignInViewModel(INavigationService navigationService, IPageDialogService dialogService, IAuthorizationManager authManager) : base(navigationService)
        {
            this._email = String.Empty;
            this._password = String.Empty;

            Title = "Sign In";
            _dialogService = dialogService;
            _authManager = authManager;
        }

        #region -- Public properties --

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value, nameof(Email));
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, nameof(Password));
        }

        public ICommand SignUpCommand => new Command(OnSignUp);

        public ICommand SignInCommand => new Command(OnSignIn);

        #endregion

        #region -- Private helpers --

        private async void OnSignIn()
        {
            string hints = UserValidator.ValidateUser(Email, Password);
            if(hints.Length > 0)
            {
                await _dialogService.DisplayAlertAsync(Title, hints, "Cancel");
                return;
            }

            if (await _authManager.SignIn(this._email, this._password))
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(NoteTabbedView)}");
            }
            else
            {
                await _dialogService.DisplayAlertAsync(Title, "Invalid email or password!", "Cancel");
            }
        }

        private async void OnSignUp()
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpView)}");
        }

        #endregion 
    }
}
