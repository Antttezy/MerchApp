using Domain.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MerchendiserApi.Interfaces
{
    public interface IWorkshiftReader
    {
        Task<List<Workshift>> GetWorkshifts(string login, string password);
    }
}
