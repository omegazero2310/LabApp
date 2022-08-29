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
    public partial class StaffListingPage : ContentPage
    {
        private List<SwipeView> _swipeViews { set; get; } = new List<SwipeView>();
        public StaffListingPage()
        {
            InitializeComponent();
        }
        private void SwipeView_SwipeStarted(object sender, SwipeStartedEventArgs e)
        {
            //Console.WriteLine("SwipeView_SwipeStarted");

            if (_swipeViews.Count == 1)
            {
                _swipeViews[0].Close();
                _swipeViews.Remove(_swipeViews[0]);
            }
        }

        private void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            //Console.WriteLine("SwipeView_SwipeEnded");
            _swipeViews.Add((SwipeView)sender);
        }
    }
}