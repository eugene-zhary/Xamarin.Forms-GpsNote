using GpsNote.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace GpsNote.ViewModels
{
    public class PinViewModel : BindableBase
    {
        #region -- Public properties --

        private string _address;
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value, nameof(Address));
        }

        private string _label;
        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value, nameof(Label));
        }

        private Position _position;
        public Position Position
        {
            get => _position;
            set => SetProperty(ref _position, value, nameof(Position));
        }

        #endregion
    }
}
