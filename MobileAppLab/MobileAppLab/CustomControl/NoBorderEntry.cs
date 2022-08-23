using Xamarin.Forms;

namespace MobileAppLab.CustomControl
{
    public class NoBorderEntry :Entry
    {
        public static readonly BindableProperty IsPasswordFlagProperty =
        BindableProperty.Create("IsPasswordFlag", typeof(bool), typeof(NoBorderEntry), defaultBindingMode: BindingMode.OneWay);
        public bool IsPasswordFlag
        {
            get { return (bool)GetValue(IsPasswordFlagProperty); }
            set { SetValue(IsPasswordFlagProperty, value); }
        }
    }
}
