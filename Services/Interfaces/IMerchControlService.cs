using Domain.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IMerchControlService
    {
        Task<List<Merchendiser>> Merches(string login, string password);

        Task<Merchendiser> GetMerch(int id, string login, string password);

        Task AddMerch(Authed<Merchendiser> merchendiser);

        Task UpdateMerch(Authed<Merchendiser> merchendiser);

        Task RemoveMerch(Authed<Merchendiser> merchendiser);

        Task EndShift(Authed<int> merchId);
    }
}
