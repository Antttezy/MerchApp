using Domain.Core.Models;
using Domain.Services.Interfaces;
using Grpc.Core;
using MerchInfoProtocol;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UserRepositoryService.Services
{
    public class MerchendiserService : MerchInfoRepository.MerchInfoRepositoryBase
    {
        private readonly IRepository<Merchendiser> merchendisers;

        public MerchendiserService(IRepository<Merchendiser> merchendisers)
        {
            this.merchendisers = merchendisers;
        }

        public override async Task<StatusResponse> Add(MerchInfo request, ServerCallContext context)
        {
            try
            {
                await Task.Run(() => merchendisers.Add(new Merchendiser
                {
                    FirstName = request.FirstName,
                    SecondName = request.SecondName,
                    Login = request.Login,
                    Password = request.Password,
                    CurrentShiftId = request.ShiftId >= 0 ? (int?)request.ShiftId : null
                }));
            }
            catch (Exception)
            {
                return new StatusResponse { Status = -1 };
            }

            return new StatusResponse { Status = 0 };
        }

        public override async Task<MerchList> All(Request request, ServerCallContext context)
        {
            var userlist = new MerchList();
            userlist.Users.AddRange((await
                Task.Run(() => merchendisers.All())
                ).Select(m => new MerchInfo
                {
                    Id = m.Id,
                    FirstName = m.FirstName,
                    SecondName = m.SecondName,
                    Login = m.Login,
                    Password = m.Password,
                    ShiftId = m.CurrentShiftId ?? -1
                }));

            return userlist;
        }

        public override async Task<MerchInfo> Get(MerchId request, ServerCallContext context)
        {
            var user = await Task.Run(() => merchendisers.Get(request.Id));

            return new MerchInfo
            {
                Id = user.Id,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                Login = user.Login,
                Password = user.Password,
                ShiftId = user.CurrentShiftId ?? -1
            };
        }

        public override async Task<StatusResponse> Remove(MerchId request, ServerCallContext context)
        {
            try
            {
                await Task.Run(() => merchendisers.Remove(new Merchendiser { Id = request.Id }));
            }
            catch (Exception)
            {
                return new StatusResponse { Status = -1 };
            }

            return new StatusResponse { Status = 0 };
        }

        public override async Task<StatusResponse> Update(MerchInfo request, ServerCallContext context)
        {
            try
            {
                await Task.Run(() => merchendisers.Update(new Merchendiser
                {
                    Id = request.Id,
                    FirstName = request.FirstName,
                    SecondName = request.SecondName,
                    Login = request.Login,
                    Password = request.Password,
                    CurrentShiftId = request.ShiftId >= 0 ? (int?)request.ShiftId : null
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
