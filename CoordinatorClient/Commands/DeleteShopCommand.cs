using CoordinatorClient.Models;
using System;
using System.Windows.Input;

namespace CoordinatorClient.Commands
{
    class DeleteShopCommand : ICommand
    {
        private readonly ShopModel shop;

        public event EventHandler CanExecuteChanged;

        public DeleteShopCommand(ShopModel shop)
        {
            this.shop = shop;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                shop.ViewModel.DeleteShop(shop.Id);
            }
            catch (Exception)
            {

            }
        }
    }
}
