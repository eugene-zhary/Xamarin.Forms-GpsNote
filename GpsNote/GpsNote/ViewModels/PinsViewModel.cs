using GpsNote.Resources;
using GpsNote.Services.Map;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using GpsNote.Extensions;
using System.Collections.ObjectModel;
using GpsNote.Models;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Generic;
using GpsNote.Interfaces;
using GpsNote.Views;

namespace GpsNote.ViewModels
{
    public class PinsViewModel : ViewModelBase, IViewActionsHandler
    {
        private readonly IPinService _pinService;

        public PinsViewModel(
            INavigationService navigationService,
            IPinService pinService)
            : base(navigationService)
        {
            _pinService = pinService;

            Title = Strings.PinsTitle;
            PinsCollection = new ObservableCollection<PinModel>();
        }

        #region -- Public properties --

        private ObservableCollection<PinModel> _pinsCollection;
        public ObservableCollection<PinModel> PinsCollection
        {
            get => _pinsCollection;
            set => SetProperty(ref _pinsCollection, value, nameof(PinsCollection));
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value, nameof(SearchText));
        }

        private PinModel _selectedPin;
        public PinModel SelectedPin
        {
            get => _selectedPin;
            set => SetProperty(ref _selectedPin, value, nameof(SelectedPin));
        }

        public ICommand AddPinCommand => new Command(OnAddPin);
        public ICommand EditPinCommand => new Command<PinModel>(OnEditPin);
        public ICommand NavigateToPinCommand => new Command<PinModel>(OnNavigateToPin);
        public ICommand RemovePinCommand => new Command<PinModel>(OnRemovePin);
        public ICommand CheckedCommand => new Command<PinModel>(OnCheckedCommand);

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

            if (args.PropertyName == nameof(SelectedPin))
            {
                NavigateToPin(SelectedPin);
            }
            else if (args.PropertyName == nameof(SearchText))
            {
                await UpdatePinsAsync(SearchText);
            }
        }

        #endregion

        #region -- IViewActionHandler implementation --

        public async void OnAppearing()
        {
            if (_pinService.IsCollectionUpdated)
            {
                await UpdatePinsAsync();
            }
        }

        #endregion

        #region -- Private helpers --

        private async void OnAddPin()
        {
            await _navigationService.NavigateAsync($"{nameof(AddEditPinPage)}");
        }

        private async void OnEditPin(PinModel pin)
        {
            var navParams = new NavigationParameters
            {
                { Constants.Navigation.SELECTED_PIN, pin },
            };

            await _navigationService.NavigateAsync($"{nameof(AddEditPinPage)}", navParams);
        }

        private void OnNavigateToPin(PinModel pin)
        {
            NavigateToPin(pin);
        }

        private async void OnRemovePin(PinModel pin)
        {
            PinsCollection.Remove(pin);
            await _pinService.RemovePinAsync(pin);
        }

        private async void OnCheckedCommand(PinModel pin)
        {
            if (pin != null)
            {
                await _pinService.AddOrUpdatePinAsync(pin);
            }
        }

        private async void NavigateToPin(PinModel pin)
        {
            if (pin != null)
            {
                var navParams = new NavigationParameters
                {
                    { Constants.Navigation.SELECTED_PIN, pin },
                };

                await _navigationService.SelectTabAsync($"{nameof(MapPage)}", navParams);
            }
        }

        private async Task UpdatePinsAsync(string searchText = null)
        {
            IEnumerable<PinModel> pins;

            if (searchText == null || searchText.Equals(string.Empty))
            {
                pins = await _pinService.GetPinsAsync();
            }
            else
            {
                pins = await _pinService.SearchPinsAsync(searchText);
            }

            PinsCollection = new ObservableCollection<PinModel>(pins);
        }

        #endregion
    }
}