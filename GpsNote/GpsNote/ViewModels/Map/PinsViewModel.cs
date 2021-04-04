using GpsNote.Extensions;
using GpsNote.Models;
using GpsNote.Services.Map;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace GpsNote.ViewModels
{
    public class PinsViewModel : BindableBase
    {
        private readonly IPinManager _pinManager;

        public PinsViewModel(IPinManager pinManager)
        {
            _pinManager = pinManager;

            MapTitle = "Map";
            PinsTitle = "Pins";

            Pins = new ObservableCollection<Pin>();
            MapSpan = new MapSpan(new Position(), 0.01, 0.01);
        }

        #region -- Public properties --

        public ObservableCollection<Pin> Pins { get; set; }

        private MapSpan _mapSpan;
        public MapSpan MapSpan
        {
            get => _mapSpan;
            set => SetProperty(ref _mapSpan, value, nameof(MapSpan));
        }

        private string _mapTitle;
        public string MapTitle
        {
            get => _mapTitle;
            set => SetProperty(ref _mapTitle, value, nameof(MapTitle));
        }

        private string _pinsTitle;
        public string PinsTitle
        {
            get => _pinsTitle;
            set => SetProperty(ref _pinsTitle, value, nameof(PinsTitle));
        }

        public ICommand MapClickedCommand => new Command<Position>(OnMapClicked);

        #endregion

        #region -- Private helpers -- 

        private void OnMapClicked(Position obj)
        {
            var position = obj;
        }

        #endregion
    }
}
