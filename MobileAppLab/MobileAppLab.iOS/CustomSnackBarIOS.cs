using Foundation;
using MobileAppLab.iOS;
using MobileAppLab.Utilities;
using UIKit;
[assembly: Xamarin.Forms.Dependency(typeof(CustomSnackBarIOS))]
namespace MobileAppLab.iOS
{
    public class CustomSnackBarIOS : ICustomSnackBar
    {
        const double LONG_DELAY = 3.5;
        NSTimer alertDelay;
        UIViewController alert;

        const float DialogWith = 160;
        public void SnackbarShow(string message)
        {
            ShowAlert(message, LONG_DELAY);
        }
        void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                dismissMessage();
            });
            alert = new UIViewController();

            UILabel view = new UILabel();
            int DeviceWidth = (int)UIScreen.MainScreen.Bounds.Width;

            float position = (DeviceWidth - DialogWith) / 2;

            view.Frame = new CoreGraphics.CGRect(position, 0, DialogWith, 100);
            view.Text = message;

            // you can customize  the style as you want
            view.TextColor = UIColor.Red;
            view.BackgroundColor = UIColor.Yellow;

            alert.View.Add(view);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }
        void dismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}