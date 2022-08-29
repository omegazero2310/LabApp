using CommonClass.Enums;
using CommonClass.Models;
using MobileAppLab.ApiServices;
using MobileAppLab.Properties;
using MobileAppLab.Utilities;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MobileAppLab.ViewModels
{
    public class StaffInfoDetailViewModel : ViewModelBase
    {
        private AdminStaffServices _adminStaffService;
        private AdminPartServices _adminPartService;
        public event Action<IDialogParameters> RequestClose;

        public Dictionary<string, string> ErrorMessages { get; set; }

        private readonly static IReadOnlyDictionary<string, GenderOptions> _staffGenders = new Dictionary<string, GenderOptions>
                {
                    {LocalizationResourceManager.Instance[nameof(AppResource.Gender_Male)],GenderOptions.Male },
                    {LocalizationResourceManager.Instance[nameof(AppResource.Gender_Female)],GenderOptions.Female },
                    {LocalizationResourceManager.Instance[nameof(AppResource.Gender_Other)],GenderOptions.Other },
                };
        public List<string> StaffGenders { get; } = _staffGenders.Keys.ToList();
        public int? ID { get; set; }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }

        private string _positionName;
        public string PositionName
        {
            get { return _positionName; }
            set { SetProperty(ref _positionName, value); }
        }

        private string _emailAddress;
        public string EmailAddress
        {
            get { return _emailAddress; }
            set { SetProperty(ref _emailAddress, value); }
        }
        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value); }

        }

        private DelegateCommand _commandCancel;
        public DelegateCommand CommandCancel =>
            _commandCancel ?? (_commandCancel = new DelegateCommand(ExecuteCommandCancel));




        public StaffInfoDetailViewModel(INavigationService navigationService, HttpClient httpClient) : base(navigationService)
        {
            this._adminStaffService = new AdminStaffServices(httpClient);
            this._adminPartService = new AdminPartServices(httpClient);
        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters == null)
                return;
            else
            {
                if (parameters["Type"]?.ToString() == AppResource.Label_Staff_View)
                {
                    this.Title = AppResource.Label_Staff_View;
                    await this.LoadStaffInfo((int)parameters["Value"]);
                }
            }
        }
        private async Task LoadStaffInfo(int id)
        {
            AdminStaff adminStaff = await this._adminStaffService.GetByID(id);
            var listParts = await this._adminPartService.GetAllAsDictionary();
            if (adminStaff != null)
            {
                listParts.TryGetValue(adminStaff.PartID, out var part);
                this.ID = adminStaff.ID;
                this.UserName = adminStaff.UserName;
                this.Address = adminStaff.Address;
                this.PhoneNumber = adminStaff.PhoneNumber;
                this.PositionName = part;
                this.EmailAddress = adminStaff.Email;
                this.Gender = _staffGenders.Where(pos => pos.Value == adminStaff.Gender).FirstOrDefault().Key;
            }
        }
        private async void ExecuteCommandCancel()
        {
            await this.NavigationService.GoBackAsync();
        }

    }
}
