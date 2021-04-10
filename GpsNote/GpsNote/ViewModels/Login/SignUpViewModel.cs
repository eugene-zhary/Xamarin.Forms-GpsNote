using GpsNote.Helpers;
using GpsNote.Properties;
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
        private readonly IAuthorizationManager _authManager;

        public SignUpViewModel(INavigationService navigationService, IPageDialogService dialogService, IAuthorizationManager authManager) : base(navigationService)
        {
            Title = AppResources.SignUpTitle;
            _dialogService = dialogService;
            _authManager = authManager;
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
                await _dialogService.DisplayAlertAsync(AppResources.SignUpTitle, AppResources.InvalidSignIn, AppResources.Cancel);
            }
            else if(await _authManager.TrySignUpAsync(Name, Email, Password))
            {
                await _dialogService.DisplayAlertAsync(AppResources.SignUpTitle, AppResources.SignUpSuccess, AppResources.Cancel);
                await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}");
            }
            else
            {
                await _dialogService.DisplayAlertAsync(AppResources.SignUpTitle, AppResources.SignUpError, AppResources.Cancel);
            }
        }

        #endregion
    }
}
