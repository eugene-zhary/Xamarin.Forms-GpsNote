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
using System;
using Prism;

namespace GpsNote.ViewModels
{
    public class PinsViewModel : ViewModelBase, IViewActionsHandler
    {
        private readonly IPinManager _pinManager;

        public PinsViewModel(INavigationService navigation, IPinManager pinManager) : base(navigation)
        {
            _pinManager = pinManager;

            Title = AppResources.PinsTitle;
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
        public ICommand EditPinCommand => new Command<UserPin>(OnEditPin);
        public ICommand NavigateToPinCommand => new Command<UserPin>(OnNavigateToPin);
        public ICommand RemovePinCommand => new Command<UserPin>(OnRemovePin);
        public ICommand CheckedCommand => new Command<UserPin>(OnCheckedCommand);

        #endregion

        #region -- IViewActionHandler implementation --

        public async void OnAppearing()
        {
            if(_pinManager.IsCollectionUpdated)
            {
                await UpdatePinsAsync();
            }
        }

        public void OnDisappearing() { }

        #endregion

        #region -- Overrides --

        public async override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            await UpdatePinsAsync();
        }

        protected async override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch(args.PropertyName)
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

        private async void OnEditPin(UserPin pin)
        {
            await NavigationService.NavigateAsync($"{nameof(AddEditPinView)}", pin.AsNavigationParameters());
        }

        private void OnNavigateToPin(UserPin pin)
        {
            NavigateToPin(pin);
        }

        private async void OnRemovePin(UserPin pin)
        {
            PinsCollection.Remove(pin);
            await _pinManager.RemovePinAsync(pin);
        }

        private async void OnCheckedCommand(UserPin pin)
        {
            if(pin != null)
            {
                await _pinManager.AddOrUpdatePinAsync(pin);
            }
        }

        private async void NavigateToPin(UserPin pin)
        {
            if(pin != null)
            {
                await NavigationService.SelectTabAsync($"{nameof(MapView)}", pin.AsPin().AsNavigationParameters());
            }
        }

        private async Task UpdatePinsAsync(string searchText = null)
        {
            IEnumerable<UserPin> pins = null;
            PinsCollection.Clear();

            if(searchText == null)
            {
                pins = await _pinManager.GetPinsAsync();
            }
            else
            {
                pins = await _pinManager.SearchPinsAsync(searchText);
            }

            pins.ToList().ForEach(PinsCollection.Add);
        }


        #endregion
    }
}