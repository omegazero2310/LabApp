using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileAppLab.ViewModels
{
    public class UserAccountViewModel : ViewModelBase
    {
        private ImageSource _profilePicture;
        public ImageSource ProfilePicture
        {
            get { return _profilePicture; }
            set { SetProperty(ref _profilePicture, value); }
        }
        private DelegateCommand _commandChangeProfilePicture;
        public DelegateCommand CommandName =>
            _commandChangeProfilePicture ?? (_commandChangeProfilePicture = new DelegateCommand(ExecuteChangeProfilePicture));

        
        public UserAccountViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
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
        }
    }
}
