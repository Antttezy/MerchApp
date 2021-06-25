using Domain.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IShopControlService
    {
        Task<List<Shop>> Shops(Authed user);

        Task<Shop> GetShop(Authed<int> id);

        Task AddShop(Authed<Shop> item);

        Task UpdateShop(Authed<Shop> item);

        Task RemoveShop(Authed<Shop> item);
    }
}
