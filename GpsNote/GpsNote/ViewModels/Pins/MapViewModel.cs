using GpsNote.Properties;
using GpsNote.Services.Map;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.ViewModels
{
    public class MapViewModel : BasePinsViewModel
    {
        public MapViewModel(INavigationService navigation, IPinManager pinManager) : base(navigation, pinManager)
        {
            Title = AppResources.MapTitle;
        }

        #region -- Public properties --

        public ICommand MapClickedCommand => new Command<Position>(OnMapClicked);

        #endregion

        #region -- Private helpers --

        private void OnMapClicked(Position obj)
        {
            PinsCollection.Add(new Pin
            {
                Label = "clicked",
                Address = "default",
                Position = obj
            });
        }

        #endregion
    }
}
