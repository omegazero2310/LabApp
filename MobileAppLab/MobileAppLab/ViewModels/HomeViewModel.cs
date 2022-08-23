using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppLab.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
        public override void OnNavigatedToAsync(INavigationParameters parameters)
        {
            base.OnNavigatedToAsync(parameters);
        }
    }
}
