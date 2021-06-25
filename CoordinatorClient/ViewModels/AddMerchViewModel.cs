using CoordinatorClient.Commands;
using CoordinatorClient.Models;
using CoordinatorClient.State.Navigators;
using Domain.Core.Models;
using System.Windows.Input;

namespace CoordinatorClient.ViewModels
{
    public class AddMerchViewModel : ViewModelBase
    {
        public MerchendiserModel Merch { get; set; } = new MerchendiserModel(new Merchendiser());

        public INavigator Navigator => State.Navigators.Navigator.Instance;

        public ICommand CreateCommand => new CreateMerchendiserCommand(this);
    }
}
