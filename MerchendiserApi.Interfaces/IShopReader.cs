using Domain.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MerchendiserApi.Interfaces
{
    public interface IShopReader
    {
        Task<List<Shop>> GetShops(string login, string password);
    }
}
