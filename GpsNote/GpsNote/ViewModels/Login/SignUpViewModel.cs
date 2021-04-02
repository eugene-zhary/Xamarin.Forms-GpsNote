using GpsNote.Helpers;
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
        private IPageDialogService _dialogService;
        private IAuthorizationManager _authManager;

        public SignUpViewModel(INavigationService navigationService, IPageDialogService dialogService, IAuthorizationManager authManager) : base(navigationService)
        {
            Title = "New Account";
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
            if(!UserValidator.IsValid(Email, Password, Name))
            {
                await _dialogService.DisplayAlertAsync(Title, "Invalid email or password!", "Cancel");
            }
            else if(await _authManager.TrySignUp(Name, Email, Password))
            {
                await _dialogService.DisplayAlertAsync(Title, "Account successfuly created!", "Cancel");
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}");
            }
            else
            {
                await _dialogService.DisplayAlertAsync(Title, "This email already exists", "Cancel");
            }
        }

        #endregion
    }
}
