namespace MerchendiserClient.Models
{
    public class LoginModel : ObservableObject
    {
        string login = "";
        public string Login
        {
            get => login;

            set
            {
                login = value;
                OnPropertyChanged();
            }
        }

        string password = "";
        public string Password
        {
            get => password;

            set
            {
                password = value;
                OnPropertyChanged();
            }
        }
    }
}
