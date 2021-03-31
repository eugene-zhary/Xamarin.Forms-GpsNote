using GpsNote.Controls;
using GpsNote.Models;
using GpsNote.Services.Map;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;


namespace GpsNote.ViewModels
{
    public class MapViewModel : ViewModelBase
    {
        private IMapManager _mapManager;

        public MapViewModel(INavigationService navigationService, IMapManager mapManager) : base(navigationService)
        {
            Title = "Map";
            _mapManager = mapManager;

            Pins = new ObservableCollection<UsersPin>
            {
                new UsersPin
                {
                    Label = "First",
                    Address="test",
                    Latitude = 48.464965,
                    Longitude= 35.049669
                }
            };
        }

        #region -- Public properties --

        public ObservableCollection<UsersPin> Pins { get; set; }
        public ICommand AddPinCommand => new Command(OnAddPin);

        #endregion


        #region -- Private helpers --

        private void OnAddPin(object obj)
        {
            // TODO: add pins
        }

        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            // TODO: read pins from database
        }

        #endregion
    }
}
