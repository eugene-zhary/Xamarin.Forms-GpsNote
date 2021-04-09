using GpsNote.Properties;
using GpsNote.Services.Map;
using Prism.Navigation;
using GpsNote.Views.Pins;
using System.Windows.Input;
using Xamarin.Forms;
using GpsNote.Extensions;
using GpsNote.Controls;
using System.Collections.ObjectModel;
using GpsNote.Models;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Generic;

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

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value, nameof(SearchText));
        }

        private UserPin _selectedPin;
        public UserPin SelectedPin
        {
            get => _selectedPin;
            set => SetProperty(ref _selectedPin, value, nameof(SelectedPin));
        }

        public ICommand AddPinCommand => new Command(OnAddPin);
        public ICommand EditPinCommand => new Command(OnEditPin);
        public ICommand NavigateToPinCommand => new Command(OnNavigateToPin);
        public ICommand RemovePinCommand => new Command(OnRemovePin);
        public ICommand CheckedCommand => new Command(OnCheckedCommand);

        #endregion

        #region -- IViewActionsHandler implementation --

        public async void OnAppearing()
        {
            await UpdatePinsAsync();
        }

        public void OnDisappearing() { }

        #endregion


        #region -- Overrides --
        protected async override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case nameof(SelectedPin):
                    NavigateToPin(SelectedPin);
                    break;
                case nameof(SearchText):
                    await UpdatePinsAsync(SearchText);
                    break;
            }
        }

        #endregion

        #region -- Private helpers --

        private async void OnAddPin()
        {
            await NavigationService.NavigateAsync($"{nameof(AddEditPinView)}");
        }

        private async void OnEditPin(object obj)
        {
            if (obj is UserPin pin)
            {
                await NavigationService.NavigateAsync($"{nameof(AddEditPinView)}", pin.AsNavigationParameters());
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

        private async void OnCheckedCommand(object obj)
        {
            if (obj is UserPin pin)
            {
                await _pinManager.AddOrUpdatePinAsync(pin);
            }
        }

        private async void NavigateToPin(UserPin pin)
        {
            await NavigationService.SelectTabAsync($"{nameof(MapView)}", pin.AsPin().AsNavigationParameters());
        }

        private async Task UpdatePinsAsync(string searchQuery = null)
        {
            PinsCollection.Clear();
            IEnumerable<UserPin> pins = null;
            
            if (searchQuery == null)
            {
                pins = await _pinManager.GetPinsAsync();
            }
            else
            {
                pins = await _pinManager.GetPinsWithQueryAsync(searchQuery);
            }

            pins.ToList().ForEach(PinsCollection.Add);
        }

        private async Task SavePinsAsync()
        {
            foreach (var pin in PinsCollection)
            {
                await _pinManager.AddOrUpdatePinAsync(pin);
            }
        }

        #endregion
    }
}
