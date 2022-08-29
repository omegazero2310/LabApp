using Xamarin.Forms;

namespace MobileAppLab.CustomControl
{
    /// <summary>
    /// Entry không có dòng kẻ ở dưới
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="Xamarin.Forms.Entry" />
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
