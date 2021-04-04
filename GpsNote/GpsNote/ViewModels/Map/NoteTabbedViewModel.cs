using GpsNote.Extensions;
using GpsNote.Services.Map;
using GpsNote.Views;
using Prism.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GpsNote.ViewModels
{
    public class NoteTabbedViewModel : ViewModelBase
    {
        private readonly IPinManager _pinManager;

        public NoteTabbedViewModel(INavigationService navigationService, IPinManager pinManager, PinsViewModel pinsViewModel) : base(navigationService)
        {
            _pinManager = pinManager;

            Title = "Gps Note";

            PinsViewModel = pinsViewModel;
        }

        #region -- Public properties --

        private PinsViewModel _pinsViewModel;
        public PinsViewModel PinsViewModel
        {
            get => _pinsViewModel;
            set => SetProperty(ref _pinsViewModel, value, nameof(PinsViewModel));
        }

        public ICommand LogoutCommand => new Command(OnLogout);

        #endregion

        #region -- Private helpers --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await SetCurrentPosition();

            var pins = await _pinManager.GetPins();
            pins.ToList().ForEach(p => PinsViewModel.Pins.Add(p.ToPin()));
        }

        private async void OnLogout()
        {
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInView)}");
            Preferences.Clear();
        }

        private async Task SetCurrentPosition()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();

            try
            {
                if(location != null)
                {
                    var pos = new Position(location.Latitude, location.Longitude);
                    PinsViewModel.MapSpan = new MapSpan(pos, 0.3, 0.3);
                }
            }
            catch(Exception)
            {

            }
        }

        #endregion
    }
}
