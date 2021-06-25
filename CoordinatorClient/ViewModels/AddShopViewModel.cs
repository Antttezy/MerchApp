using CoordinatorClient.Commands;
using CoordinatorClient.Models;
using CoordinatorClient.State.Navigators;
using Domain.Core.Models;
using System.Windows.Input;

namespace CoordinatorClient.ViewModels
{
    public class AddShopViewModel : ViewModelBase
    {
        public ShopModel ShopModel { get; set; } = new ShopModel(new Shop());

        public INavigator Navigator => State.Navigators.Navigator.Instance;

        public ICommand CreateCommand => new CreateShopCommand(this);
    }
}
