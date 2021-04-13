using GpsNote.Properties;
using GpsNote.Services.Map;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GpsNote.ViewModels
{
    public class PinsViewModel : BasePinsViewModel
    {
        public PinsViewModel(INavigationService navigation, IPinManager pinsManager) : base(navigation, pinsManager)
        {
            Title = AppResources.PinsTitle;
        }

    }
}
