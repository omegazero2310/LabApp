using MobileAppLab.Properties;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace MobileAppLab.Utilities
{
    public class LocalizationResourceManager : INotifyPropertyChanged
    {
        public static LocalizationResourceManager Instance { get; } = new LocalizationResourceManager();

        public string this[string text]
        {
            get
            {
                return AppResource.ResourceManager.GetString(text, AppResource.Culture);
            }
        }

        public void SetCulture(CultureInfo language)
        {
            Thread.CurrentThread.CurrentUICulture = language;
            AppResource.Culture = language;

            Invalidate();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Invalidate()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
    }
}
