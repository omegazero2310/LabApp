using CommonClass.Models;
using MobileAppLab.ApiServices;
using MobileAppLab.Properties;
using MobileAppLab.Views;
using Prism;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        private DelegateCommand _commandNewStaff;
        public DelegateCommand CommandNewStaff =>
            _commandNewStaff ?? (_commandNewStaff = new DelegateCommand(ExecuteCommandNewStaff));


        public StaffListingViewModel(INavigationService navigationService,IDialogService dialogService, IPageDialogService pageDialogService, HttpClient httpClient) : base(navigationService)
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
                var list = await this._adminStaffService.GetAll();
                foreach (var user in list)
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
        private void RaiseIsActiveChanged()
        {
        }
        private void ExecuteCommandLoadData()
        {
            this.LoadStaffs();
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
            await this.NavigationService.NavigateAsync("StaffEditPage", null, true, true);
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
