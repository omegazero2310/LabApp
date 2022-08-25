using CommonClass.Models;
using MobileAppLab.ApiServices;
using MobileAppLab.Properties;
using MobileAppLab.Views;
using Prism;
using Prism.Commands;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileAppLab.ViewModels
{
    public class StaffListingViewModel : ViewModelBase, IActiveAware
    {
        private AdminStaffService _adminStaffService;
        private IPageDialogService _dialogService;
        private IDialogService _dialog;

        public ObservableCollection<AdminStaff> Staffs { get; } = new ObservableCollection<AdminStaff>();
        private AdminStaff _selectedStaff;
        public AdminStaff SelectedStaff
        {
            get { return _selectedStaff; }
            set { SetProperty(ref _selectedStaff, value); }
        }
        public event EventHandler IsActiveChanged;
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value, RaiseIsActiveChanged); }
        }

        private DelegateCommand _commandLoadData;
        public DelegateCommand CommandLoadData =>
            _commandLoadData ?? (_commandLoadData = new DelegateCommand(ExecuteCommandLoadData));
        private DelegateCommand<AdminStaff> _commandSwipeEdit;
        public DelegateCommand<AdminStaff> CommandSwipeEdit =>
            _commandSwipeEdit ?? (_commandSwipeEdit = new DelegateCommand<AdminStaff>(ExecuteCommandSwipeEdit));

        private DelegateCommand<AdminStaff> _commandSwipeDelete;

        public DelegateCommand<AdminStaff> CommandSwipeDelete =>
            _commandSwipeDelete ?? (_commandSwipeDelete = new DelegateCommand<AdminStaff>(ExecuteCommandSwipeDelete));
        private DelegateCommand<AdminStaff> _commandView;
        public DelegateCommand<AdminStaff> CommandView =>
            _commandView ?? (_commandView = new DelegateCommand<AdminStaff>(ExecuteCommandView));



        private DelegateCommand _commandNewStaff;
        public DelegateCommand CommandNewStaff =>
            _commandNewStaff ?? (_commandNewStaff = new DelegateCommand(ExecuteCommandNewStaff));
        private DelegateCommand _commandBackToHome;
        public DelegateCommand CommandBackToHome =>
            _commandBackToHome ?? (_commandBackToHome = new DelegateCommand(ExecuteCommandBackToHome));
        private DelegateCommand<object> _commandSearch;
        public DelegateCommand<object> CommandSearch =>
            _commandSearch ?? (_commandSearch = new DelegateCommand<object>(ExecuteCommandSearch));

        public StaffListingViewModel(INavigationService navigationService, IDialogService dialogService, IPageDialogService pageDialogService, HttpClient httpClient) : base(navigationService)
        {
            this._adminStaffService = new AdminStaffService(httpClient);
            this._dialogService = pageDialogService;
            this._dialog = dialogService;
        }

        private async void LoadStaffs()
        {
            try
            {
                this.IsRefreshing = true;
                this.Staffs.Clear();
                var listStaff = await this._adminStaffService.GetAll(isForceRefresh: true);
                foreach (var user in listStaff)
                {
                    this.Staffs.Add(user);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                await this._dialogService.DisplayAlertAsync("Load Error", ex.Message, "OK");
            }
            finally
            {
                this.IsRefreshing = false;
            }
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            this.IsRefreshing = true;
        }
        private void RaiseIsActiveChanged()
        {
            this.IsRefreshing = true;
        }
        private async void ExecuteCommandSearch(object parameter)
        {
            if (string.IsNullOrEmpty(parameter?.ToString() ?? ""))
                this.IsRefreshing = true;
            else
            {
                var listStaff = await this._adminStaffService.GetAll();
                var filtered = listStaff.Where(staff => 
                        staff.UserName.Contains(parameter.ToString())
                        || staff.PositionName.Contains(parameter.ToString())
                        || staff.Email.Contains(parameter.ToString())
                        || staff.PhoneNumber.Contains(parameter.ToString())
                        );
                this.Staffs.Clear();
                foreach (var user in filtered)
                {
                    this.Staffs.Add(user);
                }
            }    
        }
        private async void ExecuteCommandBackToHome()
        {
            await this.NavigationService.SelectTabAsync("HomePage");
        }
        private void ExecuteCommandLoadData()
        {
            this.LoadStaffs();
        }
        private async void ExecuteCommandView(AdminStaff parameter)
        {
            if (parameter == null)
                return;
            NavigationParameters pairs = new NavigationParameters();
            pairs.Add("Type", AppResource.Label_Staff_View);
            pairs.Add("Value", parameter.ID);
            await this.NavigationService.NavigateAsync("StaffEditPopupPage", pairs);
        }
        private async void ExecuteCommandNewStaff()
        {
            NavigationParameters pairs = new NavigationParameters();
            pairs.Add("Type", AppResource.Label_Staff_Add);
            pairs.Add("Value", "");
            await this.NavigationService.NavigateAsync("StaffEditPopupPage", pairs);
        }
        private async void ExecuteCommandSwipeEdit(AdminStaff parameter)
        {
            if (parameter == null)
                return;
            NavigationParameters pairs = new NavigationParameters();
            pairs.Add("Type", AppResource.Label_Staff_Update);
            pairs.Add("Value", parameter.ID);
            await this.NavigationService.NavigateAsync("StaffEditPopupPage", pairs);
        }
        private async void ExecuteCommandSwipeDelete(AdminStaff parameter)
        {
            if (parameter == null)
                return;
            if (await this._dialogService.DisplayAlertAsync("Confirm Delete", $"Are you sure to Delete ({parameter.ID} - {parameter.UserName}) ?", "Delete", "Cancel"))
            {
                try
                {
                    await this._adminStaffService.Delete(parameter.ID);
                    this.Staffs.Remove(parameter);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    await this._dialogService.DisplayAlertAsync("Delete Error", ex.Message, "OK");
                }
            }


        }

    }
}
