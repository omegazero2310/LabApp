using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Resources;
using MobileAppLab.Properties;
using MobileAppLab.Utilities;
using MobileAppLab.ApiServices;
using System.Net.Http;
using Prism.Services;

namespace MobileAppLab.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private AdminUserServices _adminUserServices;
        private IPageDialogService _pageDialogService;
        private string _appVersion;
        public string AppVersion
        {
            get { return _appVersion; }
            set { SetProperty(ref _appVersion, value); }
        }
        private bool _isSaveLoginInfo;
        public bool IsSaveLoginInfo
        {
            get { return _isSaveLoginInfo; }
            set { SetProperty(ref _isSaveLoginInfo, value); }
        }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        private string _language = "EN";
        public string SelectedLanguage
        {
            get { return _language; }
            set { SetProperty(ref _language, value, OnPickerLangChange); }
        }
        private ImageSource imageSource;
        public ImageSource ImageLanguage
        {
            get { return imageSource; }
            set { SetProperty(ref imageSource, value); }
        }

        private static Dictionary<string, string> _listLanguages = new Dictionary<string, string>()
                {
                    {"English","en-US" },
                    {"Tiếng Việt","vi-VN"}
                };
        public List<string> ListLanguages { get; } = _listLanguages.Keys.ToList();

        private DelegateCommand _commandLogin;
        public DelegateCommand CommandLogin =>
            _commandLogin ?? (_commandLogin = new DelegateCommand(ExecuteCommandLogin));
        private DelegateCommand _commandForgotPassword;
        public DelegateCommand CommandForgotPassword =>
            _commandForgotPassword ?? (_commandForgotPassword = new DelegateCommand(ExecuteCommandForgotPassword));



        public LoginViewModel(INavigationService navigationService, IPageDialogService pageDialog, HttpClient httpClient)
            : base(navigationService)
        {
            this._pageDialogService = pageDialog;
            Title = "Main Page Login";
            this.AppVersion = this.GetType().Assembly.GetName().Version.ToString();
            this._adminUserServices = new AdminUserServices(httpClient);
           
        }
        ~LoginViewModel()
        {

        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            this.GetSavedUserLogin();
        }

        public override void OnNavigatedToAsync(INavigationParameters parameters)
        {
            base.OnNavigatedToAsync(parameters);
        }
        private void GetSavedUserLogin()
        {
            if (Preferences.ContainsKey("REMEMBER_LOGIN"))
            {
                this.IsSaveLoginInfo = Preferences.Get("REMEMBER_LOGIN", false);
                if (this.IsSaveLoginInfo)
                {
                    this.UserName = SecureStorage.GetAsync("USER_NAME").Result;
                    this.Password = SecureStorage.GetAsync("USER_PASSWORD").Result;
                }
            }
            else
                Preferences.Set("REMEMBER_LOGIN", false);

            if (!Preferences.ContainsKey("LANGUAGE"))
            {
                Preferences.Set("LANGUAGE", "en-US");
                this.SelectedLanguage = _listLanguages.Where(val => val.Value == "en-US").FirstOrDefault().Key ?? "English";
            }
            else
            {
                string language = Preferences.Get("LANGUAGE", "en-US");
                this.SelectedLanguage = _listLanguages.Where(val => val.Value == language).FirstOrDefault().Key ?? "English";
            }
        }

        private async void ExecuteCommandLogin()
        {
            try
            {
                this.IsBusy = true;
                var result = await this._adminUserServices.Login(this.UserName, this.Password);

                var language = _listLanguages[this.SelectedLanguage];
                if (Preferences.ContainsKey("LANGUAGE"))
                    Preferences.Set("LANGUAGE", language);
                else
                    Preferences.Set("LANGUAGE", "en-US");
                if (result.Item1)//nếu login thành công
                {
                    //Lưu lại thông tin đăng nhập nếu tích vào checkbox
                    if (!string.IsNullOrEmpty(this.UserName) && !string.IsNullOrEmpty(this.Password))
                    {
                        if (IsSaveLoginInfo)
                        {
                            await SecureStorage.SetAsync("USER_NAME", this.UserName);
                            await SecureStorage.SetAsync("USER_PASSWORD", this.Password);

                            if (Preferences.ContainsKey("REMEMBER_LOGIN"))
                                Preferences.Set("REMEMBER_LOGIN", IsSaveLoginInfo);
                            else
                                Preferences.Set("REMEMBER_LOGIN", false);
                        }

                    }
                    //chuyển sang trang chủ
                    await this.NavigationService.NavigateAsync("MainTabbedPage", ("USER_ID", this.UserName));
                }
                else
                {
                    //thông báo lỗi đăng nhập
                    await this._pageDialogService.DisplayAlertAsync("Login Error", "Login Failed: " + result.Item2, "Ok");
                }
            }
            catch (Exception ex)
            {
                await this._pageDialogService.DisplayAlertAsync("Login Error", "Login Failed: " + ex.Message, "Ok");
            }
            finally
            {
                this.IsBusy = false;
            }

        }
        private async void ExecuteCommandForgotPassword()
        {

        }
        private async void OnPickerLangChange()
        {
            var language = _listLanguages[this.SelectedLanguage];
            LocalizationResourceManager.Instance.SetCulture(new System.Globalization.CultureInfo(language));
            if (Preferences.ContainsKey("LANGUAGE"))
                Preferences.Set("LANGUAGE", language);
            //tìm ảnh bắt đầu bằng mã quốc gia kết thúc bằng từ flag
            string prefix = this.GetType().Assembly.GetName().Name + ".AssetImages.";
            string resName = prefix + language.ToLower() + "_flag.png";
            this.ImageLanguage = ImageSource.FromResource(resName);
        }
    }
}
