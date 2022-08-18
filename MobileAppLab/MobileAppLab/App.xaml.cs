using MobileAppLab.ViewModels;
using MobileAppLab.Views;
using Prism;
using Prism.Ioc;
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
        public const string API_URL = "http://10.1.11.100/api/";
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }
    }
}
