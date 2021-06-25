using CoordinatorClient.Models;
using CoordinatorClient.State.Navigators;
using CoordinatorClient.State.Objects;
using System;
using System.Windows.Input;

namespace CoordinatorClient.Commands
{
    class EditShopCommand : ICommand
    {
        private readonly ShopModel viewModel;

        public event EventHandler CanExecuteChanged;

        public EditShopCommand(ShopModel ViewModel)
        {
            this.viewModel = ViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            StateStorage<ShopModel>.Instance.State = viewModel;
            viewModel.ViewModel.Navigator.UpdateCurrentVM.Execute(ViewType.EditShop);
        }
    }
}
