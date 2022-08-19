﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileAppLab.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
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
                    {"English","US" },
                    {"Tiếng Việt","VN"}
                };
        public List<string> ListLanguages { get; } = _listLanguages.Keys.ToList();

        private DelegateCommand _commandLogin;
        public DelegateCommand CommandLogin =>
            _commandLogin ?? (_commandLogin = new DelegateCommand(ExecuteCommandLogin));
        private DelegateCommand _commandForgotPassword;
        public DelegateCommand CommandForgotPassword =>
            _commandForgotPassword ?? (_commandForgotPassword = new DelegateCommand(ExecuteCommandForgotPassword));



        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page Login";
            this.AppVersion = "Version " + this.GetType().Assembly.GetName().Version.ToString();
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
                Preferences.Set("LANGUAGE", "EN");
                this.SelectedLanguage = _listLanguages.Where(val => val.Value == "EN").FirstOrDefault().Key ?? "English";
            }
            else
            {
                string language = Preferences.Get("LANGUAGE", "EN");
                this.SelectedLanguage = _listLanguages.Where(val => val.Value == language).FirstOrDefault().Key ?? "English";
            }


        }
        ~MainPageViewModel()
        {

        }
        private void GetSavedUserLogin()
        {

        }

        private async void ExecuteCommandLogin()
        {
            bool isLoginSuccess = true;
            var language = _listLanguages[this.SelectedLanguage];
            if (Preferences.ContainsKey("LANGUAGE"))
                Preferences.Set("LANGUAGE", language);
            else
                Preferences.Set("LANGUAGE", "EN");

            if (isLoginSuccess && !string.IsNullOrEmpty(this.UserName) && !string.IsNullOrEmpty(this.Password))
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

        }
        private async void ExecuteCommandForgotPassword()
        {

        }
        private async void OnPickerLangChange()
        {
            var language = _listLanguages[this.SelectedLanguage];
            if (Preferences.ContainsKey("LANGUAGE"))
                Preferences.Set("LANGUAGE", language);
            //tìm ảnh bắt đầu bằng mã quốc gia kết thúc bằng từ flag
            string prefix = this.GetType().Assembly.GetName().Name + ".AssetImages.";
            string resName = prefix + language.ToLower() + "_flag.png";
            this.ImageLanguage = ImageSource.FromResource(resName);
        }
    }
}
