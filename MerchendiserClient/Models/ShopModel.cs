using Domain.Core.Models;

namespace MerchendiserClient.Models
{
    public class ShopModel : ObservableObject
    {
        private readonly Shop shop;

        public ShopModel(Shop shop)
        {
            this.shop = shop;
        }

        public int Id
        {
            get
            {
                return shop.Id;
            }

            set
            {
                shop.Id = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
                OnPropertyChanged(nameof(Description));
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
                OnPropertyChanged();
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Description
        {
            get
            {
                return $"{Address}, {Name}";
            }
        }
    }
}
