using CoordinatorClient.State.Authentication;
using CoordinatorClient.State.Navigators;
using CoordinatorClient.Util;
using CoordinatorClient.ViewModels;
using CoordinatorControls.Services;
using Domain.Core.Models;
using Domain.Services.Interfaces;
using System;
using System.Windows.Input;

namespace CoordinatorClient.Commands
{
    class CreateMerchendiserCommand : ICommand
    {
        private readonly AddMerchViewModel viewModel;

        public event EventHandler CanExecuteChanged;

        private IMerchControlService merchControl = new ApiMerchControlService();
        private AuthenticationData authData = AuthenticationData.Instance;

        public CreateMerchendiserCommand(AddMerchViewModel viewModel)
        {
            this.viewModel = viewModel;
            viewModel.Merch.PropertyChanged += Merch_PropertyChanged;
        }

        private void Merch_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(viewModel.Merch.FirstName) &&
                !string.IsNullOrWhiteSpace(viewModel.Merch.SecondName) &&
                !string.IsNullOrWhiteSpace(viewModel.Merch.Login) &&
                !string.IsNullOrWhiteSpace(viewModel.Merch.Password);
        }

        public void Execute(object parameter)
        {
            try
            {
                AsyncHelpers.RunSync(() => merchControl.AddMerch(new Authed<Merchendiser>
                {
                    Login = authData.Login,
                    Password = authData.Password,
                    InnerData = new Merchendiser
                    {
                        FirstName = viewModel.Merch.FirstName,
                        SecondName = viewModel.Merch.SecondName,
                        Login = viewModel.Merch.Login,
                        Password = viewModel.Merch.Password
                    }
                }));

                viewModel.Navigator.UpdateCurrentVM.Execute(ViewType.Merches);
            }
            catch (Exception)
            {

            }
        }
    }
}
