using CoordinatorClient.State.Navigators;

namespace CoordinatorClient.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; } = State.Navigators.Navigator.Instance;
    }
}
