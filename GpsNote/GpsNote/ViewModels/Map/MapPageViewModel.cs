using GpsNote.Models;
using GpsNote.Services.Map;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace GpsNote.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private IMapManager _mapManager;

        public MapPageViewModel(INavigationService navigationService, IMapManager mapManager) : base(navigationService)
        {
            Pins = new ObservableCollection<UsersPinViewModel>
            {
                new UsersPinViewModel
                {
                    Pin = new UsersPin
                    {
                        Address = "test",
                        Label = "First",
                        //48.516638, 35.057997
                        Latitude = 48.516638,
                        Longitude = 35.057997
                    }
                },
                new UsersPinViewModel
                {
                    Pin = new UsersPin
                    {
                        Address = "test",
                        Label = "Second",
                        //48.450194, 35.025029
                        Latitude = 48.450194,
                        Longitude = 35.025029
                    }
                },
                new UsersPinViewModel
                {
                    Pin = new UsersPin
                    {
                        Address = "test",
                        Label = "Third",
                        //48.432083, 35.129569
                        Latitude = 48.432083,
                        Longitude = 35.129569
                    }
                }
            };

            Title = "Map";
            _mapManager = mapManager;
        }

        #region -- Public properties --

        public ObservableCollection<UsersPinViewModel> Pins { get; set; }

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
