using CommonClass.Models.Request;
using MobileAppLab.ApiServices;
using MobileAppLab.Properties;
using MobileAppLab.Views;
using Newtonsoft.Json;
using Prism;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileAppLab.ViewModels
{
    public class UserAccountViewModel : ViewModelBase, IActiveAware
    {
        private AdminStaffService _adminStaffService;
        private IPageDialogService _pageDialogService;
        private ImageSource _profilePicture = ImageSource.FromResource("MobileAppLab.AssetImages.icon_default_profile_pic.png");
        public ImageSource ProfilePicture
        {
            get { return _profilePicture; }
            set { SetProperty(ref _profilePicture, value); }
        }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }
        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value, RaiseIsActiveChanged); }
        }



        private DelegateCommand _commandChangeProfilePicture;

        public event EventHandler IsActiveChanged;

        public DelegateCommand CommandName =>
            _commandChangeProfilePicture ?? (_commandChangeProfilePicture = new DelegateCommand(ExecuteChangeProfilePicture));
        private DelegateCommand _commandLogout;
        public DelegateCommand CommandLogout =>
            _commandLogout ?? (_commandLogout = new DelegateCommand(ExecuteCommandLogout));




        public UserAccountViewModel(INavigationService navigationService, HttpClient httpClient, IPageDialogService pageDialog) : base(navigationService)
        {
            this._adminStaffService = new AdminStaffService(httpClient);
            this._pageDialogService = pageDialog;
        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            RaiseIsActiveChanged();
        }
        public async void RaiseIsActiveChanged()
        {
            if (this.IsActive)
            {
                string jsonToken = SecureStorage.GetAsync("JWT").Result;
                var token = JsonConvert.DeserializeObject<UserTokens>(jsonToken);
                //load dữ liệu
                var userInfo = await this._adminStaffService.GetByID(token.Id);
                this.UserName = userInfo?.UserName ?? "PlaceHolder Name";
                this.PhoneNumber = userInfo?.PhoneNumber ?? "PlaceHolder PhoneNumber";
                var userProfilePicture = await this._adminStaffService.GetProfilePicture(token.Id);
                if (userProfilePicture.IsSuccessStatusCode)
                {
                    this.ProfilePicture = StreamImageSource.FromStream(() => userProfilePicture.Content.ReadAsStreamAsync().Result);
                }
            }

        }
        private async void ExecuteChangeProfilePicture()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }
        private async Task LoadPhotoAsync(FileResult photo)
        {
            string photoPath = "";
            // trường hợp bị hủy
            if (photo == null)
            {
                photoPath = null;
                return;
            }
            // Lưu tạm ảnh vào bộ nhớ điện thoại
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            photoPath = newFile;

            //Lấy ảnh upload lên API
            //lấy id user đăng nhập 
            string jsonToken = await SecureStorage.GetAsync("JWT");
            var token = JsonConvert.DeserializeObject<UserTokens>(jsonToken);
            //tải ảnh lên api
            var result = this._adminStaffService.UploadProfilePicture(token.Id, photoPath);
            //chỉ ảnh tới đường dẫn mới
        }

        private async void ExecuteCommandLogout()
        {
            if (await this._pageDialogService.DisplayAlertAsync(AppResource.Account_Confirm_Logout_Title, AppResource.Account_Confirm_Logout_Msg, "Ok", "Cancel"))
            {
                try
                {

                    AdminUserServices.Logout();
                    await this.NavigationService.NavigateAsync("app:///NavigationPage/LoginPage", null, false, false);


                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }
    }
}
