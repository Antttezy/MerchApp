using CoordinatorClient.ViewModels;
using System;
using System.Windows.Input;

namespace CoordinatorClient.Commands
{
    class DeleteMerchendiserCommand : ICommand
    {
        private readonly MerchViewModel parent;

        public event EventHandler CanExecuteChanged;

        public DeleteMerchendiserCommand(MerchViewModel parent)
        {
            this.parent = parent;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                parent.Vm.DeleteMerch(parent.Merch.Id);
            }
            catch (Exception)
            {

            }
        }
    }
}
