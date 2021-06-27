﻿using GpsNote.Resources;
using GpsNote.Services.Map;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using GpsNote.Services.Permissions;
using Prism.Services;
using Prism.Services.Dialogs;
using GpsNote.Extensions;
using System.ComponentModel;
using GpsNote.Interfaces;
using GpsNote.Views;
using GpsNote.Models;

namespace GpsNote.ViewModels
{
    public class MapViewModel : BaseMapViewModel, IViewActionsHandler
    {
        private readonly IDialogService _dialogService;

        public MapViewModel(
            INavigationService navigationService,
            IPinService pinService,
            IPermissionService permissionService,
            IPageDialogService pageDialogService,
            IDialogService dialogService)
            : base(navigationService, pinService, permissionService, pageDialogService)
        {
            _dialogService = dialogService;

            Title = Strings.MapTitle;
        }

        #region -- Public properties --

        private Pin _selectedPin;
        public Pin SelectedPin
        {
            get => _selectedPin;
            set => SetProperty(ref _selectedPin, value, nameof(SelectedPin));
        }

        public ICommand PinClickedCommand => new Command<PinClickedEventArgs>(OnPinClicked);

        #endregion

        #region -- Overrides --
        
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == nameof(UpdateSelectedPin))
            {
                UpdateSelectedPin();
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(Constants.Navigation.SELECTED_PIN, out PinModel pin))
            {
                var selectedPin = pin.ToPin();

                NavigateCamera(selectedPin.Position);
            }
        }

        #endregion

        #region -- IViewActionsHandler implementation --

        public async void OnAppearing()
        {
            await UpdatePinsCollectionAsync();
        }

        #endregion

        #region -- Private helpers --

        private void OnPinClicked(PinClickedEventArgs arg)
        {
            if (arg != null && arg.Pin != null)
            {
                NavigateCamera(arg.Pin.Position);

                var navParams = new DialogParameters
                {
                    { Constants.Navigation.SELECTED_PIN, arg.Pin.ToPinModel() }
                };

                _dialogService.ShowDialog(nameof(PinInfoDialogPage), navParams);
            }
        }

        private void UpdateSelectedPin()
        {
            if (SelectedPin != null)
            {
                NavigateCamera(SelectedPin.Position);
            }
        }

        #endregion
    }
}
