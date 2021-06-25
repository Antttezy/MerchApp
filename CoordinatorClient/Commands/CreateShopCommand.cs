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
    class CreateShopCommand : ICommand
    {
        private readonly AddShopViewModel viewModel;

        public event EventHandler CanExecuteChanged;

        private IShopControlService shopControl = new ApiShopControlService();
        private AuthenticationData authData = AuthenticationData.Instance;

        public CreateShopCommand(AddShopViewModel ViewModel)
        {
            viewModel = ViewModel;
            viewModel.ShopModel.PropertyChanged += ShopModel_PropertyChanged;
        }

        private void ShopModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(viewModel.ShopModel.Address) &&
                   !string.IsNullOrEmpty(viewModel.ShopModel.Name);
        }

        public void Execute(object parameter)
        {
            try
            {
                AsyncHelpers.RunSync(() => shopControl.AddShop(new Authed<Shop>
                {
                    Login = authData.Login,
                    Password = authData.Password,
                    InnerData = new Shop
                    {
                        Name = viewModel.ShopModel.Name,
                        Address = viewModel.ShopModel.Address
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
