using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppLab.ViewModels
{
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
