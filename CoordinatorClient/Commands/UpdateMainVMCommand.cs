using CoordinatorClient.State.Navigators;
using CoordinatorClient.ViewModels;
using System;
using System.Windows.Input;

namespace CoordinatorClient.Commands
{
    class UpdateMainVMCommand : ICommand
    {
        private INavigator navigator;

        public UpdateMainVMCommand(INavigator navigator)
        {
            this.navigator = navigator;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType viewType)
            {
                switch (viewType)
                {
                    case ViewType.Authentication:
                        navigator.CurrentViewModel = new AuthenticationViewModel();
                        break;

                    case ViewType.Main:
                        navigator.CurrentViewModel = new MainViewModel();
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
