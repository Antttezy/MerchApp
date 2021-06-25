using CoordinatorClient.Commands;
using CoordinatorClient.ViewModels;
using Domain.Core.Models;
using System;
using System.Windows.Input;

namespace CoordinatorClient.Models
{
    public class WorkshiftModel : ObservableObject
    {
        private readonly Workshift Shift;
        public WorkshiftsViewModel ViewModel { get; }

        public WorkshiftModel(Workshift workshift)
        {
            Shift = workshift;
        }

        public WorkshiftModel(WorkshiftsViewModel viewModel, Workshift workshift)
        {
            ViewModel = viewModel;
            Shift = workshift;
        }

        public int Id
        {
            get
            {
                return Shift.Id;
            }
        }

        public int? ShopId
        {
            get
            {
                return Shift.ShopId;
            }

            set
            {
                Shift.ShopId = value;
                OnPropertyChanged(nameof(ShopId));
            }
        }

        public ShopModel Shop
        {
            get
            {
                return new ShopModel(Shift.Shop);
            }

            set
            {
                Shift.Shop = new Shop
                {
                    Id = value.Id,
                    Address = value.Address,
                    Name = value.Name
                };

                OnPropertyChanged(nameof(Shop));
            }
        }

        public int? MerchId
        {
            get
            {
                return Shift.MerchendiserId;
            }

            set
            {
                Shift.MerchendiserId = value;
                OnPropertyChanged(nameof(MerchId));
            }
        }

        public MerchendiserModel Merch
        {
            get
            {
                return new MerchendiserModel(Shift.Merchendiser);
            }

            set
            {
                Shift.Merchendiser = new Merchendiser
                {
                    Id = value.Id,
                    Login = value.Login,
                    Password = value.Password,
                    CurrentShiftId = value.CurrentShiftId,
                    FirstName = value.FirstName,
                    SecondName = value.SecondName
                };
                OnPropertyChanged(nameof(Merch));
            }
        }

        public DateTime StartTime
        {
            get
            {
                return Shift.StartTime;
            }

            set
            {
                Shift.StartTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }

        public StatusModel RunningStatus
        {
            get => new StatusModel
            {
                Status = "Выполняется",
                Visibility = Merch.CurrentShiftId == Id ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed
            };

            set
            {
                OnPropertyChanged(nameof(RunningStatus));
            }
        }

        public StatusModel EndedStatus
        {
            get => new StatusModel
            {
                Visibility = Merch.CurrentShiftId != Id ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed
            };

            set
            {
                OnPropertyChanged(nameof(EndedStatus));
            }
        }

        public DateTime EndTime
        {
            get
            {
                return Shift.EndTime;
            }

            set
            {
                Shift.EndTime = value;
                OnPropertyChanged(nameof(EndTime));
            }
        }

        public ICommand DeleteCommand => new DeleteWorkshiftCommand(this);

        public ICommand EndCommand => new EndWorkshiftCommand(this);
    }
}
