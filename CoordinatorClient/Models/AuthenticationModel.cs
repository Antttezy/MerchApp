using CoordinatorClient.State.Authentication;

namespace CoordinatorClient.Models
{
    public class AuthenticationModel : ObservableObject
    {
        AuthenticationData authData = AuthenticationData.Instance;

        public string Login
        {
            get => authData.Login;

            set
            {
                authData.Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => authData.Password;

            set
            {
                authData.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
    }
}
