using Foundation;
using MobileAppLab.Utilities;
using Prism;
using Prism.Ioc;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.XForms.iOS.Core;
using Syncfusion.XForms.iOS.TextInputLayout;
using UIKit;


namespace MobileAppLab.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.Init();
            SfAvatarViewRenderer.Init();
            SfListViewRenderer.Init();
            SfTextInputLayoutRenderer.Init();
            SfBusyIndicatorRenderer.Init();
            LoadApplication(new App(new iOSInitializer()));
            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
            containerRegistry.Register<ICustomSnackBar, CustomSnackBarIOS>();
        }
    }
}
