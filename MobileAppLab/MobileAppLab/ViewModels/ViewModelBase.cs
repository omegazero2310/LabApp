using MobileAppLab.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileAppLab.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                SetProperty(ref _isBusy, value);
                RaisePropertyChanged();
            }
        }
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }
        private bool _isDisconnected = false;
        public bool IsDisconnected
        {
            get { return _isDisconnected; }
            set { SetProperty(ref _isDisconnected, value, OnDisconnected); }
        }
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            IsDisconnected = Connectivity.NetworkAccess != NetworkAccess.Internet;
            Debug.WriteLine($"NAVIGATION ({this.GetType().Name}):" + this.NavigationService.GetNavigationUriPath());
        }
        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsDisconnected = Connectivity.NetworkAccess != NetworkAccess.Internet;
        }
        private async void OnDisconnected()
        {
            if (IsDisconnected)
            {
                var toastOptions = new ToastOptions();
                var messageOptions = new MessageOptions();
                messageOptions.Message = LocalizationResourceManager.Instance["MSG_DISCONNECTED"];
                messageOptions.Foreground = Color.White;
                toastOptions.BackgroundColor = Color.Red;
                toastOptions.MessageOptions = messageOptions;
                toastOptions.Duration = TimeSpan.MaxValue;
                //await Shell.Current?.CurrentPage?.DisplaySnackBarAsync(toastOptions);
            }

        }
        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedToAsync(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
