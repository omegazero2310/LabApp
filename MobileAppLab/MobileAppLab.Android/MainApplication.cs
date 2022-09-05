using System;
using Android.App;
using Android.Runtime;

namespace MobileAppLab.Droid
{
#if DEBUG
    [Application(
        Theme = "@style/MainTheme",
        Debuggable = true
        )]
#else
    [Application(
        Theme = "@style/MainTheme",
        Debuggable = false
        )]
#endif

    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Xamarin.Essentials.Platform.Init(this);
        }
    }
}
