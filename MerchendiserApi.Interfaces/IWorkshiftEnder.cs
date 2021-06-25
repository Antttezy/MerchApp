using Domain.Core.Models;
using System.Threading.Tasks;

namespace MerchendiserApi.Interfaces
{
    public interface IWorkshiftEnder
    {
        Task EndWorkshift(Authed info);
    }
}
