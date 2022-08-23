using Android.Content;
using Android.Graphics.Drawables;
using Android.Text;
using MobileAppLab.CustomControl;
using MobileAppLab.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NoBorderEntry), typeof(NoBorderEntryRendererAndroid))]
namespace MobileAppLab.Droid.Renderers
{
    public class NoBorderEntryRendererAndroid : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.Transparent);
                this.Control.SetBackground(gd);
                this.Control.SetPadding(20, 0, 0, 0);

                NoBorderEntry customEntry = (NoBorderEntry)e.NewElement;
                if (customEntry.IsPasswordFlag)
                {
                    this.Control.InputType = InputTypes.TextVariationVisiblePassword;
                }

            }
        }
        public NoBorderEntryRendererAndroid(Context context) : base(context)
        {
        }
    }
}