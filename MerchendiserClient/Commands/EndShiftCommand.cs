using Domain.Core.Models;
using MerchendiserApi.Client;
using MerchendiserApi.Interfaces;
using MerchendiserClient.State.Storage;
using MerchendiserClient.Util;
using System;
using System.Windows.Input;

namespace MerchendiserClient.Commands
{
    class EndShiftCommand : ICommand
    {
        private readonly Action onExecute;

        public EndShiftCommand()
        {

        }

        public EndShiftCommand(Action onExecute)
        {
            this.onExecute = onExecute;
        }

        IWorkshiftEnder workshiftEnder = new WorkshiftEnder();

        string Login { get; } = SessionStorage.GetStorage["Login"] as string;
        string Password { get; } = SessionStorage.GetStorage["Password"] as string;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                AsyncHelpers.RunSync(() => workshiftEnder.EndWorkshift(new Authed
                {
                    Login = Login,
                    Password = Password
                }));
            }
            catch (Exception)
            {

            }
            finally
            {
                onExecute?.Invoke();
            }
        }
    }
}
