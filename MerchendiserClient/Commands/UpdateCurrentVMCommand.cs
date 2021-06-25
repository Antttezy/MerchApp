using MerchendiserClient.State.Navigators;
using MerchendiserClient.ViewModels;
using System;
using System.Windows.Input;

namespace MerchendiserClient.Commands
{
    class UpdateCurrentVMCommand : ICommand
    {
        private readonly INavigator navigator;

        public UpdateCurrentVMCommand(INavigator navigator)
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
                        navigator.CurrentViewModel = new LoginViewModel();
                        break;
                    case ViewType.Shifts:
                        navigator.CurrentViewModel = new ShiftsViewModel();
                        break;
                    case ViewType.AddShift:
                        navigator.CurrentViewModel = new AddShiftViewModel();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
