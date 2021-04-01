using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

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
