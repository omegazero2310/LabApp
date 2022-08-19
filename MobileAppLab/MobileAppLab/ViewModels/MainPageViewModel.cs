using Prism.Commands;
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
        private DelegateCommand<string> _commandPickerLanguageChange;


        
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page Login";
            this.AppVersion = "Version " + this.GetType().Assembly.GetName().Version.ToString();
            if (Application.Current.Properties.ContainsKey("REMEMBER_LOGIN"))
            {
                var isRemember = Application.Current.Properties["REMEMBER_LOGIN"] as bool?;
                
                this.IsSaveLoginInfo = isRemember ?? false;
            }
        }
        private void GetSavedUserLogin()
        {

        }

        private async void ExecuteCommandLogin()
        {

        }
        private async void ExecuteCommandForgotPassword()
        {

        }
        private async void OnPickerLangChange()
        {
            var language = _listLanguages[this.SelectedLanguage];
            //tìm ảnh bắt đầu bằng mã quốc gia kết thúc bằng từ flag
            string prefix = this.GetType().Assembly.GetName().Name + ".AssetImages.";
            string resName = prefix + language.ToLower()+"_flag.png";
            this.ImageLanguage = ImageSource.FromResource(resName);
        }
    }
}
