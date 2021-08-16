using GpsNote.Resources;
using GpsNote.Services.Map;
using Prism.Navigation;
using GpsNote.Extensions;
using System.Collections.ObjectModel;
using GpsNote.Models;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Generic;
using GpsNote.Interfaces;
using GpsNote.Views;
using Xamarin.CommunityToolkit.ObjectModel;
using Prism.Services;
using System;

namespace GpsNote.ViewModels
{
    public class PinsViewModel : BaseViewModel, IViewActionsHandler
    {
        private readonly IPageDialogService _dialogService;
        private readonly IPinService _pinService;

        public PinsViewModel(
            IPageDialogService dialogService,
            INavigationService navigationService,
            IPinService pinService)
            : base(navigationService)
        {
            _dialogService = dialogService;
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

        private IAsyncCommand _addPinCommand;
        public IAsyncCommand AddPinCommand => _addPinCommand ??= new AsyncCommand(OnAddPinAsync, allowsMultipleExecutions: false);

        private IAsyncCommand<PinModel> _editPinCommand;
        public IAsyncCommand<PinModel> EditPinCommand => _editPinCommand ??= new AsyncCommand<PinModel>(OnEditPinAsync, allowsMultipleExecutions: false);

        private IAsyncCommand<PinModel> _navigateToPinCommand;
        public IAsyncCommand<PinModel> NavigateToPinCommand => _navigateToPinCommand ??= new AsyncCommand<PinModel>(OnNavigateToPinAsync, allowsMultipleExecutions: false);

        private IAsyncCommand<PinModel> _removePinCommand;
        public IAsyncCommand<PinModel> RemovePinCommand => _removePinCommand ??= new AsyncCommand<PinModel>(OnRemovePinAsync, allowsMultipleExecutions: false);

        private IAsyncCommand<PinModel> _checkCommand;
        public IAsyncCommand<PinModel> CheckedCommand => _checkCommand ??= new AsyncCommand<PinModel>(OnCheckedAsync, allowsMultipleExecutions: false);

        private IAsyncCommand _updateCommand;
        public IAsyncCommand UpdateCommand => _updateCommand ??= new AsyncCommand(OnUpdateAsync, allowsMultipleExecutions: false);

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
                await NavigateToPin(SelectedPin);
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

        private async Task OnAddPinAsync()
        {
            await NavigationService.NavigateAsync($"{nameof(AddEditPinPage)}");
        }

        private async Task OnEditPinAsync(PinModel pin)
        {
            var navParams = new NavigationParameters
            {
                { Constants.Navigation.SELECTED_PIN, pin },
            };

            await NavigationService.NavigateAsync($"{nameof(AddEditPinPage)}", navParams);
        }

        private async Task OnNavigateToPinAsync(PinModel pin)
        {
            await NavigateToPin(pin);
        }

        private async Task OnRemovePinAsync(PinModel pin)
        {
            try
            {
                await _pinService.RemovePinAsync(pin);

                PinsCollection.Remove(pin);
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync(Title, ex.Message, Strings.Cancel);
            }
        }

        private async Task OnCheckedAsync(PinModel pin)
        {
            try
            {
                if (pin != null)
                {
                    await _pinService.AddOrUpdatePinAsync(pin);
                }
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync(Title, ex.Message, Strings.Cancel);
            }
        }

        private async Task OnUpdateAsync()
        {
            IsBusy = true;

            await UpdatePinsAsync();

            IsBusy = false;
        }

        private async Task NavigateToPin(PinModel pin)
        {
            if (pin != null)
            {
                var navParams = new NavigationParameters
                {
                    { Constants.Navigation.SELECTED_PIN, pin },
                };

                await NavigationService.SelectTabAsync($"{nameof(MapPage)}", navParams);
            }
        }

        private async Task UpdatePinsAsync(string searchText = null)
        {
            IEnumerable<PinModel> pins = null;

            try
            {
                pins = string.IsNullOrWhiteSpace(searchText)
                    ? await _pinService.GetPinsAsync()
                    : await _pinService.SearchPinsAsync(searchText);
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync(Title, ex.Message, Strings.Cancel);
            }

            PinsCollection = pins != null
                ? new ObservableCollection<PinModel>(pins)
                : new ObservableCollection<PinModel>();
        }

        #endregion
    }
}