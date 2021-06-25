using CoordInfoProtocol;
using Domain.Core.Models;
using Domain.Services.Interfaces;
using Grpc.Core;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UserRepositoryService.Services
{
    public class CoordinatorService : CoordInfoRepository.CoordInfoRepositoryBase
    {
        private readonly IRepository<Coordinator> coordinators;

        public CoordinatorService(IRepository<Coordinator> coordinators)
        {
            this.coordinators = coordinators;
        }

        public override async Task<StatusResponse> Add(CoordInfo request, ServerCallContext context)
        {
            try
            {
                await Task.Run(() => coordinators.Add(new Coordinator
                {
                    FirstName = request.FirstName,
                    SecondName = request.SecondName,
                    Login = request.Login,
                    Password = request.Password
                }));
            }
            catch (Exception)
            {
                return new StatusResponse { Status = -1 };
            }

            return new StatusResponse { Status = 0 };
        }

        public override async Task<CoordList> All(Request request, ServerCallContext context)
        {
            var userlist = new CoordList();
            userlist.Users.AddRange((await
                Task.Run(() => coordinators.All())
                ).Select(m => new CoordInfo
                {
                    Id = m.Id,
                    FirstName = m.FirstName,
                    SecondName = m.SecondName,
                    Login = m.Login,
                    Password = m.Password
                }));

            return userlist;
        }

        public override async Task<CoordInfo> Get(CoordId request, ServerCallContext context)
        {
            var user = await Task.Run(() => coordinators.Get(request.Id));

            return new CoordInfo
            {
                Id = user.Id,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                Login = user.Login,
                Password = user.Password
            };
        }

        public override async Task<StatusResponse> Remove(CoordId request, ServerCallContext context)
        {
            try
            {
                await Task.Run(() => coordinators.Remove(new Coordinator { Id = request.Id }));
            }
            catch (Exception)
            {
                return new StatusResponse { Status = -1 };
            }

            return new StatusResponse { Status = 0 };
        }

        public override async Task<StatusResponse> Update(CoordInfo request, ServerCallContext context)
        {
            try
            {
                await Task.Run(() => coordinators.Update(new Coordinator
                {
                    Id = request.Id,
                    FirstName = request.FirstName,
                    SecondName = request.SecondName,
                    Login = request.Login,
                    Password = request.Password
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
