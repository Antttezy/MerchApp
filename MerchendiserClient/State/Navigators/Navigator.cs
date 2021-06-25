using MerchendiserClient.Commands;
using MerchendiserClient.Models;
using MerchendiserClient.ViewModels;
using System.Windows.Input;

namespace MerchendiserClient.State.Navigators
{
    public sealed class Navigator : ObservableObject, INavigator
    {
        private ViewModelBase currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => currentViewModel;

            set
            {
                currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateCurrentVM => new UpdateCurrentVMCommand(this);
    }
}
