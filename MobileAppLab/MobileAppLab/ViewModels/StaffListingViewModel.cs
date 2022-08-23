using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppLab.ViewModels
{
    public class StaffListingViewModel : ViewModelBase
    {
        public StaffListingViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
        public override void OnNavigatedToAsync(INavigationParameters parameters)
        {
            base.OnNavigatedToAsync(parameters);
        }
    }
}
