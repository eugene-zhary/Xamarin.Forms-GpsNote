using GpsNote.Extensions;
using GpsNote.Services.Map;
using GpsNote.Views;
using Prism.Navigation;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GpsNote.ViewModels
{
    public class NoteTabbedViewModel : ViewModelBase
    {
        private readonly IPinManager _pinManager;

        public NoteTabbedViewModel(INavigationService navigationService, IPinManager pinManager) : base(navigationService)
        {
            _pinManager = pinManager;

            Title = "Gps Note";
            PinsViewModel = new PinsViewModel();
        }

        #region -- Public properties --

        public PinsViewModel PinsViewModel { get; set; }
        public ICommand LogoutCommand => new Command(OnLogout);

        #endregion

        #region -- Private helpers --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var pins = await _pinManager.GetPins();
            pins.ToList().ForEach(p => PinsViewModel.Pins.Add(p.ToPin()));
        }

        private async void OnLogout()
        {
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}");
            Preferences.Clear();
        }

        #endregion
    }
}
