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
    public class SignUpViewModel : ViewModelBase
    {
        private readonly IPageDialogService _dialogService;
        private readonly IAuthorizationService _authService;

        public SignUpViewModel(
            INavigationService navigationService, 
            IPageDialogService dialogService, 
            IAuthorizationService authService)
            : base(navigationService)
        {
            _dialogService = dialogService;
            _authService = authService;

            Title = Strings.SignUpTitle;
        }

        #region -- Public properties --

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, nameof(Name));
        }

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

        public ICommand CreateCommand => new Command(OnCreate);

        #endregion

        #region -- Private helpers --

        private async void OnCreate()
        {
            if(!UserValidator.Validate(Email, Password, Name))
            {
                await _dialogService.DisplayAlertAsync(Strings.SignUpTitle, Strings.InvalidSignIn, Strings.Cancel);
            }
            else if(await _authService.TrySignUpAsync(Name, Email, Password))
            {
                await _dialogService.DisplayAlertAsync(Strings.SignUpTitle, Strings.SignUpSuccess, Strings.Cancel);
                await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInPage)}");
            }
            else
            {
                await _dialogService.DisplayAlertAsync(Strings.SignUpTitle, Strings.SignUpError, Strings.Cancel);
            }
        }

        #endregion
    }
}
