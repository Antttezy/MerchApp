using Domain.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IWorkshiftControlService
    {
        Task<List<Workshift>> Workshifts(Authed user);

        Task<Workshift> GetWorkshift(Authed<int> id);

        Task UpdateWorkshift(Authed<Workshift> workshift);

        Task RemoveWorkshift(Authed<Workshift> workshift);

        Task EndWorkshift(Authed<int> id);
    }
}
