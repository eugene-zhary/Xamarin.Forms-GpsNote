using GpsNote.Resources;
using GpsNote.Views;
using Prism.Navigation;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GpsNote.ViewModels
{
    public class NoteTabbedViewModel : BaseViewModel
    {
        public NoteTabbedViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = Strings.NoteTitle;
        }

        #region -- Public properties --

        private IAsyncCommand _logoutCommand;
        public IAsyncCommand LogoutCommand => _logoutCommand ??= new AsyncCommand(OnLogoutAsync, allowsMultipleExecutions: false);

        #endregion

        #region -- Private helpers --

        private async Task OnLogoutAsync()
        {
            Preferences.Clear();

            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInPage)}");
        }

        #endregion
    }
}
