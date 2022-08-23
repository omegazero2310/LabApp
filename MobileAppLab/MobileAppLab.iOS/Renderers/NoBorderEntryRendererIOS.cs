using CoreGraphics;
using Foundation;
using MobileAppLab.CustomControl;
using MobileAppLab.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(NoBorderEntry), typeof(NoBorderEntryRendererIOS))]
namespace MobileAppLab.iOS.Renderers
{
    public class NoBorderEntryRendererIOS : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            this.Control.LeftView = new UIView(new CGRect(0, 0, 8, this.Control.Frame.Height));
            this.Control.RightView = new UIView(new CGRect(0, 0, 8, this.Control.Frame.Height));
            this.Control.LeftViewMode = UITextFieldViewMode.Always;
            this.Control.RightViewMode = UITextFieldViewMode.Always;

            this.Control.BorderStyle = UITextBorderStyle.None;
            this.Element.HeightRequest = 30;
        }
    }
}