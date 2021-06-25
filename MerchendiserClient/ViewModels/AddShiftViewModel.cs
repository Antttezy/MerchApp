using MerchendiserApi.Client;
using MerchendiserApi.Interfaces;
using MerchendiserClient.Commands;
using MerchendiserClient.Models;
using MerchendiserClient.State.Navigators;
using MerchendiserClient.State.Storage;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MerchendiserClient.ViewModels
{
    public class AddShiftViewModel : ViewModelBase
    {
        public ObservableCollection<ShopModel> Shops { get; set; } = new ObservableCollection<ShopModel>();

        public ShopModel SelectedShop { get; set; }

        public ICommand ConfirmStart => new ConfirmStartShiftCommand();
        public ICommand RenavigateCommand => new UpdateCurrentVMCommand(SessionStorage.GetStorage["MainViewModel.Navigator"] as INavigator);

        string Login { get; } = SessionStorage.GetStorage["Login"] as string;
        string Password { get; } = SessionStorage.GetStorage["Password"] as string;

        IShopReader shopReader = new ShopReader();

        private async Task ReadShops()
        {
            try
            {
                var shops = await shopReader.GetShops(Login, Password);

                foreach (var s in shops)
                {
                    Shops.Add(new ShopModel(s));
                }
            }
            catch (Exception)
            {

            }
        }

        public AddShiftViewModel()
        {
            ReadShops();
        }
    }
}
