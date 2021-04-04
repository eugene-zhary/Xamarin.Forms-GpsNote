using Prism.Mvvm;
using System.Collections.ObjectModel;
using Xamarin.Forms.Maps;

namespace GpsNote.ViewModels
{
    public class PinsViewModel : BindableBase
    {
        public PinsViewModel()
        {
            Pins = new ObservableCollection<Pin>();
            Pins.Add(new Pin
            {
                Label = "First",
                Address = "test",
                Position = new Position(48.513395, 35.054189)
            });
            Pins.Add(new Pin
            {
                Label = "Second",
                Address = "test",
                Position = new Position(48.455651, 35.017595)
            });
            Pins.Add(new Pin
            {
                Label = "Third",
                Address = "test",
                Position = new Position(48.435000, 35.129087)
            });
        }


        #region -- Public properties --

        public ObservableCollection<Pin> Pins { get; set; }

        #endregion
    }
}
