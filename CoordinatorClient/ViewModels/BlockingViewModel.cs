using CoordinatorClient.State.Navigators;

namespace CoordinatorClient.ViewModels
{
    public class BlockingViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; } = AuthenticationNavigator.Instance;

        public BlockingViewModel()
        {
            Navigator.UpdateCurrentVM.Execute(ViewType.Authentication);
        }
    }
}
