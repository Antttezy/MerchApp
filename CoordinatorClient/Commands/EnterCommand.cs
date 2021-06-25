using CoordinatorClient.ViewModels;
using System;
using System.Windows.Input;

namespace CoordinatorClient.Commands
{
    class EnterCommand : ICommand
    {
        private readonly AuthenticationViewModel viewModel;

        public event EventHandler CanExecuteChanged;

        public EnterCommand(AuthenticationViewModel viewModel)
        {
            this.viewModel = viewModel;
            viewModel.Model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(viewModel.Model.Login) &&
                !string.IsNullOrEmpty(viewModel.Model.Password);
        }

        public void Execute(object parameter)
        {
            viewModel.Authenticate(viewModel.Model.Login, viewModel.Model.Password);
        }
    }
}
