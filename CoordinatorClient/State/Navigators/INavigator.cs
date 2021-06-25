using CoordinatorClient.ViewModels;
using System.Windows.Input;

namespace CoordinatorClient.State.Navigators
{
    public enum ViewType
    {
        Merches,
        Shops,
        AddShop,
        EditShop,
        Shifts,
        AddMerch,
        EditMerch,
        Main,
        Authentication
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }

        ICommand UpdateCurrentVM { get; }
    }
}
