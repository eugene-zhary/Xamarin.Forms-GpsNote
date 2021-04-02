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

        protected override void OnInitialized()
        {
            InitializeComponent();

            var auth_manager = Container.Resolve<IAuthorizationManager>();
            
            if (auth_manager.IsAuthorized)
            {
                //MainPage
                NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignInView)}");
            }
            else
            {
                NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(NoteTabbedView)}");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // services
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<ISettingManager>(Container.Resolve<SettingManager>());

            containerRegistry.RegisterInstance<IAuthorizationManager>(Container.Resolve<AuthorizationManager>());
            containerRegistry.RegisterInstance<IMapManager>(Container.Resolve<MapManager>());


            // navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<NoteTabbedView, NoteTabbedViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
            containerRegistry.RegisterForNavigation<PinsPage, PinsPageViewModel>();
        }
    }
}
