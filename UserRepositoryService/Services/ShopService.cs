using Domain.Core.Models;
using Domain.Services.Interfaces;
using Grpc.Core;
using ShopProtocol;
using System;
using System.Linq;
using System.Threading.Tasks;
using static ShopProtocol.ShopRepository;

namespace UserRepositoryService.Services
{
    public class ShopService : ShopRepositoryBase
    {
        private readonly IRepository<Shop> shops;

        public ShopService(IRepository<Shop> shops)
        {
            this.shops = shops;
        }

        public override async Task<StatusResponse> Add(ShopInfo request, ServerCallContext context)
        {
            try
            {
                await Task.Run(() => shops.Add(new Shop
                {
                    Address = request.Address,
                    Name = request.Name
                }));
            }
            catch (Exception)
            {
                return new StatusResponse { Status = -1 };
            }

            return new StatusResponse { Status = 0 };
        }

        public override async Task<ShopList> All(Request request, ServerCallContext context)
        {
            var list = new ShopList();
            list.Shops.AddRange((await Task.Run(() =>
            shops.All()))
            .Select(x => new ShopInfo
            {
                Id = x.Id,
                Address = x.Address,
                Name = x.Name
            }));

            return list;
        }

        public override async Task<ShopInfo> Get(ShopId request, ServerCallContext context)
        {
            var shop = await Task.Run(() => shops.Get(request.Id));

            return new ShopInfo
            {
                Id = shop.Id,
                Address = shop.Address,
                Name = shop.Name
            };
        }

        public override async Task<StatusResponse> Remove(ShopId request, ServerCallContext context)
        {
            try
            {
                await Task.Run(() => shops.Remove(new Shop { Id = request.Id }));
            }
            catch (Exception)
            {
                return new StatusResponse { Status = -1 };
            }

            return new StatusResponse { Status = 0 };
        }

        public override async Task<StatusResponse> Update(ShopInfo request, ServerCallContext context)
        {
            try
            {
                await Task.Run(() => shops.Update(new Shop
                {
                    Id = request.Id,
                    Address = request.Address,
                    Name = request.Name
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
