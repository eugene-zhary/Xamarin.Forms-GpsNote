using GpsNote.Properties;
using GpsNote.Services.Map;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using System.Threading.Tasks;
using GpsNote.Services.Permissions;
using Prism.Services;
using Prism.Services.Dialogs;
using GpsNote.Views.Dialogs;
using GpsNote.Extensions;
using System.ComponentModel;
using System;
using GpsNote.Controls;

namespace GpsNote.ViewModels
{
    public class MapViewModel : BaseMapViewModel, IViewActionsHandler
    {
        private readonly IPageDialogService _pageDialogService;
        private readonly IDialogService _dialogService;

        public MapViewModel(INavigationService navigation,
            IPinManager pinManager,
            IPermissionManager permissions,
            IPageDialogService pageDialog,
            IDialogService dialogService) :
        base(navigation, pinManager, permissions)
        {
            _pageDialogService = pageDialog;
            _dialogService = dialogService;

            Title = AppResources.MapTitle;
            SearchText = String.Empty;
        }

        #region -- Public properties --

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value, nameof(SearchText));
        }

        private bool _isSuggestionsVisible;
        public bool IsSuggestionsVisible
        {
            get => _isSuggestionsVisible;
            set => SetProperty(ref _isSuggestionsVisible, value, nameof(IsSuggestionsVisible));
        }

        private Pin _selectedPin;
        public Pin SelectedPin
        {
            get => _selectedPin;
            set => SetProperty(ref _selectedPin, value, nameof(SelectedPin));
        }

        public ICommand PinClickedCommand => new Command<PinClickedEventArgs>(OnPinClicked);
        public ICommand SearchUnfocusedCommand => new Command<FocusEventArgs>(OnSearchUnfocused);

        #endregion

        #region -- IViewActionsHandler implementation --

        public async void OnAppearing()
        {
            await UpdatePinsCollectionAsync();
        }

        public void OnDisappearing()
        {

        }

        #endregion

        #region -- Protected implementation --

        protected override async void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch(args.PropertyName)
            {
                case nameof(SearchText):
                    await UpdateSearch();
                    break;
                case nameof(SelectedPin):
                    UpdateSelectedPin();
                    break;
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if(parameters.ContainsKey(nameof(Pin)))
            {
                var selectedPin = parameters.GetValue<Pin>(nameof(Pin));
                NavigateCamera(selectedPin.Position);
            }
        }

        #endregion

        #region -- Private helpers --

        private void OnPinClicked(PinClickedEventArgs arg)
        {
            if(arg != null && arg.Pin != null)
            {
                NavigateCamera(arg.Pin.Position);
                _dialogService.ShowDialog(nameof(PinInfoDialog), arg.Pin.AsUserPin().AsDialogParams());
            }
        }

        private void OnSearchUnfocused(FocusEventArgs obj)
        {
            IsSuggestionsVisible = false;
        }

        private void UpdateSelectedPin()
        {
            if(SelectedPin != null)
            {
                NavigateCamera(SelectedPin.Position);

                SearchText = String.Empty;
            }
        }

        private async Task UpdateSearch()
        {
            if(!SearchText.Equals(string.Empty))
            {
                await UpdatePinsCollectionAsync(SearchText);

                IsSuggestionsVisible = true;
            }
            else
            {
                await UpdatePinsCollectionAsync();

                IsSuggestionsVisible = false;
            }
        }

        #endregion
    }
}
