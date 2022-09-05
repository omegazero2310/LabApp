using Android.App;
using Google.Android.Material.Snackbar;
using MobileAppLab.Droid;
using MobileAppLab.Utilities;
using Plugin.CurrentActivity;
[assembly: Xamarin.Forms.Dependency(typeof(CustomSnackBarAndroid))]
namespace MobileAppLab.Droid
{
    public class CustomSnackBarAndroid : ICustomSnackBar
    {
        public void SnackbarShow(string message)
        {
            Activity activity = CrossCurrentActivity.Current.Activity;
            Android.Views.View view = activity.FindViewById(Android.Resource.Id.Content);
            Snackbar.Make(view, message, Snackbar.LengthLong).Show();
        }
    }
}