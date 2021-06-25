using Domain.Core.Models;
using Domain.Services.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkshiftProtocol;
using static WorkshiftProtocol.WorkshiftRepository;

namespace UserRepositoryService.Services
{
    public class WorkshiftService : WorkshiftRepositoryBase
    {
        private readonly IRepository<Workshift> shifts;

        public WorkshiftService(IRepository<Workshift> shifts)
        {
            this.shifts = shifts;
        }

        public override async Task<StatusResponse> Add(WorkshiftInfo request, ServerCallContext context)
        {
            try
            {
                await Task.Run(() => shifts.Add(new Workshift
                {
                    MerchendiserId = request.Merchid >= 0 ? (int?)request.Merchid : null,
                    ShopId = request.ShopId >= 0 ? (int?)request.ShopId : null,
                    StartTime = request.Starttime.ToDateTime().ToLocalTime(),
                    EndTime = request.Endtime.ToDateTime().ToLocalTime()
                }));
            }
            catch (Exception e)
            {
                return new StatusResponse { Status = -1 };
            }

            return new StatusResponse { Status = 0 };
        }

        public override async Task<WorkshiftList> All(Request request, ServerCallContext context)
        {
            var list = new WorkshiftList();
            list.Workshifts.AddRange((await Task.Run(() =>
            shifts.All()))
                .Select(x => new WorkshiftInfo
                {
                    Id = x.Id,
                    ShopId = x.ShopId ?? -1,
                    Merchid = x.MerchendiserId ?? -1,
                    Starttime = Timestamp.FromDateTime(x.StartTime.ToUniversalTime()),
                    Endtime = Timestamp.FromDateTime(x.EndTime.ToUniversalTime())
                }));

            return list;
        }

        public override async Task<WorkshiftInfo> Get(WorkshiftId request, ServerCallContext context)
        {
            var shift = await Task.Run(() => shifts.Get(request.Id));

            return new WorkshiftInfo
            {
                Id = shift.Id,
                Merchid = shift.MerchendiserId ?? -1,
                ShopId = shift.ShopId ?? -1,
                Starttime = Timestamp.FromDateTime(shift.StartTime.ToUniversalTime()),
                Endtime = Timestamp.FromDateTime(shift.EndTime.ToUniversalTime())
            };
        }

        public override async Task<StatusResponse> Remove(WorkshiftId request, ServerCallContext context)
        {
            try
            {
                await Task.Run(() => shifts.Remove(new Workshift { Id = request.Id }));
            }
            catch (Exception)
            {
                return new StatusResponse { Status = -1 };
            }

            return new StatusResponse { Status = 0 };
        }

        public override async Task<StatusResponse> Update(WorkshiftInfo request, ServerCallContext context)
        {
            try
            {
                await Task.Run(() => shifts.Update(new Workshift
                {
                    Id = request.Id,
                    ShopId = request.ShopId >= 0 ? (int?)request.ShopId : null,
                    MerchendiserId = request.Merchid >= 0 ? (int?)request.Merchid : null,
                    StartTime = request.Starttime.ToDateTime().ToLocalTime(),
                    EndTime = request.Endtime.ToDateTime().ToLocalTime()
                }));
            }
            catch (Exception)
            {
                return new StatusResponse { Status = -1 };
            }

            return new StatusResponse { Status = 0 };
        }
    }
}
