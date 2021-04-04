using GpsNote.Models;
using GpsNote.Services;
using GpsNote.Services.Map;
using GpsNote.Services.Repository;
using GpsNote.ViewModels;
using GpsNote.Views;
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
            var auth_manager = Container.Resolve<IAuthorizationManager>();

            if(auth_manager.IsAuthorized)
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
            containerRegistry.Register<IRepository, Repository>();
            containerRegistry.RegisterInstance<ISettingManager>(Container.Resolve<SettingManager>());

            containerRegistry.RegisterInstance<IAuthorizationManager>(Container.Resolve<AuthorizationManager>());
            containerRegistry.RegisterInstance<IPinManager>(Container.Resolve<PinManager>());

            // navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<NoteTabbedView, NoteTabbedViewModel>();

            //containerRegistry.RegisterForNavigation<MapPage>();
            //containerRegistry.RegisterForNavigation<PinsPage>();
        }
    }
}
