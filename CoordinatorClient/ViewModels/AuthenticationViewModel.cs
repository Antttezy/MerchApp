using CoordinatorClient.Commands;
using CoordinatorClient.Models;
using CoordinatorClient.State.Navigators;
using CoordinatorControls.Services;
using Domain.Core.Models;
using Domain.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CoordinatorClient.ViewModels
{
    public class AuthenticationViewModel : ViewModelBase
    {
        public AuthenticationModel Model { get; set; } = new AuthenticationModel();

        public StatusModel LoginStatus { get; set; } = new StatusModel
        {
            Status = "",
            Visibility = System.Windows.Visibility.Collapsed
        };

        private IAuthenticator<Coordinator> Authenticator { get; set; } = new AuthenticationApi();

        public async Task Authenticate(string login, string password)
        {
            try
            {
                if (await Authenticator.IsCorrect(login, password))
                {
                    Navigator.UpdateCurrentVM.Execute(ViewType.Main);
                }
                else
                {
                    LoginStatus.Status = "Неправильное имя пользователя или пароль";
                    LoginStatus.Visibility = System.Windows.Visibility.Visible;
                }
            }
            catch (Exception)
            {
                LoginStatus.Status = "Ошибка аутентификации. Возможно, сервер не в сети";
                LoginStatus.Visibility = System.Windows.Visibility.Visible;
            }
        }

        public INavigator Navigator { get; set; } = AuthenticationNavigator.Instance;

        public ICommand EnterCommand => new EnterCommand(this);
    }
}
