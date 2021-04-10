using GpsNote.Models;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace GpsNote.ViewModels.Dialogs
{
    public class PinInfoDialogViewModel : BindableBase, IDialogAware
    {
        #region -- Public properties --

        private UserPin _pin;
        public UserPin Pin
        {
            get => _pin;
            set => SetProperty(ref _pin, value, nameof(Pin));
        }

        #endregion

        #region -- IDialogAware implementation --

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed() 
        {
        
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey(nameof(UserPin)))
            {
                Pin = parameters.GetValue<UserPin>(nameof(UserPin));
            }
        }

        #endregion
    }
}
