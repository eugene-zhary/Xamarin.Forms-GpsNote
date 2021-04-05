using GpsNote.Services.Map;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.ViewModels
{
    public class MapViewModel : BasePinsViewModel
    {
        public MapViewModel(INavigationService navigation, IPinManager pinManager) : base(navigation, pinManager)
        {
            Title = "Map";
        }

        #region -- Public properties --

        public ICommand MapClickedCommand => new Command<Position>(OnMapClicked);

        #endregion

        #region -- Private helpers --

        private void OnMapClicked(Position obj)
        {
            
        }

        #endregion
    }
}
