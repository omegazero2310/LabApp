using CommonClass.Enums;
using CommonClass.Models;
using MobileAppLab.ApiServices;
using MobileAppLab.Properties;
using MobileAppLab.Utilities;
using Prism;
using Prism.Commands;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using Prism.Services;
using Prism.Services.Dialogs;
using Prism.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;

namespace MobileAppLab.ViewModels
{
    /// <summary>
    /// ViewModel màn hình hiển thị danh sách nhân viên
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="MobileAppLab.ViewModels.ViewModelBase" />
    /// <seealso cref="Prism.IActiveAware" />
    public class StaffListingViewModel : ViewModelBase, IActiveAware
    {
        #region Service      
        private IAdminStaffServices _adminStaffService;
        private IAdminPartServices _adminPartService;
        private IPageDialogService _dialogService;
        private IDialogService _dialog;
        #endregion

        #region các thuộc tính binding
        private static IReadOnlyDictionary<int, string> _staffPositions = new Dictionary<int, string>();
        public ObservableCollection<AdminStaff> Staffs { get; } = new ObservableCollection<AdminStaff>();
        private AdminStaff _selectedStaff;
        public AdminStaff SelectedStaff
        {
            get { return _selectedStaff; }
            set { SetProperty(ref _selectedStaff, value); }
        }
        public event EventHandler IsActiveChanged;
        /// <summary>
        /// kiểm tra xem tab đã được kích hoạt
        /// </summary>
        /// <Modified>
        /// Name Date Comments
        /// annv3 25/08/2022 created
        /// </Modified>
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


        #endregion

        public StaffListingViewModel(INavigationService navigationService, IDialogService dialogService, IPageDialogService pageDialogService,
            IAdminStaffServices adminStaffServices, IAdminPartServices adminPartServices)
            : base(navigationService)
        {
            this._adminStaffService = adminStaffServices;
            this._adminPartService = adminPartServices;
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
                _staffPositions = await this._adminPartService.GetAllAsDictionary();
                this.IsRefreshing = true;
                this.Staffs.Clear();
                var listStaff = await this._adminStaffService.GetAll(isForceRefresh: true);
                foreach (var user in listStaff.OrderBy(staff => staff.StaffName))
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
            if (Convert.ToBoolean(parameters["IsSuccess"]?.ToString() ?? ""))
                this.IsRefreshing = true;
        }
        private void RaiseIsActiveChanged()
        {
            //nếu tab được chọn. load lại list
            if (this.IsActive)
            {
                this.IsRefreshing = true;
            }

        }
        #region các method của command
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
            pairs.Add("Value", parameter.StaffID);
            await this.NavigationService.NavigateAsync("StaffDetailInfoPage", pairs);
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
            pairs.Add("Value", parameter.StaffID);
            await this.NavigationService.NavigateAsync("StaffEditPopupPage", pairs);
        }
        private async void ExecuteCommandSwipeDelete(AdminStaff parameter)
        {
            if (parameter == null)
                return;
            if (await this._dialogService.DisplayAlertAsync(AppResource.MSG_CONFIRM_DELETE_TITLE, string.Format(AppResource.MSG_CONFIRM_DELETE,parameter.StaffID, parameter.StaffName), AppResource.Label_Delete, "Cancel"))
            {
                try
                {
                    await this._adminStaffService.Delete(parameter.StaffID);
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
