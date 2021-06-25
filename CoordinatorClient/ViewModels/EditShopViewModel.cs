using CoordinatorClient.Commands;
using CoordinatorClient.Models;
using CoordinatorClient.State.Navigators;
using CoordinatorClient.State.Objects;
using System.Windows.Input;

namespace CoordinatorClient.ViewModels
{
    public class EditShopViewModel : ViewModelBase
    {
        public ShopModel Shop { get; set; }

        public EditShopViewModel()
        {
            Shop = StateStorage<ShopModel>.Instance.State;
        }

        public INavigator Navigator => State.Navigators.Navigator.Instance;

        public ICommand ConfirmEditCommand => new ConfirmEditShopCommand(this);
    }
}
