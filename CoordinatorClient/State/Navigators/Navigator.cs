using CoordinatorClient.Commands;
using CoordinatorClient.Models;
using CoordinatorClient.ViewModels;
using System.Windows.Input;

namespace CoordinatorClient.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        private ViewModelBase currentViewModel;

        public static readonly Navigator Instance = new Navigator();

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

        public ICommand UpdateCurrentVM => new UpdateCurrentVMCommand(this);

        private Navigator()
        {

        }
    }
}
