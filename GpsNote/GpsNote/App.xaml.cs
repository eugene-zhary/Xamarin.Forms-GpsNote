using GpsNote.Models;
using GpsNote.Services.Auth;
using GpsNote.Services.Repository;
using GpsNote.ViewModels;
using GpsNote.ViewModels.Map;
using GpsNote.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GpsNote
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            
            if (Preferences.Get("UserId", 0) == 0)
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(SignInView)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(NoteTabbedView)}");
            }
        }
        
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // services
            containerRegistry.Register(typeof(IRepository<>), typeof(Repository<>));
            containerRegistry.RegisterInstance<IAuthorizationManager>(Container.Resolve<AuthorizationManager>());

            // navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<NoteTabbedView, NoteTabbedViewModel>();
            containerRegistry.RegisterForNavigation<MapView, MapViewModel>();
            containerRegistry.RegisterForNavigation<PinsView, PinsViewModel>();
        }
    }
}
