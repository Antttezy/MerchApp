using MerchendiserApi.Client;
using MerchendiserApi.Interfaces;
using MerchendiserClient.Commands;
using MerchendiserClient.Models;
using MerchendiserClient.State.Navigators;
using MerchendiserClient.State.Storage;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MerchendiserClient.ViewModels
{
    public class ShiftsViewModel : ViewModelBase
    {
        public ObservableCollection<WorkshiftModel> Workshifts { get; set; } = new ObservableCollection<WorkshiftModel>();

        public SingleModel<Visibility> RunningWorkshiftVisibility { get; } = new SingleModel<Visibility>();
        public SingleModel<Visibility> NotRunningWorkshiftVisibility { get; } = new SingleModel<Visibility>();

        public SingleModel<string> LoadingStatus { get; } = new SingleModel<string>();
        public SingleModel<Visibility> LoadingStatusVisibility { get; } = new SingleModel<Visibility>();
        public SingleModel<Visibility> LoadedStatusVisibility { get; } = new SingleModel<Visibility>();

        public ICommand RenavigateCommand => new UpdateCurrentVMCommand(SessionStorage.GetStorage["MainViewModel.Navigator"] as INavigator);
        public ICommand EndShiftCommand => new EndShiftCommand(WorkshiftEnded);

        public string Login { get; }
        public string Password { get; }

        IWorkshiftReader workshiftReader = new WorkshiftReader();

        private async Task LoadWorkshifts()
        {
            LoadedStatusVisibility.Value = Visibility.Collapsed;
            LoadingStatusVisibility.Value = Visibility.Visible;
            RunningWorkshiftVisibility.Value = Visibility.Collapsed;
            NotRunningWorkshiftVisibility.Value = Visibility.Collapsed;
            LoadingStatus.Value = "Загрузка";

            try
            {
                var shifts = await workshiftReader.GetWorkshifts(Login, Password);

                foreach (var s in shifts.OrderByDescending(x => x.Id))
                {
                    Workshifts.Add(new WorkshiftModel(s, this));
                }

                LoadedStatusVisibility.Value = Visibility.Visible;
                LoadingStatusVisibility.Value = Visibility.Collapsed;
                RunningWorkshiftVisibility.Value = (shifts.Any() && shifts[0].Merchendiser.CurrentShiftId != null) ? Visibility.Visible : Visibility.Collapsed;
                NotRunningWorkshiftVisibility.Value = (shifts.Any() && shifts[0].Merchendiser.CurrentShiftId != null) ? Visibility.Collapsed : Visibility.Visible;
            }
            catch (Exception)
            {
                LoadedStatusVisibility.Value = Visibility.Collapsed;
                LoadingStatusVisibility.Value = Visibility.Visible;
                RunningWorkshiftVisibility.Value = Visibility.Collapsed;
                NotRunningWorkshiftVisibility.Value = Visibility.Collapsed;
                LoadingStatus.Value = "Ошибка загрузки";
            }
        }

        private void WorkshiftEnded()
        {
            Workshifts.Clear();
            LoadWorkshifts();
        }

        public ShiftsViewModel()
        {
            Login = SessionStorage.GetStorage["Login"] as string;
            Password = SessionStorage.GetStorage["Password"] as string;
            LoadWorkshifts();
        }
    }
}
