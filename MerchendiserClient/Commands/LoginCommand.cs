using MerchendiserApi.Client;
using MerchendiserApi.Interfaces;
using MerchendiserClient.State.Navigators;
using MerchendiserClient.State.Storage;
using MerchendiserClient.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MerchendiserClient.Commands
{
    class LoginCommand : ICommand
    {
        private readonly LoginViewModel model;

        public event EventHandler CanExecuteChanged;

        private IAuthenticator authenticator = new Authenticator();

        public LoginCommand(LoginViewModel model)
        {
            this.model = model;
            model.LoginModel.PropertyChanged += LoginModel_PropertyChanged;
        }

        private void LoginModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(model.LoginModel.Login) &&
                !string.IsNullOrEmpty(model.LoginModel.Password);
        }

        private async Task Auth()
        {
            try
            {
                if (await authenticator.IsCorrect(model.LoginModel.Login, model.LoginModel.Password))
                {
                    INavigator navigator = SessionStorage.GetStorage["MainViewModel.Navigator"] as INavigator;
                    SessionStorage.GetStorage["Login"] = model.LoginModel.Login;
                    SessionStorage.GetStorage["Password"] = model.LoginModel.Password;
                    navigator.UpdateCurrentVM.Execute(ViewType.Shifts);
                }
                else
                {
                    model.LoginStatus.Value = "Неверный логин или пароль";
                    model.LoginStatusVisible.Value = System.Windows.Visibility.Visible;
                }
            }
            catch (Exception)
            {
                model.LoginStatus.Value = "Ошибка входа. Возможно, сервер не в сети";
                model.LoginStatusVisible.Value = System.Windows.Visibility.Visible;
            }
        }

        public void Execute(object parameter)
        {
            Auth();
        }
    }
}
