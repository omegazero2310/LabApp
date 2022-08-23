using CommonClass.Models;
using MobileAppLab.ApiServices;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace MobileAppLab.ViewModels
{
    public class StaffListingViewModel : ViewModelBase
    {
        private AdminStaffService _adminStaffService;
        private IPageDialogService _dialogService;

        public ObservableCollection<AdminStaff> Staffs { get; } = new ObservableCollection<AdminStaff>();
        private AdminStaff _selectedStaff;
        public AdminStaff SelectedStaff
        {
            get { return _selectedStaff; }
            set { SetProperty(ref _selectedStaff, value); }
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




        public StaffListingViewModel(INavigationService navigationService, IPageDialogService pageDialogService, HttpClient httpClient) : base(navigationService)
        {
            this._adminStaffService = new AdminStaffService(httpClient);
            this._dialogService = pageDialogService;
        }

        private async void LoadStaffs()
        {
            try
            {
                this.IsBusy = true;
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
                this.IsBusy =false;
            }
        }
        private async void ExecuteCommandLoadData()
        {
           this.LoadStaffs();
        }
        private async void ExecuteCommandSwipeEdit(AdminStaff parameter)
        {

        }
        private async void ExecuteCommandSwipeDelete(AdminStaff parameter)
        {

        }

    }
}
