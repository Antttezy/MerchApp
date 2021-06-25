using Domain.Core.Models;

namespace CoordinatorClient.Models
{
    public class MerchendiserModel : ObservableObject
    {
        private readonly Merchendiser merchendiser;

        public int Id
        {
            get
            {
                return merchendiser.Id;
            }
        }

        public string FirstName
        {
            get
            {
                return merchendiser.FirstName;
            }

            set
            {
                merchendiser.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string SecondName
        {
            get
            {
                return merchendiser.SecondName;
            }

            set
            {
                merchendiser.SecondName = value;
                OnPropertyChanged(nameof(SecondName));
            }
        }

        public string Login
        {
            get
            {
                return merchendiser.Login;
            }

            set
            {
                merchendiser.Login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string password = "";

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public int? CurrentShiftId
        {
            get
            {
                return merchendiser.CurrentShiftId;
            }

            set
            {
                merchendiser.CurrentShiftId = value;
                OnPropertyChanged(nameof(CurrentShiftId));
                OnPropertyChanged(nameof(Status));
            }
        }

        public string Status
        {
            get
            {
                return CurrentShiftId != null ? "работает" : "";
            }
        }

        public MerchendiserModel(Merchendiser merchendiser)
        {
            this.merchendiser = merchendiser;
        }
    }
}
