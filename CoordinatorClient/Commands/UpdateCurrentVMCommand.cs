using CoordinatorClient.State.Navigators;
using CoordinatorClient.ViewModels;
using System;
using System.Windows.Input;

namespace CoordinatorClient.Commands
{
    class UpdateCurrentVMCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private INavigator navigator;

        public UpdateCurrentVMCommand(INavigator navigator)
        {
            this.navigator = navigator;
        }

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
                    case ViewType.Merches:
                        navigator.CurrentViewModel = new MerchesViewModel();
                        break;
                    case ViewType.Shops:
                        navigator.CurrentViewModel = new ShopsViewModel();
                        break;
                    case ViewType.AddShop:
                        navigator.CurrentViewModel = new AddShopViewModel();
                        break;
                    case ViewType.EditShop:
                        navigator.CurrentViewModel = new EditShopViewModel();
                        break;
                    case ViewType.Shifts:
                        navigator.CurrentViewModel = new WorkshiftsViewModel();
                        break;
                    case ViewType.AddMerch:
                        navigator.CurrentViewModel = new AddMerchViewModel();
                        break;
                    case ViewType.EditMerch:
                        navigator.CurrentViewModel = new EditMerchViewModel();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
