using CoordinatorClient.Commands;
using CoordinatorClient.ViewModels;
using Domain.Core.Models;
using System.Windows.Input;

namespace CoordinatorClient.Models
{
    public class ShopModel : ObservableObject
    {
        private readonly Shop shop;
        public ShopsViewModel ViewModel { get; }

        public ShopModel(Shop shop)
        {
            this.shop = shop;
            ViewModel = null;
        }

        public ShopModel(ShopsViewModel ViewModel, Shop shop)
        {
            this.ViewModel = ViewModel;
            this.shop = shop;
        }

        public int Id
        {
            get
            {
                return shop.Id;
            }
        }

        public string Address
        {
            get
            {
                return shop.Address;
            }

            set
            {
                shop.Address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public string Name
        {
            get
            {
                return shop.Name;
            }

            set
            {
                shop.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ICommand DeleteCommand => new DeleteShopCommand(this);

        public ICommand EditCommand => new EditShopCommand(this);
    }
}
