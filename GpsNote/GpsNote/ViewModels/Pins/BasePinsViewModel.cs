using GpsNote.Controls;
using GpsNote.Extensions;
using GpsNote.Services;
using GpsNote.Services.Map;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.ViewModels
{
    public class BasePinsViewModel : ViewModelBase, IViewActionsHandler
    {
        private readonly IPinManager _pinManager;
        public BasePinsViewModel(INavigationService navigation, IPinManager pinManager) : base(navigation)
        {
            _pinManager = pinManager;
        }

        #region -- Public properties --
        public ObservableCollection<Pin> PinsCollection { get; set; }
        #endregion 

        #region -- IViewActionsHandler implementation --
        public virtual void OnAppearing() { }
        public virtual void OnDisappearing() { }
        #endregion

        protected async Task UpdatePins()
        {
            PinsCollection.Clear();
            var pins = await _pinManager.GetPinsAsync();
            pins.ToList().ForEach(p => PinsCollection.Add(p.AsPin()));
        }
        protected async Task SavePins()
        {
            foreach (var pin in PinsCollection)
            {
                await _pinManager.SavePinAsync(pin);
            }
        }
    }
}