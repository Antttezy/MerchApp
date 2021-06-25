using Domain.Core.Models;
using Domain.Services.Interfaces;
using MerchInfoProtocol;
using System.Linq;
using static CoordInfoProtocol.CoordInfoRepository;
using static MerchInfoProtocol.MerchInfoRepository;
using static WorkshiftProtocol.WorkshiftRepository;

namespace MerchendiseServer.Services
{
    public class UserRepository : IRepository<Merchendiser>, IRepository<Coordinator>
    {
        private readonly MerchInfoRepositoryClient merchClient;
        private readonly CoordInfoRepositoryClient coordClient;
        private readonly WorkshiftRepositoryClient workshiftClient;
        private readonly IRepository<Shop> shops;

        public UserRepository(MerchInfoRepositoryClient merchClient, CoordInfoRepositoryClient coordClient, WorkshiftRepositoryClient workshiftClient, IRepository<Shop> shops)
        {
            this.merchClient = merchClient;
            this.coordClient = coordClient;
            this.workshiftClient = workshiftClient;
            this.shops = shops;
        }

        #region Merchendiser

        public void Add(Merchendiser item)
        {
            merchClient.Add(new MerchInfo
            {
                FirstName = item.FirstName,
                SecondName = item.SecondName,
                Login = item.Login,
                Password = item.Password,
                ShiftId = item.CurrentShiftId ?? -1
            });
        }

        public IQueryable<Merchendiser> All()
        {
            var resp = merchClient.All(new Request());

            return resp.Users.Select(x =>
            {
                var ret = new Merchendiser
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    SecondName = x.SecondName,
                    Login = x.Login,
                    Password = x.Password,
                    CurrentShiftId = x.ShiftId >= 0 ? (int?)x.ShiftId : null
                };

                if (ret.CurrentShiftId.HasValue)
                {
                    var shiftinfo = workshiftClient.Get(new WorkshiftProtocol.WorkshiftId { Id = x.ShiftId });
                    var shift = new Workshift
                    {
                        Id = shiftinfo.Id,
                        MerchendiserId = shiftinfo.Merchid >= 0 ? (int?)shiftinfo.Merchid : null,
                        Merchendiser = ret,
                        ShopId = shiftinfo.ShopId >= 0 ? (int?)shiftinfo.ShopId : null,
                        Shop = shiftinfo.ShopId >= 0 ? shops.Get(shiftinfo.ShopId) : null,
                        StartTime = shiftinfo.Starttime.ToDateTime().ToLocalTime(),
                        EndTime = shiftinfo.Endtime.ToDateTime().ToLocalTime()
                    };
                    ret.CurrentShift = shift;
                }

                return ret;

            }).ToArray().AsQueryable();
        }

        public Merchendiser Get(int id)
        {
            var resp = merchClient.Get(new MerchId { Id = id });
            var ret = new Merchendiser
            {
                Id = resp.Id,
                FirstName = resp.FirstName,
                SecondName = resp.SecondName,
                Login = resp.Login,
                Password = resp.Password,
                CurrentShiftId = resp.ShiftId >= 0 ? (int?)resp.ShiftId : null,
            };

            if (ret.CurrentShiftId.HasValue)
            {
                var shiftinfo = workshiftClient.Get(new WorkshiftProtocol.WorkshiftId { Id = resp.ShiftId });
                var shift = new Workshift
                {
                    Id = shiftinfo.Id,
                    MerchendiserId = shiftinfo.Merchid >= 0 ? (int?)shiftinfo.Merchid : null,
                    Merchendiser = ret,
                    ShopId = shiftinfo.ShopId >= 0 ? (int?)shiftinfo.ShopId : null,
                    Shop = shiftinfo.ShopId >= 0 ? shops.Get(shiftinfo.ShopId) : null,
                    StartTime = shiftinfo.Starttime.ToDateTime().ToLocalTime(),
                    EndTime = shiftinfo.Endtime.ToDateTime().ToLocalTime()
                };
                ret.CurrentShift = shift;
            }

            return ret;
        }

        public void Remove(Merchendiser item)
        {
            merchClient.Remove(new MerchId { Id = item.Id });
        }

        public void Update(Merchendiser item)
        {
            merchClient.Update(new MerchInfo
            {
                Id = item.Id,
                FirstName = item.FirstName,
                SecondName = item.SecondName,
                Login = item.Login,
                Password = item.Password,
                ShiftId = item.CurrentShiftId ?? -1
            });
        }

        #endregion

        #region Coordinator

        public void Add(Coordinator item)
        {
            coordClient.Add(new CoordInfoProtocol.CoordInfo
            {
                Id = item.Id,
                FirstName = item.FirstName,
                SecondName = item.SecondName,
                Login = item.Login,
                Password = item.Password
            });
        }

        public void Remove(Coordinator item)
        {
            coordClient.Remove(new CoordInfoProtocol.CoordId { Id = item.Id });
        }

        public void Update(Coordinator item)
        {
            coordClient.Update(new CoordInfoProtocol.CoordInfo
            {
                Id = item.Id,
                FirstName = item.FirstName,
                SecondName = item.SecondName,
                Login = item.Login,
                Password = item.Password
            });
        }

        IQueryable<Coordinator> IRepository<Coordinator>.All()
        {
            var resp = coordClient.All(new CoordInfoProtocol.Request());

            return resp.Users.Select(x => new Coordinator
            {
                Id = x.Id,
                FirstName = x.FirstName,
                SecondName = x.SecondName,
                Login = x.Login,
                Password = x.Password
            }).ToArray().AsQueryable();
        }

        Coordinator IRepository<Coordinator>.Get(int id)
        {
            var resp = coordClient.Get(new CoordInfoProtocol.CoordId { Id = id });
            return new Coordinator
            {
                Id = resp.Id,
                FirstName = resp.FirstName,
                SecondName = resp.SecondName,
                Login = resp.Login,
                Password = resp.Password
            };
        }

        #endregion
    }
}
