using CommonClass.Models;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SwipeEndedEventArgs = Xamarin.Forms.SwipeEndedEventArgs;
using SwipeStartedEventArgs = Xamarin.Forms.SwipeStartedEventArgs;
using SwipeView = Xamarin.Forms.SwipeView;

namespace MobileAppLab.Utilities.ViewActions
{
    internal class SyncFusionFilterBehavior : Behavior<ContentPage>
    {
        #region Fields

        private SfListView _listView;
        private Entry _searchBar;
        #endregion

        #region Overrides
        protected override void OnAttachedTo(ContentPage bindable)
        {
            _listView = bindable.FindByName<SfListView>("ItemsListView");
            _searchBar = bindable.FindByName<Entry>("srcBar");
            _searchBar.TextChanged += SearchBar_TextChanged;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            _searchBar.TextChanged -= SearchBar_TextChanged;
            _searchBar = null;
            _listView = null;
            base.OnDetachingFrom(bindable);
        }
        #endregion

        #region Methods

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_listView.DataSource != null)
            {
                _listView.DataSource.Filter = FilterContacts;
                _listView.DataSource.RefreshFilter();
            }
            _listView.RefreshView();
        }

        private bool FilterContacts(object obj)
        {
            if (_searchBar == null || _searchBar.Text == null)
                return true;

            var staff = obj as AdminStaff;
            return (staff.StaffName.ToLower().Contains(_searchBar.Text.ToLower())
                || staff.PositionName.ToLower().Contains(_searchBar.Text.ToLower()))
                || staff.PhoneNumber.Contains(_searchBar.Text.ToLower())
                || staff.Email.ToLower().Contains(_searchBar.Text.ToLower());
        }
        
        #endregion
    }
}
