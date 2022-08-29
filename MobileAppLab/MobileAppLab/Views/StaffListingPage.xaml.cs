﻿using System;
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
        private async void SwipeView_SwipeStarted(object sender, SwipeStartedEventArgs e)
        {
            try
            {
                //Console.WriteLine("SwipeView_SwipeStarted");
                if (_swipeViews.Count == 1)
                {
                    _swipeViews[0].Close();
                    _swipeViews.Remove(_swipeViews[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private async void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            try
            {
                //Console.WriteLine("SwipeView_SwipeEnded");
                _swipeViews.Add((SwipeView)sender);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}