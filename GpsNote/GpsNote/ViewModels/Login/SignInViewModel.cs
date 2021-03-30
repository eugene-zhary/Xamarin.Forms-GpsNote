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
            Title = "Sign In";
            _dialogService = dialogService;
            _authManager = authManager;
        }

        #region -- Public properties --

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

        public ICommand SignUpCommand => new Command(OnSignUp);

        public ICommand SignInCommand => new Command(OnSignIn);

        #endregion

        #region -- Private helpers --

        private async void OnSignIn()
        {
            if (await _authManager.SignIn(this.email, this.password))
            {
                await NavigationService.NavigateAsync($"/{nameof(NoteTabbedView)}");
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
