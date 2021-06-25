using CoordinatorClient.Models;
using CoordinatorClient.State.Authentication;
using CoordinatorClient.State.Navigators;
using CoordinatorClient.Util;
using CoordinatorControls.Services;
using Domain.Core.Models;
using Domain.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CoordinatorClient.ViewModels
{
    public class ShopsViewModel : ViewModelBase
    {
        public ObservableCollection<ShopModel> Shops { get; set; } = new ObservableCollection<ShopModel>();
        public StatusModel LoadingStatus { get; set; } = new StatusModel();
        public StatusModel ContentStatus { get; set; } = new StatusModel();


        private AuthenticationData authData = AuthenticationData.Instance;
        private IShopControlService shopControlService = new ApiShopControlService();

        public void DeleteShop(int id)
        {
            var del = Shops.FirstOrDefault(s => s.Id == id);
            AsyncHelpers.RunSync(() => shopControlService.RemoveShop(new Authed<Shop>
            {
                Login = authData.Login,
                Password = authData.Password,
                InnerData = new Shop
                {
                    Id = del.Id
                }
            }));

            Shops.Remove(del);
        }

        public INavigator Navigator { get; set; } = State.Navigators.Navigator.Instance;

        private async Task FillCollection()
        {
            LoadingStatus.Visibility = System.Windows.Visibility.Visible;
            LoadingStatus.Status = "Загрузка";
            ContentStatus.Visibility = System.Windows.Visibility.Collapsed;

            try
            {
                var shops = await shopControlService.Shops(
                    new Authed
                    {
                        Login = authData.Login,
                        Password = authData.Password
                    });

                foreach (var s in shops)
                {
                    Shops.Add(new ShopModel(this, s));
                }

                LoadingStatus.Status = "";
                LoadingStatus.Visibility = System.Windows.Visibility.Collapsed;
                ContentStatus.Visibility = System.Windows.Visibility.Visible;
            }
            catch (Exception)
            {
                LoadingStatus.Status = "Ошибка загрузки";
            }
        }

        public ShopsViewModel()
        {
            FillCollection();
        }
    }
}
