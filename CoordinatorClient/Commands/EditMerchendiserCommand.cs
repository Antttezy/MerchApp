using CoordinatorClient.Models;
using CoordinatorClient.State.Navigators;
using CoordinatorClient.State.Objects;
using CoordinatorClient.ViewModels;
using System;
using System.Windows.Input;

namespace CoordinatorClient.Commands
{
    class EditMerchendiserCommand : ICommand
    {
        private readonly MerchViewModel viewModel;

        public event EventHandler CanExecuteChanged;

        public EditMerchendiserCommand(MerchViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            StateStorage<MerchendiserModel>.Instance.State = viewModel.Merch;
            viewModel.Vm.Navigator.UpdateCurrentVM.Execute(ViewType.EditMerch);
        }
    }
}
