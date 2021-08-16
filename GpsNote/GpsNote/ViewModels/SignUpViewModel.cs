using GpsNote.Helpers;
using GpsNote.Resources;
using GpsNote.Services;
using GpsNote.Views;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace GpsNote.ViewModels
{
    public class SignUpViewModel : BaseViewModel
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

        private IAsyncCommand _createCommand;
        public IAsyncCommand CreateCommand => _createCommand ??= new AsyncCommand(OnCreateAsync);

        #endregion

        #region -- Private helpers --

        private async Task OnCreateAsync()
        {
            bool isValid = UserValidator.Validate(Email, Password, Name);

            if (isValid)
            {
                try
                {
                    bool isSucced = await _authService.TrySignUpAsync(Name, Email, Password);

                    if (isSucced)
                    {
                        await _dialogService.DisplayAlertAsync(Title, Strings.SignUpSuccess, Strings.Cancel);

                        await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInPage)}");
                    }
                    else
                    {
                        await _dialogService.DisplayAlertAsync(Title, Strings.SignUpError, Strings.Cancel);
                    }
                }
                catch (Exception ex)
                {
                    await _dialogService.DisplayAlertAsync(Title, ex.Message, Strings.Cancel);
                }
            }
            else
            {
                await _dialogService.DisplayAlertAsync(Title, Strings.InvalidSignIn, Strings.Cancel);
            }
        }

        #endregion
    }
}
