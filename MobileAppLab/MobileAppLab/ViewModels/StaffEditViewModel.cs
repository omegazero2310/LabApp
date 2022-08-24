using MobileAppLab.ApiServices;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace MobileAppLab.ViewModels
{
    public class StaffEditViewModel : ViewModelBase, IDialogAware
    {
        private AdminStaffService _adminStaffService;
        private IPageDialogService _pageDialog;
        public event Action<IDialogParameters> RequestClose;

        private static Dictionary<string, string> _staffGenders = new Dictionary<string, string>()
                {
                    {"English","en-US" },
                    {"Tiếng Việt","vi-VN"}
                };
        public List<string> ListLanguages { get; } = _staffGenders.Keys.ToList();

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

        private DelegateCommand _commandSave;
        public DelegateCommand CommandSave =>
            _commandSave ?? (_commandSave = new DelegateCommand(ExecuteCommandSave));


        public StaffEditViewModel(INavigationService navigationService, IPageDialogService dialogService, HttpClient httpClient) : base(navigationService)
        {
        }

        private async void ExecuteCommandSave()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await this._pageDialog.DisplayAlertAsync("Save Error", ex.Message, "Ok");
            }
        }

        public bool CanCloseDialog()
        {
            throw new NotImplementedException();
        }

        public void OnDialogClosed()
        {
            throw new NotImplementedException();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
