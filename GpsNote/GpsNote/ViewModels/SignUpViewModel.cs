using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace GpsNote.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        #region --- Properties ---

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value, nameof(Name));
        }

        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value, nameof(Email));
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value, nameof(Password));
        }

        #endregion

        public SignUpViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "New Account";
        }

        public ICommand CreateCommand => new Command(()=>
        {
            //TODO: add to database
        });
    }
}
