using CoordinatorClient.Models;
using System;
using System.Windows.Input;

namespace CoordinatorClient.Commands
{
    class DeleteWorkshiftCommand : ICommand
    {
        private WorkshiftModel Model { get; }

        public DeleteWorkshiftCommand(WorkshiftModel model)
        {
            Model = model;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                Model.ViewModel.DeleteShift(Model.Id);
            }
            catch (Exception)
            {

            }
        }
    }
}
