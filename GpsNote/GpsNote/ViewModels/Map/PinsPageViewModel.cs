using Prism.Navigation;

namespace GpsNote.ViewModels
{
    public class PinsPageViewModel : ViewModelBase
    {
        public PinsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Pins";
        }
    }
}
