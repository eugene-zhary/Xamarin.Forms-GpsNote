using GpsNote.Controls;
using GpsNote.Services.Map;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GpsNote.ViewModels
{
    public abstract class BasePinsViewModel : ViewModelBase, IViewActionsHandler
    {
        protected readonly IPinManager _pinManager;
        protected readonly INavigationService _navigationService;

        public BasePinsViewModel(INavigationService navigation, IPinManager pinManager) : base(navigation)
        {
            _pinManager = pinManager;
            _navigationService = navigation;
            PinsCollection = new ObservableCollection<Pin>();
        }

        #region -- Public properties --

        public ObservableCollection<Pin> PinsCollection { get; set; }

        #endregion

        #region -- Overrides --

        public virtual void OnAppearing()
        {
            
        }

        public virtual void OnDisappearing()
        {

        }

        #endregion


    }
}
