using GpsNote.Properties;
using GpsNote.Services.Map;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using System.Linq;
using Xamarin.Essentials;
using System.Threading.Tasks;
using GpsNote.Services.Permissions;
using Plugin.Permissions;
using Prism.Services;
using Prism.Services.Dialogs;
using GpsNote.Views.Dialogs;
using GpsNote.Models;
using GpsNote.Extensions;
using GpsNote.Controls;

namespace GpsNote.ViewModels
{
    public class MapViewModel : BaseMapViewModel
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
            PinsCollection = new ObservableCollection<Pin>();
        }

        #region -- Public properties --

        public ICommand PinClickedCommand => new Command<PinClickedEventArgs>(OnPinClicked);

        #endregion

        #region -- IViewActionsHandler implementation --

        public async override void OnAppearing()
        {
            await UpdatePinsCollectionAsync();
        }

        #endregion

        #region -- Private helpers --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if(parameters.ContainsKey(nameof(Pin)))
            {
                var selectedPin = parameters.GetValue<Pin>(nameof(Pin));
                MapCamera = new CameraPosition(selectedPin.Position, 15);
            }
        }

        private void OnPinClicked(PinClickedEventArgs arg)
        {
            if(arg != null)
            {
                MapCamera = new CameraPosition(arg.Pin.Position, 15);
                _dialogService.ShowDialog(nameof(PinInfoDialog), arg.Pin.AsUserPin().AsDialogParams());
            }
        }

        #endregion
    }
}
