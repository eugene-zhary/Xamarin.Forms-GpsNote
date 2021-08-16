using GpsNote.Helpers;
using GpsNote.Resources;
using GpsNote.Services;
using GpsNote.Views;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace GpsNote.ViewModels
{
    public class SignInViewModel : BaseViewModel
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

        private IAsyncCommand _signUpCommand;
        public IAsyncCommand SignUpCommand => _signUpCommand ??= new AsyncCommand(OnSignUpAsync, allowsMultipleExecutions: false);

        private IAsyncCommand _signInCommand;
        public IAsyncCommand SignInCommand => _signInCommand ??= new AsyncCommand(OnSignInAsync, allowsMultipleExecutions: false);

        #endregion

        #region -- Private helpers --

        private async Task OnSignInAsync()
        {
            bool isValid = UserValidator.Validate(Email, Password);

            if (isValid)
            {
                try
                {
                    bool isSucced = await _authService.TrySignInAsync(Email, Password);

                    if (isSucced)
                    {
                        await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(NoteTabbedPage)}");
                    }
                    else
                    {
                        await _dialogService.DisplayAlertAsync(Title, Strings.InvalidSignIn, Strings.Cancel);
                    }
                }
                catch (System.Exception ex)
                {
                    await _dialogService.DisplayAlertAsync(Title, ex.Message, Strings.Cancel);
                }
            }
            else
            {
                await _dialogService.DisplayAlertAsync(Title, Strings.InvalidSignIn, Strings.Cancel);
            }
        }

        private async Task OnSignUpAsync()
        {
            await NavigationService.NavigateAsync($"{nameof(SignUpPage)}");
        }

        #endregion
    }
}
