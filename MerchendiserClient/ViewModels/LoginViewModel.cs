using MerchendiserClient.Commands;
using MerchendiserClient.Models;
using System.Windows;
using System.Windows.Input;

namespace MerchendiserClient.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginModel LoginModel { get; set; } = new LoginModel();
        public SingleModel<string> LoginStatus { get; set; } = new SingleModel<string>("");
        public SingleModel<Visibility> LoginStatusVisible { get; set; } = new SingleModel<Visibility>(Visibility.Collapsed);

        public ICommand LoginCommand => new LoginCommand(this);
    }
}
