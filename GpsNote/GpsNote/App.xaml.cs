using GpsNote.Models;
using GpsNote.Services;
using GpsNote.Services.Map;
using GpsNote.Services.Permissions;
using GpsNote.Services.Repository;
using GpsNote.ViewModels;
using GpsNote.ViewModels.Dialogs;
using GpsNote.Views;
using GpsNote.Views.Dialogs;
using GpsNote.Views.Pins;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GpsNote
{
    public partial class App
    { 

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            var authManager = Container.Resolve<IAuthorizationManager>();

            if(authManager.IsAuthorized)
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(NoteTabbedView)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignInView)}");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // services
            containerRegistry.RegisterInstance<IPermissionManager>(Container.Resolve<PermissionManager>());
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingManager>());
            containerRegistry.RegisterInstance<IAuthorizationManager>(Container.Resolve<AuthorizationManager>());
            containerRegistry.RegisterInstance<IPinManager>(Container.Resolve<PinManager>());

            // navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<NoteTabbedView, NoteTabbedViewModel>();
            containerRegistry.RegisterForNavigation<MapView, MapViewModel>();
            containerRegistry.RegisterForNavigation<PinsView, PinsViewModel>();
            containerRegistry.RegisterForNavigation<AddEditPinView, AddEditPinViewModel>();

            // dialogs
            containerRegistry.RegisterDialog<PinInfoDialog, PinInfoDialogViewModel>();
        }
    }
}
