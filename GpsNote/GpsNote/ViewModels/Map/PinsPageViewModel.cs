using GpsNote.Extensions;
using GpsNote.Models;
using GpsNote.Services.Map;
using Prism;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace GpsNote.ViewModels
{
    public class PinsPageViewModel : ViewModelBase, IActiveAware 
    {
        private readonly IPinManager _mapManager;

        public PinsPageViewModel(INavigationService navigationService, IPinManager mapManager) : base(navigationService)
        {
            _mapManager = mapManager;

            Title = "Pins";
            UserPins = new ObservableCollection<UserPin>();
        }

        #region -- Public properties --
        public ObservableCollection<UserPin> UserPins { get; set; }
        #endregion


        #region -- IActiveAware implementation --

        public event EventHandler IsActiveChanged;

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value, RaiseIsActiveChanged); }
        }

        protected async void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);

            if(IsActive)
            {
                var pins = await _mapManager.GetPins();

                UserPins.Clear();
                pins.ToList().ForEach(UserPins.Add);
            }
        }

        #endregion
    }
}
