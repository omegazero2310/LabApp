using CommonClass.Enums;
using CommonClass.ErrorCodes;
using CommonClass.Models;
using CommonClass.Models.Request;
using CommonClass.Validations;
using DryIoc;
using MobileAppLab.ApiServices;
using MobileAppLab.Properties;
using MobileAppLab.Utilities;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
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
using Xamarin.Forms;

namespace MobileAppLab.ViewModels
{
    /// <summary>
    /// ViewModel màn hình thêm và sửa thông tin nhân viên
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="MobileAppLab.ViewModels.ViewModelBase" />
    public class StaffEditViewModel : ViewModelBase
    {
        #region các Service
        private AdminStaffServices _adminStaffService;
        private AdminPartServices _adminPartService;
        private IPageDialogService _pageDialog;
        #endregion

        #region các Dictionary cho picker
        public Dictionary<string, string> ErrorMessages { get; set; }

        private readonly static IReadOnlyDictionary<string, GenderOptions> _staffGenders = new Dictionary<string, GenderOptions>
                {
                    {LocalizationResourceManager.Instance[nameof(AppResource.Gender_Male)],GenderOptions.Male },
                    {LocalizationResourceManager.Instance[nameof(AppResource.Gender_Female)],GenderOptions.Female },
                    {LocalizationResourceManager.Instance[nameof(AppResource.Gender_Other)],GenderOptions.Other },
                };
        public List<string> StaffGenders { get; } = _staffGenders.Keys.ToList();
        public ObservableCollection<AdminParts> StaffPositions { get; } = new ObservableCollection<AdminParts>();
        #endregion

        #region các thuộc tính binding
        private AdminParts _selectedStaffPosition;
        /// <summary>
        /// chức danh được chọn
        /// </summary>
        /// <value>
        /// The selected staff position.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public AdminParts SelectedStaffPosition
        {
            get { return _selectedStaffPosition; }
            set { SetProperty(ref _selectedStaffPosition, value); }
        }
        private bool _isEdit;
        /// <summary>
        /// Chế độ chỉnh sửa của dòng
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is edit mode; otherwise, <c>false</c>.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public bool IsEditMode
        {
            get { return _isEdit; }
            set { SetProperty(ref _isEdit, value); }
        }
        /// <summary>
        /// ID của nhân viên được chỉnh sửa, nếu null thì sẽ thêm mới
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public int? ID { get; set; }
        private string _userName;
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _address;
        /// <summary>
        /// địa chỉ
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        private string _phoneNumber;
        /// <summary>
        /// Số điện thoại
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }

        private string _positionName;
        /// <summary>
        /// tên chức danh
        /// </summary>
        /// <value>
        /// The name of the position.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public string PositionName
        {
            get { return _positionName; }
            set { SetProperty(ref _positionName, value); }
        }

        private string _emailAddress;
        /// <summary>
        /// địa chỉ email
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public string EmailAddress
        {
            get { return _emailAddress; }
            set { SetProperty(ref _emailAddress, value); }
        }
        private string _gender;
        /// <summary>
        /// giới tính
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        /// <Modified>
        /// Name Date Comments
        /// annv3 29/08/2022 created
        /// </Modified>
        public string Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value); }
        }
        #endregion

        #region Command
        private DelegateCommand _commandSave;
        public DelegateCommand CommandSave =>
            _commandSave ?? (_commandSave = new DelegateCommand(ExecuteCommandSave));
        private DelegateCommand _commandCancel;
        public DelegateCommand CommandCancel =>
            _commandCancel ?? (_commandCancel = new DelegateCommand(ExecuteCommandCancel));

        private DelegateCommand<object> _commandOpenPicker;
        public DelegateCommand<object> CommandOpenPicker =>
            _commandOpenPicker ?? (_commandOpenPicker = new DelegateCommand<object>(ExecuteCommandOpenPicker));

        
        #endregion



        #region Contructor
        public StaffEditViewModel(INavigationService navigationService, HttpClient httpClient, IPageDialogService pageDialogService) : base(navigationService)
        {
            this._adminStaffService = new AdminStaffServices(httpClient);
            _adminPartService = new AdminPartServices(httpClient);
            this._pageDialog = pageDialogService;
        }
        #endregion

        #region override method
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters == null)
                return;
            else
            {
                if (parameters["Type"]?.ToString() == AppResource.Label_Staff_Add)
                {
                    this.Title = AppResource.Label_Staff_Add;
                    this.ID = null;
                    this.IsEditMode = true;
                    IEnumerable<AdminParts> parts = await this._adminPartService.GetAll();
                    this.StaffPositions.Clear();
                    foreach (var part in parts)
                    {
                        this.StaffPositions.Add(part);
                    }
                }
                else if (parameters["Type"]?.ToString() == AppResource.Label_Staff_Update)
                {
                    this.Title = AppResource.Label_Staff_Update;
                    this.IsEditMode = true;
                    this.ID = (int)parameters["Value"];
                    await this.LoadStaffInfo((int)parameters["Value"]);
                }
                else if (parameters["Type"]?.ToString() == AppResource.Label_Staff_View)
                {
                    this.Title = AppResource.Label_Staff_View;
                    this.IsEditMode = false;
                    await this.LoadStaffInfo((int)parameters["Value"]);
                }
            }
        }
        #endregion

        #region Command menthod
        private async Task LoadStaffInfo(int id)
        {
            AdminStaff adminStaff = await this._adminStaffService.GetByID(id);
            IEnumerable<AdminParts> parts = await this._adminPartService.GetAll();
            this.StaffPositions.Clear();
            foreach (var part in parts)
            {
                this.StaffPositions.Add(part);
            }
            if (adminStaff != null)
            {
                this.ID = adminStaff.StaffID;
                this.UserName = adminStaff.StaffName;
                this.Address = adminStaff.Address;
                this.PhoneNumber = adminStaff.PhoneNumber;
                this.SelectedStaffPosition = this.StaffPositions.Where(part => part.PartID == adminStaff.PartID).FirstOrDefault();
                this.EmailAddress = adminStaff.Email;
                this.Gender = _staffGenders.Where(pos => pos.Value == adminStaff.Gender).FirstOrDefault().Key;
            }
        }
        private async void ExecuteCommandSave()
        {
            try
            {
                this.ErrorMessages = new Dictionary<string, string>();



                AdminStaff adminStaff = new AdminStaff();
                if (this.IsEditMode && this.ID.HasValue)
                    adminStaff.StaffID = this.ID.Value;
                adminStaff.StaffName = this.UserName;
                adminStaff.Address = this.Address;
                adminStaff.PhoneNumber = this.PhoneNumber;
                if (this.SelectedStaffPosition == null)
                {
                    this.ErrorMessages.Add("POSITION_ID", LocalizationResourceManager.Instance[nameof(AppResource.MSG_POSITION_NOT_VALID)]);
                    RaisePropertyChanged(nameof(ErrorMessages));
                }
                else
                    adminStaff.PartID = this.SelectedStaffPosition.PartID;
                if (string.IsNullOrEmpty(this.Gender))
                {
                    this.ErrorMessages.Add("GENDER", LocalizationResourceManager.Instance[nameof(AppResource.MSG_GENDER_NOT_VALID)]);
                    RaisePropertyChanged(nameof(ErrorMessages));
                }
                else
                    adminStaff.Gender = _staffGenders[this.Gender];
                adminStaff.Email = this.EmailAddress;
                AdminStaffValidator validationRules = new AdminStaffValidator();
                var resultValidate = validationRules.Validate(adminStaff);
                if (!resultValidate.IsValid || this.ErrorMessages.Count > 0)
                {
                    foreach (var e in resultValidate.Errors)
                    {
                        ErrorMessages[e.PropertyName] = LocalizationResourceManager.Instance[e.ErrorMessage];
                    }
                    RaisePropertyChanged(nameof(ErrorMessages));
                    return;

                }
                else
                {
                    ServerRespone serverRespone = null;
                    if (IsEditMode && this.ID.HasValue)
                        serverRespone = await this._adminStaffService.Update(adminStaff);
                    else
                        serverRespone = await this._adminStaffService.CreateNew(adminStaff);
                    if (serverRespone.IsSuccess && (serverRespone.Message == "Updated" || serverRespone.Message == "Created"))
                    {
                        NavigationParameters valuePairs = new NavigationParameters();
                        valuePairs.Add("IsSuccess", true);
                        await this.NavigationService.GoBackAsync(valuePairs);
                    }
                    else if (serverRespone.IsSuccess && serverRespone.Message == "NoChange")
                    {
                        await this.NavigationService.GoBackAsync();
                    }
                    else if (!serverRespone.IsSuccess)
                    {
                        if (serverRespone.Message == AdminStaffErrorCode.DUPLICATE_EMAIL)
                        {
                            ErrorMessages["Email"] = LocalizationResourceManager.Instance[AdminStaffErrorCode.DUPLICATE_EMAIL];
                            RaisePropertyChanged(nameof(ErrorMessages));
                        } 
                        else if(serverRespone.Message == AdminStaffErrorCode.DUPLICATE_PHONE_NUMBER)
                        {
                            ErrorMessages["PhoneNumber"] = LocalizationResourceManager.Instance[AdminStaffErrorCode.DUPLICATE_PHONE_NUMBER];
                            RaisePropertyChanged(nameof(ErrorMessages));
                        }    
                        else
                        {
                            await this._pageDialog.DisplayAlertAsync("Save Error", serverRespone.Message, "Ok");
                        }    
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await this._pageDialog.DisplayAlertAsync("Save Error", ex.Message, "Ok");
            }
            finally
            {

            }
        }
        private async void ExecuteCommandCancel()
        {
            await this.NavigationService.GoBackAsync();
        }
        void ExecuteCommandOpenPicker(object parameter)
        {
            try
            {
                var picker = parameter as Picker;

                picker.IsEnabled = true;
                picker.IsVisible = true;
                picker.Focus();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        #endregion

    }
}
