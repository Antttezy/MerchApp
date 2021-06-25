using Domain.Core.Models;
using Domain.Services.Interfaces;
using Google.Protobuf.WellKnownTypes;
using System.Linq;
using WorkshiftProtocol;
using static WorkshiftProtocol.WorkshiftRepository;

namespace MerchendiseServer.Services
{
    public class WorkshiftRepository : IRepository<Workshift>
    {
        private readonly WorkshiftRepositoryClient workshiftClient;
        private readonly MerchInfoProtocol.MerchInfoRepository.MerchInfoRepositoryClient merchClient;
        private readonly IRepository<Shop> shops;

        public WorkshiftRepository(WorkshiftRepositoryClient workshiftClient, MerchInfoProtocol.MerchInfoRepository.MerchInfoRepositoryClient merchClient, IRepository<Shop> shops)
        {
            this.workshiftClient = workshiftClient;
            this.merchClient = merchClient;
            this.shops = shops;
        }

        public void Add(Workshift item)
        {
            workshiftClient.Add(new WorkshiftInfo
            {
                Id = item.Id,
                Merchid = item.MerchendiserId ?? -1,
                ShopId = item.ShopId ?? -1,
                Starttime = Timestamp.FromDateTime(item.StartTime.ToUniversalTime()),
                Endtime = Timestamp.FromDateTime(item.EndTime.ToUniversalTime())
            });
        }

        public IQueryable<Workshift> All()
        {
            var resp = workshiftClient.All(new Request());

            var result = resp.Workshifts.Select(x =>
           {
               var ret = new Workshift
               {
                   Id = x.Id,
                   MerchendiserId = x.Merchid >= 0 ? (int?)x.Merchid : null,
                   ShopId = x.ShopId >= 0 ? (int?)x.ShopId : null,
                   Shop = x.ShopId >= 0 ? shops.Get(x.ShopId) : null,
                   StartTime = x.Starttime.ToDateTime().ToLocalTime(),
                   EndTime = x.Endtime.ToDateTime().ToLocalTime()
               };

               if (ret.MerchendiserId.HasValue)
               {
                   var minfo = merchClient.Get(new MerchInfoProtocol.MerchId { Id = x.Merchid });
                   var merch = new Merchendiser
                   {
                       Id = minfo.Id,
                       CurrentShiftId = minfo.ShiftId >= 0 ? (int?)minfo.ShiftId : null,
                       CurrentShift = minfo.ShiftId >= 0 ? ret : null,
                       FirstName = minfo.FirstName,
                       SecondName = minfo.SecondName,
                       Login = minfo.Login,
                       Password = minfo.Password
                   };
                   ret.Merchendiser = merch;
               }

               return ret;
           }).ToArray().AsQueryable();

            return result;
        }

        public Workshift Get(int id)
        {
            var resp = workshiftClient.Get(new WorkshiftId { Id = id });
            var ret = new Workshift
            {
                Id = resp.Id,
                MerchendiserId = resp.Merchid >= 0 ? (int?)resp.Merchid : null,
                ShopId = resp.ShopId >= 0 ? (int?)resp.ShopId : null,
                Shop = resp.ShopId >= 0 ? shops.Get(resp.ShopId) : null,
                StartTime = resp.Starttime.ToDateTime().ToLocalTime(),
                EndTime = resp.Endtime.ToDateTime().ToLocalTime()
            };

            if (ret.MerchendiserId.HasValue)
            {
                var minfo = merchClient.Get(new MerchInfoProtocol.MerchId { Id = resp.Merchid });
                var merch = new Merchendiser
                {
                    Id = minfo.Id,
                    FirstName = minfo.FirstName,
                    SecondName = minfo.SecondName,
                    Login = minfo.Login,
                    Password = minfo.Password,
                    CurrentShiftId = minfo.ShiftId >= 0 ? (int?)minfo.ShiftId : null,
                    CurrentShift = minfo.ShiftId >= 0 ? ret : null
                };
                ret.Merchendiser = merch;
            }

            return ret;
        }

        public void Remove(Workshift item)
        {
            workshiftClient.Remove(new WorkshiftId { Id = item.Id });
        }

        public void Update(Workshift item)
        {
            workshiftClient.Update(new WorkshiftInfo
            {
                Id = item.Id,
                ShopId = item.ShopId ?? -1,
                Merchid = item.MerchendiserId ?? -1,
                Starttime = Timestamp.FromDateTime(item.StartTime.ToUniversalTime()),
                Endtime = Timestamp.FromDateTime(item.EndTime.ToUniversalTime())
            });
        }
    }
}
