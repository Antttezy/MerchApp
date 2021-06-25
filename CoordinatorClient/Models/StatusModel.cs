using System.Windows;

namespace CoordinatorClient.Models
{
    public class StatusModel : ObservableObject
    {
        private string status = "";
        public string Status
        {
            get => status;

            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private Visibility visibility = Visibility.Visible;
        public Visibility Visibility
        {
            get => visibility;

            set
            {
                visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }
    }
}
