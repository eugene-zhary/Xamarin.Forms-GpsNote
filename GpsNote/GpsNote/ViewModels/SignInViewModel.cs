using GpsNote.Helpers;
using GpsNote.Resources;
using GpsNote.Services;
using GpsNote.Views;
using Prism.Navigation;
using Prism.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNote.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly IPageDialogService _dialogService;
        private readonly IAuthorizationService _authService;

        public SignInViewModel(
            INavigationService navigationService,
            IPageDialogService dialogService,
            IAuthorizationService authService) 
            : base(navigationService)
        {
            _dialogService = dialogService;
            _authService = authService;

            _email = string.Empty;
            _password = string.Empty;

            Title = Strings.SignInTitle;
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

            if(isValid && await _authService.TrySignInAsync(Email, Password))
            {
                await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(NoteTabbedPage)}");
            }
            else
            {
                await _dialogService.DisplayAlertAsync(Strings.SignInTitle, Strings.InvalidSignIn, Strings.Cancel);
            }
        }

        private async void OnSignUp()
        {
            await _navigationService.NavigateAsync($"{nameof(SignUpPage)}");
        }

        #endregion
    }
}
