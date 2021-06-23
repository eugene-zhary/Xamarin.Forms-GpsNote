using GpsNote.Services;
using GpsNote.Services.Map;
using GpsNote.Services.Permissions;
using GpsNote.Services.Repository;
using GpsNote.Services.Weather;
using GpsNote.ViewModels;
using GpsNote.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;

namespace GpsNote
{
    public partial class App
    {
        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var authManager = Container.Resolve<IAuthorizationService>();

            if (authManager.IsAuthorized)
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(NoteTabbedPage)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignInPage)}");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Services
            containerRegistry.RegisterInstance<IPermissionService>(Container.Resolve<PermissionService>());
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingManager>());
            containerRegistry.RegisterInstance<IWeatherService>(Container.Resolve<OpenWeatherMapWeatherService>());
            containerRegistry.RegisterInstance<IAuthorizationService>(Container.Resolve<AuthorizationService>());
            containerRegistry.RegisterInstance<IPinService>(Container.Resolve<PinService>());

            // Navigations
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<NoteTabbedPage, NoteTabbedViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapViewModel>();
            containerRegistry.RegisterForNavigation<PinsPage, PinsViewModel>();
            containerRegistry.RegisterForNavigation<AddEditPinPage, AddEditPinViewModel>();
            containerRegistry.RegisterDialog<PinInfoDialogPage, PinInfoDialogViewModel>();
        }
    }
}
