using CoordinatorClient.Commands;
using CoordinatorClient.Models;
using System.Windows.Input;

namespace CoordinatorClient.ViewModels
{
    public class MerchViewModel : ViewModelBase
    {
        public MerchendiserModel Merch { get; set; }

        public ICommand DeleteCommand => new DeleteMerchendiserCommand(this);
        public ICommand EditCommand => new EditMerchendiserCommand(this);

        public MerchesViewModel Vm { get; }

        public MerchViewModel(MerchesViewModel viewModel)
        {
            Vm = viewModel;
        }
    }
}
