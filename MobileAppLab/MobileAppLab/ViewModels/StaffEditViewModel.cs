using CommonClass.Enums;
using CommonClass.Models;
using MobileAppLab.ApiServices;
using MobileAppLab.Properties;
using Prism.Commands;
using Prism.Mvvm;
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
    public class StaffEditViewModel : ViewModelBase
    {
        private AdminStaffService _adminStaffService;
        private IPageDialogService _pageDialog;
        public event Action<IDialogParameters> RequestClose;

        private static Dictionary<string, GenderOptions> _staffGenders = new Dictionary<string, GenderOptions>()
                {
                    {AppResource.Gender_Male,GenderOptions.Male },
                    {AppResource.Gender_Female,GenderOptions.Female },
                    {AppResource.Gender_Other,GenderOptions.Other },
                };
        public List<string> ListStaffGenders { get; } = _staffGenders.Keys.ToList();

        private static Dictionary<string, PositionOptions> _staffPositions = new Dictionary<string, PositionOptions>()
                {
                    {AppResource.Position_NV,PositionOptions.NV },
                    {AppResource.Position_TP,PositionOptions.TP },
                    {AppResource.Position_GD,PositionOptions.GD },
                };
        public List<string> ListStaffPositions { get; } = _staffPositions.Keys.ToList();

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
        private DelegateCommand _commandCancel;
        public DelegateCommand CommandCancel   =>
            _commandCancel ?? (_commandCancel = new DelegateCommand(ExecuteCommandCancel));

        


        public StaffEditViewModel(INavigationService navigationService, HttpClient httpClient) : base(navigationService)
        {
            this._adminStaffService = new AdminStaffService(httpClient);
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters == null)
                return;
            else
            {
                if (parameters["Type"]?.ToString() == AppResource.Label_Staff_Add)
                {
                    this.Title = AppResource.Label_Staff_Add;
                }    
            }    
        }
        private async void ExecuteCommandSave()
        {
            try
            {
                AdminStaff adminStaff = new AdminStaff();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await this._pageDialog.DisplayAlertAsync("Save Error", ex.Message, "Ok");
            }
            finally
            {
                await this.NavigationService.GoBackAsync();
            }
        }
        private async void ExecuteCommandCancel()
        {
            await this.NavigationService.GoBackAsync();
        }

    }
}
