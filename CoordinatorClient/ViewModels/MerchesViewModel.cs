using CoordinatorClient.Models;
using CoordinatorClient.State.Authentication;
using CoordinatorClient.State.Navigators;
using CoordinatorClient.Util;
using CoordinatorControls.Services;
using Domain.Core.Models;
using Domain.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CoordinatorClient.ViewModels
{
    public class MerchesViewModel : ViewModelBase
    {
        public ObservableCollection<MerchViewModel> Merches { get; set; } = new ObservableCollection<MerchViewModel>();
        public StatusModel LoadingStatus { get; set; } = new StatusModel();
        public StatusModel ContentStatus { get; set; } = new StatusModel();


        private IMerchControlService merchControl = new ApiMerchControlService();
        private AuthenticationData authData = AuthenticationData.Instance;

        public INavigator Navigator { get; set; } = State.Navigators.Navigator.Instance;

        public void DeleteMerch(int id)
        {
            var del = Merches.FirstOrDefault(m => m.Merch.Id == id);
            AsyncHelpers.RunSync(() => merchControl.RemoveMerch(new Authed<Merchendiser>
            {
                Login = authData.Login,
                Password = authData.Password,
                InnerData = new Merchendiser
                {
                    Id = del.Merch.Id
                }
            }));

            Merches.Remove(del);
        }

        private async Task FillCollection()
        {
            LoadingStatus.Visibility = System.Windows.Visibility.Visible;
            LoadingStatus.Status = "Загрузка";
            ContentStatus.Visibility = System.Windows.Visibility.Collapsed;

            try
            {
                var merches = await merchControl.Merches(authData.Login ?? "", authData.Password ?? "");

                foreach (var m in merches)
                {
                    Merches.Add(new MerchViewModel(this)
                    {
                        Merch = new MerchendiserModel(m)
                    });
                }

                LoadingStatus.Status = "";
                LoadingStatus.Visibility = System.Windows.Visibility.Collapsed;
                ContentStatus.Visibility = System.Windows.Visibility.Visible;
            }
            catch (Exception)
            {
                LoadingStatus.Status = "Ошибка загрузки";
            }
        }

        public MerchesViewModel()
        {
            FillCollection();
        }
    }
}
