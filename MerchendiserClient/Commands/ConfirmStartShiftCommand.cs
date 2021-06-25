using Domain.Core.Models;
using MerchendiserApi.Client;
using MerchendiserApi.Interfaces;
using MerchendiserClient.State.Navigators;
using MerchendiserClient.State.Storage;
using MerchendiserClient.Util;
using System;
using System.Windows.Input;

namespace MerchendiserClient.Commands
{
    class ConfirmStartShiftCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        IWorkshiftStarter workshiftStarter = new WorkshiftStarter();

        string Login { get; } = SessionStorage.GetStorage["Login"] as string;
        string Password { get; } = SessionStorage.GetStorage["Password"] as string;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is int id)
            {
                try
                {
                    AsyncHelpers.RunSync(() => workshiftStarter.StartWorkshift(new Authed<int>
                    {
                        Login = Login,
                        Password = Password,
                        InnerData = id
                    }));

                    INavigator nav = SessionStorage.GetStorage["MainViewModel.Navigator"] as INavigator;
                    nav.UpdateCurrentVM.Execute(ViewType.Shifts);
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
