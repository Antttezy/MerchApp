using CoordinatorClient.State.Authentication;
using CoordinatorClient.State.Navigators;
using CoordinatorClient.Util;
using CoordinatorClient.ViewModels;
using CoordinatorControls.Services;
using Domain.Core.Models;
using Domain.Services.Interfaces;
using System;
using System.Windows.Input;

namespace CoordinatorClient.Commands
{
    class ConfirmEditShopCommand : ICommand
    {
        private readonly EditShopViewModel viewModel;

        public event EventHandler CanExecuteChanged;

        private IShopControlService shopControl = new ApiShopControlService();
        private AuthenticationData authData = AuthenticationData.Instance;

        public ConfirmEditShopCommand(EditShopViewModel ViewModel)
        {
            viewModel = ViewModel;
            viewModel.Shop.PropertyChanged += Shop_PropertyChanged;
        }

        private void Shop_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(viewModel.Shop.Name) &&
                   !string.IsNullOrEmpty(viewModel.Shop.Address);
        }

        public void Execute(object parameter)
        {
            try
            {
                AsyncHelpers.RunSync(() => shopControl.UpdateShop(new Authed<Shop>
                {
                    Login = authData.Login,
                    Password = authData.Password,
                    InnerData = new Shop
                    {
                        Id = viewModel.Shop.Id,
                        Address = viewModel.Shop.Address,
                        Name = viewModel.Shop.Name
                    }
                }));

                viewModel.Navigator.UpdateCurrentVM.Execute(ViewType.Shops);
            }
            catch (Exception)
            {

            }
        }
    }
}
