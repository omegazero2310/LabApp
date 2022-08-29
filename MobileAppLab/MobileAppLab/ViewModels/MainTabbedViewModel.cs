using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppLab.ViewModels
{
    /// <summary>
    /// ViewModel Màn hình chính
    /// </summary>
    /// <Modified>
    /// Name Date Comments
    /// annv3 29/08/2022 created
    /// </Modified>
    /// <seealso cref="MobileAppLab.ViewModels.ViewModelBase" />
    public class MainTabbedViewModel : ViewModelBase
    {
        private DelegateCommand _commandTabbedPage;
        public DelegateCommand CommandTabbedPage =>
            _commandTabbedPage ?? (_commandTabbedPage = new DelegateCommand(ExecuteCommandTabbedPage));

        
        public MainTabbedViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
        private async void ExecuteCommandTabbedPage()
        {

        }
    }
}
