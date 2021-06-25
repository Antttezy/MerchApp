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
    class ConfirmEditMerchendiserCommand : ICommand
    {
        private readonly EditMerchViewModel viewModel;

        public event EventHandler CanExecuteChanged;

        private IMerchControlService merchControl = new ApiMerchControlService();
        private AuthenticationData authData = AuthenticationData.Instance;

        public ConfirmEditMerchendiserCommand(EditMerchViewModel viewModel)
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
                !string.IsNullOrWhiteSpace(viewModel.Merch.Login);
        }

        public void Execute(object parameter)
        {
            try
            {
                AsyncHelpers.RunSync(() => merchControl.UpdateMerch(new Authed<Merchendiser>
                {
                    Login = authData.Login,
                    Password = authData.Password,
                    InnerData = new Merchendiser
                    {
                        Id = viewModel.Merch.Id,
                        FirstName = viewModel.Merch.FirstName,
                        SecondName = viewModel.Merch.SecondName,
                        Login = viewModel.Merch.Login,
                        Password = viewModel.Merch.Password,
                        CurrentShiftId = viewModel.Merch.CurrentShiftId
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
