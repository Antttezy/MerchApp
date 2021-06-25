using MerchendiserClient.State.Navigators;
using MerchendiserClient.State.Storage;

namespace MerchendiserClient.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; } = new Navigator { CurrentViewModel = new LoginViewModel() };

        public MainViewModel()
        {
            SessionStorage.GetStorage["MainViewModel.Navigator"] = Navigator;
        }
    }
}
