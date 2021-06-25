using CoordinatorClient.Models;
using System;
using System.Windows.Input;

namespace CoordinatorClient.Commands
{
    class EndWorkshiftCommand : ICommand
    {
        private WorkshiftModel Model { get; }

        public EndWorkshiftCommand(WorkshiftModel model)
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
                Model.ViewModel.EndShift(Model.Id);
            }
            catch (Exception)
            {

            }
        }
    }
}
