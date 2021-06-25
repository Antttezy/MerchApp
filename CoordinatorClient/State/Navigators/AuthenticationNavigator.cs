using CoordinatorClient.Commands;
using CoordinatorClient.Models;
using CoordinatorClient.ViewModels;
using System.Windows.Input;

namespace CoordinatorClient.State.Navigators
{
    public class AuthenticationNavigator : ObservableObject, INavigator
    {
        private ViewModelBase currentViewModel;

        public static readonly AuthenticationNavigator Instance = new AuthenticationNavigator();

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return currentViewModel;
            }
            set
            {
                currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ICommand UpdateCurrentVM => new UpdateMainVMCommand(this);

        private AuthenticationNavigator()
        {

        }
    }
}
