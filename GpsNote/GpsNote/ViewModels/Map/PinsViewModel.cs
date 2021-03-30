using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GpsNote.ViewModels
{
    public class PinsViewModel : ViewModelBase
    {
        public PinsViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Pins";
        }
    }
}
