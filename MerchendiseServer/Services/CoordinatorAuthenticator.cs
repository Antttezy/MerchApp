using AuthProtocol;
using Domain.Core.Models;
using Domain.Services.Interfaces;
using System.Threading.Tasks;
using static AuthProtocol.Authenticator;

namespace MerchendiseServer.Services
{
    public class CoordinatorAuthenticator : IAuthenticator<Coordinator>
    {
        private readonly AuthenticatorClient authenticatorClient;

        public CoordinatorAuthenticator(AuthenticatorClient authenticatorClient)
        {
            this.authenticatorClient = authenticatorClient;
        }

        public async Task<int> Authenticate(string login, string password)
        {
            var authResponse = await authenticatorClient.CheckLoginAsync(new AuthRequest { Login = login, Password = password, RoleId = (int)Role.Coordinator }).ResponseAsync;
            return authResponse.Status;
        }

        public async Task<bool> IsCorrect(string login, string password)
        {
            return await Authenticate(login, password) == (int)Role.Coordinator;
        }
    }
}
