using System.ComponentModel;
using Xamarin.Forms;

namespace MobileAppLab.Utilities.ViewActions
{
    public class ShowPasswordTriggerAction : TriggerAction<ImageButton>, INotifyPropertyChanged
    {
        public ImageSource ShowIcon { get; set; }
        public ImageSource HideIcon { get; set; }

        bool _hidePassword = true;

        public bool HidePassword
        {
            set
            {
                if (_hidePassword != value)
                {
                    _hidePassword = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HidePassword)));
                }
            }
            get => _hidePassword;
        }

        protected override void Invoke(ImageButton sender)
        {
            sender.Source = HidePassword ? ShowIcon : HideIcon;
            HidePassword = !HidePassword;
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
