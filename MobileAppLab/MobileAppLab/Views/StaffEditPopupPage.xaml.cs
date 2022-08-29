using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppLab.Views
{
    /// <summary>
    /// View màn hình chỉnh sửa/ thêm mới nhân viên
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="Rg.Plugins.Popup.Pages.PopupPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StaffEditPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public StaffEditPopupPage()
        {
            InitializeComponent();
        }
    }
}