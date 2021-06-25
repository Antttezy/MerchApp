using Domain.Core.Models;
using MerchendiserClient.ViewModels;
using System;
using System.Windows;

namespace MerchendiserClient.Models
{
    public class WorkshiftModel : ObservableObject
    {
        private Workshift workshift;
        public ShiftsViewModel ViewModel { get; }

        public int Id
        {
            get => workshift.Id;

            set
            {
                workshift.Id = value;
                OnPropertyChanged();
            }
        }

        public int? ShopId
        {
            get => workshift.ShopId;

            set
            {
                workshift.ShopId = value;
                OnPropertyChanged();
            }
        }

        public Shop Shop
        {
            get => workshift.Shop;

            set
            {
                workshift.Shop = value;
                OnPropertyChanged();
            }
        }

        public int? MerchendiserId
        {
            get => workshift.MerchendiserId;

            set
            {
                workshift.MerchendiserId = value;
                OnPropertyChanged();
            }
        }

        public Merchendiser Merchendiser
        {
            get => workshift.Merchendiser;

            set
            {
                workshift.Merchendiser = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartTime
        {
            get => workshift.StartTime;

            set
            {
                workshift.StartTime = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndTime
        {
            get => workshift.EndTime;

            set
            {
                workshift.EndTime = value;
                OnPropertyChanged();
            }
        }

        public SingleModel<Visibility> RunningVisibility { get; } = new SingleModel<Visibility>();

        public SingleModel<Visibility> EndedVisibility { get; } = new SingleModel<Visibility>();


        public WorkshiftModel(Workshift workshift)
        {
            this.workshift = workshift;
            RunningVisibility.Value = Merchendiser.CurrentShiftId == Id ? Visibility.Visible : Visibility.Collapsed;
            EndedVisibility.Value = Merchendiser.CurrentShiftId != Id ? Visibility.Visible : Visibility.Collapsed;
        }

        public WorkshiftModel(Workshift workshift, ShiftsViewModel viewModel) : this(workshift)
        {
            ViewModel = viewModel;
        }
    }
}
