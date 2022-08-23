using MobileAppLab.ViewModels;
using MobileAppLab.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace MobileAppLab
{
    public partial class App
    {
        public static readonly string API_URL = "http://10.1.11.100:8686/api/";
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<MainTabbedPage, MainTabbedViewModel>();
            containerRegistry.RegisterForNavigation<StaffListingPage, StaffListingViewModel>();
            containerRegistry.RegisterForNavigation<UserAccountPage, UserAccountViewModel>();
            containerRegistry.RegisterForNavigation<UserNotificationPage, UserNotificationViewModel>();
        }
    }
}
