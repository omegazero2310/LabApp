using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppLab.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StaffEditPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public StaffEditPopupPage()
        {
            InitializeComponent();
        }
    }
}