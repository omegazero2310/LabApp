using CommonClass.Enums;
using MobileAppLab.ViewModels;
using MobileAppLab.Views;
using Prism;
using Prism.Ioc;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace MobileAppLab
{
    public partial class App
    {
        /// <summary>
        /// đường dẫn đến tệp cơ sở dữ liệu của sqlite 
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        public static string LOCAL_DB_PATH = System.IO.Path.Combine(FileSystem.AppDataDirectory, typeof(App).Assembly.GetName().Name + ".db3");
        /// <summary>
        /// URL của web API
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 18/08/2022 created
        /// </Modified>
        public const string API_URL = "http://10.1.11.100:8686/api/";
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            var assembly = typeof(App).Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
            {
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            }
            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterSingleton<HttpClient>(() => new HttpClient());

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<MainTabbedPage, MainTabbedViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>();
            containerRegistry.RegisterForNavigation<StaffListingPage, StaffListingViewModel>();
            containerRegistry.RegisterForNavigation<UserNotificationPage, UserNotificationViewModel>();
            containerRegistry.RegisterForNavigation<UserAccountPage, UserAccountViewModel>();
        }
    }
}
