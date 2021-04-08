using GpsNote.Properties;
using GpsNote.Services.Map;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;
using GpsNote.Views.Pins;
using System.Windows.Input;
using Xamarin.Forms;
using GpsNote.Extensions;
using System;
using GpsNote.Controls;
using System.Collections.ObjectModel;
using GpsNote.Models;
using System.Linq;
using System.Threading.Tasks;

namespace GpsNote.ViewModels
{
    public class PinsViewModel : ViewModelBase, IViewActionsHandler
    {
        private readonly IPinManager _pinManager;
        public PinsViewModel(INavigationService navigation, IPinManager pinManager) : base(navigation)
        {
            Title = AppResources.PinsTitle;
            _pinManager = pinManager;
            PinsCollection = new ObservableCollection<UserPin>();
        }

        #region -- Public properties --

        public ObservableCollection<UserPin> PinsCollection { get; set; }

        private UserPin _selectedPin;
        public UserPin SelectedPin
        {
            get => _selectedPin;
            set
            {
                SetProperty(ref _selectedPin, value, nameof(SelectedPin));
                NavigateToPin(value);
            }
        }

        public ICommand AddPinCommand => new Command(OnAddPin);
        public ICommand EditPinCommand => new Command(OnEditPin);
        public ICommand NavigateToPinCommand => new Command(OnNavigateToPin);
        public ICommand RemovePinCommand => new Command(OnRemovePin);

        #endregion

        #region -- IViewActionsHandler implementation --

        public async void OnAppearing()
        {
            await UpdatePinsAsync();
        }

        public async void OnDisappearing()
        {
            await SavePinsAsync();
        }

        #endregion

        #region -- Private helpers --

        private async void OnAddPin()
        {
            await NavigationService.NavigateAsync($"{nameof(AddPinView)}");
        }

        private async void OnEditPin(object obj)
        {
            if (obj is UserPin pin)
            {
                await NavigationService.NavigateAsync($"{nameof(AddPinView)}", pin.AsPin().AsNavigationParameters());
            }
        }

        private void OnNavigateToPin(object obj)
        {
            if (obj is UserPin pin)
            {
                NavigateToPin(pin);
            }
        }

        private async void OnRemovePin(object obj)
        {
            if (obj is UserPin pin)
            {
                PinsCollection.Remove(pin);
                await _pinManager.RemovePinAsync(pin);
            }
        }

        private async void NavigateToPin(UserPin pin)
        {
            await NavigationService.SelectTabAsync($"{nameof(MapView)}", pin.AsPin().AsNavigationParameters());
        }

        private async Task UpdatePinsAsync()
        {
            PinsCollection.Clear();
            var pins = await _pinManager.GetPinsAsync();
            pins.ToList().ForEach(PinsCollection.Add);
        }
        private async Task SavePinsAsync()
        {
            foreach (var pin in PinsCollection)
            {
                await _pinManager.SavePinAsync(pin);
            }
        }

        #endregion
    }
}
