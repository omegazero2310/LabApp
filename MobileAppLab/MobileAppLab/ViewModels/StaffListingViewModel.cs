using CommonClass.Models;
using MobileAppLab.ApiServices;
using MobileAppLab.Properties;
using Prism;
using Prism.Commands;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;

namespace MobileAppLab.ViewModels
{
    public class StaffListingViewModel : ViewModelBase, IActiveAware
    {
        #region Service        
        /// <summary>
        /// Service CRUD bảng admin staff
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
        private AdminStaffService _adminStaffService;
        private IPageDialogService _dialogService;
        private IDialogService _dialog;
        #endregion

        #region các thuộc tính binding
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
        #endregion

        #region các command binding

        /// <summary>
        /// lệnh load dữ liệu
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
        private DelegateCommand _commandLoadData;
        public DelegateCommand CommandLoadData =>
            _commandLoadData ?? (_commandLoadData = new DelegateCommand(ExecuteCommandLoadData));

        /// <summary>
        /// lệnh gạt phải chỉnh sửa
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
        private DelegateCommand<AdminStaff> _commandSwipeEdit;
        public DelegateCommand<AdminStaff> CommandSwipeEdit =>
            _commandSwipeEdit ?? (_commandSwipeEdit = new DelegateCommand<AdminStaff>(ExecuteCommandSwipeEdit));

        /// <summary>
        /// lệnh gạt phải xóa
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
        private DelegateCommand<AdminStaff> _commandSwipeDelete;
        public DelegateCommand<AdminStaff> CommandSwipeDelete =>
            _commandSwipeDelete ?? (_commandSwipeDelete = new DelegateCommand<AdminStaff>(ExecuteCommandSwipeDelete));

        /// <summary>
        /// lệnh chạm để xem chi tiết
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
        private DelegateCommand<AdminStaff> _commandView;
        public DelegateCommand<AdminStaff> CommandView =>
            _commandView ?? (_commandView = new DelegateCommand<AdminStaff>(ExecuteCommandView));

        /// <summary>
        /// lệnh tạo mới nhân viên
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
        private DelegateCommand _commandNewStaff;
        public DelegateCommand CommandNewStaff =>
            _commandNewStaff ?? (_commandNewStaff = new DelegateCommand(ExecuteCommandNewStaff));

        /// <summary>
        /// lệnh quay về màn hình chính (home)
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
        private DelegateCommand _commandBackToHome;
        public DelegateCommand CommandBackToHome =>
            _commandBackToHome ?? (_commandBackToHome = new DelegateCommand(ExecuteCommandBackToHome));

        /// <summary>
        /// lệnh tìm kiếm khi nhập vào control searchbar
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
        private DelegateCommand<object> _commandSearch;
        public DelegateCommand<object> CommandSearch =>
            _commandSearch ?? (_commandSearch = new DelegateCommand<object>(ExecuteCommandSearch));
        #endregion

        public StaffListingViewModel(INavigationService navigationService, IDialogService dialogService, IPageDialogService pageDialogService, HttpClient httpClient) : base(navigationService)
        {
            this._adminStaffService = new AdminStaffService(httpClient);
            this._dialogService = pageDialogService;
            this._dialog = dialogService;
        }
        /// <summary>
        /// Tải dữ liệu nhân viên
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
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
        #region các method của command
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
        #endregion

    }
}
