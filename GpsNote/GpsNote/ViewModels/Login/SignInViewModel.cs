using GpsNote.Helpers;
using GpsNote.Properties;
using GpsNote.Services;
using GpsNote.Services.Permissions;
using GpsNote.Views;
using GpsNote.Views.Pins;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GpsNote.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly IPageDialogService _dialogService;
        private readonly IAuthorizationManager _authManager;
        private readonly IPermissionManager _permissionManager;

        public SignInViewModel(INavigationService navigationService, IPageDialogService dialogService, IAuthorizationManager authManager, IPermissionManager permissions) : base(navigationService)
        {
            _dialogService = dialogService;
            _authManager = authManager;
            _permissionManager = permissions;

            _email = String.Empty;
            _password = String.Empty;

            Title = AppResources.SignInTitle;
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
            bool isValid = UserValidator.Validate(Email, Password);

            if(isValid && await _authManager.TrySignInAsync(Email, Password))
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(NoteTabbedView)}");
            }
            else
            {
                await _dialogService.DisplayAlertAsync(AppResources.SignInTitle, AppResources.InvalidSignIn, AppResources.Cancel);
            }
        }

        private async void OnSignUp()
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpView)}");
        }


        #endregion
    }
}
