using Domain.Core.Models;
using Domain.Services.Interfaces;
using ShopProtocol;
using System.Linq;
using static ShopProtocol.ShopRepository;

namespace MerchendiseServer.Services
{
    public class ShopRepository : IRepository<Shop>
    {
        private readonly ShopRepositoryClient shopClient;

        public ShopRepository(ShopRepositoryClient shopClient)
        {
            this.shopClient = shopClient;
        }

        public void Add(Shop item)
        {
            shopClient.Add(new ShopInfo
            {
                Id = item.Id,
                Address = item.Address,
                Name = item.Name
            });
        }

        public IQueryable<Shop> All()
        {
            return shopClient.All(new Request()).Shops
                .Select(x =>
                new Shop
                {
                    Id = x.Id,
                    Address = x.Address,
                    Name = x.Name
                })
                .ToArray().AsQueryable();
        }

        public Shop Get(int id)
        {
            var info = shopClient.Get(new ShopId { Id = id });
            return new Shop
            {
                Id = info.Id,
                Name = info.Name,
                Address = info.Address
            };
        }

        public void Remove(Shop item)
        {
            shopClient.Remove(new ShopId { Id = item.Id });
        }

        public void Update(Shop item)
        {
            shopClient.Update(new ShopInfo
            {
                Id = item.Id,
                Address = item.Address,
                Name = item.Name
            });
        }
    }
}
