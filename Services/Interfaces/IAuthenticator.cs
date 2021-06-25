using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IAuthenticator<T>
    {
        Task<bool> IsCorrect(string login, string password);
    }
}
