using CoordinatorClient.Commands;
using CoordinatorClient.Models;
using CoordinatorClient.State.Navigators;
using CoordinatorClient.State.Objects;
using System.Windows.Input;

namespace CoordinatorClient.ViewModels
{
    class EditMerchViewModel : ViewModelBase
    {
        public MerchendiserModel Merch { get; set; }

        public EditMerchViewModel()
        {
            Merch = StateStorage<MerchendiserModel>.Instance.State;
        }

        public INavigator Navigator => State.Navigators.Navigator.Instance;

        public ICommand ConfirmCommand => new ConfirmEditMerchendiserCommand(this);
    }
}
