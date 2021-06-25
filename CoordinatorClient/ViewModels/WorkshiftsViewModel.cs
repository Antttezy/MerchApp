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
    public class WorkshiftsViewModel : ViewModelBase
    {
        public ObservableCollection<WorkshiftModel> Workshifts { get; set; } = new ObservableCollection<WorkshiftModel>();
        public StatusModel LoadingStatus { get; set; } = new StatusModel();
        public StatusModel ContentStatus { get; set; } = new StatusModel();


        private AuthenticationData authData = AuthenticationData.Instance;
        private IWorkshiftControlService workshiftControlService = new ApiWorkshiftControlService();
        private IMerchControlService merchControlService = new ApiMerchControlService();

        public void DeleteShift(int id)
        {
            var del = Workshifts.FirstOrDefault(s => s.Id == id);
            AsyncHelpers.RunSync(() => workshiftControlService.RemoveWorkshift(new Authed<Workshift>
            {
                Login = authData.Login,
                Password = authData.Password,
                InnerData = new Workshift
                {
                    Id = del.Id
                }
            }));

            Workshifts.Remove(del);
        }

        public void EndShift(int id)
        {
            var shift = Workshifts.First(s => s.Id == id);

            AsyncHelpers.RunSync(() => merchControlService.EndShift(new Authed<int>
            {
                Login = authData.Login,
                Password = authData.Password,
                InnerData = shift.MerchId.Value
            }));

            AsyncHelpers.RunSync(() => workshiftControlService.EndWorkshift(new Authed<int>
            {
                Login = authData.Login,
                Password = authData.Password,
                InnerData = id
            }));

            var s = Workshifts.First(s => s.Id == id);
            s.Merch.CurrentShiftId = null;
            s.RunningStatus = s.RunningStatus;
            s.EndedStatus = s.EndedStatus;
        }

        public INavigator Navigator { get; set; } = State.Navigators.Navigator.Instance;

        private async Task FillCollection()
        {
            LoadingStatus.Visibility = System.Windows.Visibility.Visible;
            LoadingStatus.Status = "Загрузка";
            ContentStatus.Visibility = System.Windows.Visibility.Collapsed;

            try
            {
                var shifts = await workshiftControlService.Workshifts(new Authed
                {
                    Login = authData.Login,
                    Password = authData.Password
                });

                foreach (var s in shifts.OrderByDescending(s => s.Id))
                {
                    Workshifts.Add(new WorkshiftModel(this, s));
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

        public WorkshiftsViewModel()
        {
            FillCollection();
        }
    }
}
