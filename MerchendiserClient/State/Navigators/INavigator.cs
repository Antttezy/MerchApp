using MerchendiserClient.ViewModels;
using System.Windows.Input;

namespace MerchendiserClient.State.Navigators
{
    public enum ViewType
    {
        Authentication,
        Shifts,
        AddShift
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }

        ICommand UpdateCurrentVM { get; }
    }
}
