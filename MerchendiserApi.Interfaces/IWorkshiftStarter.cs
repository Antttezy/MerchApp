using Domain.Core.Models;
using System.Threading.Tasks;

namespace MerchendiserApi.Interfaces
{
    public interface IWorkshiftStarter
    {
        Task StartWorkshift(Authed<int> shopId);
    }
}
