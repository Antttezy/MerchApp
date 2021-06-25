using AuthProtocol;
using Domain.Core.Models;
using Domain.Services.Interfaces;
using System.Threading.Tasks;
using static AuthProtocol.Authenticator;

namespace MerchendiseServer.Services
{
    public class MerchendiserAuthenticator : IAuthenticator<Merchendiser>
    {
        private readonly AuthenticatorClient authenticatorClient;

        public MerchendiserAuthenticator(AuthenticatorClient authenticatorClient)
        {
            this.authenticatorClient = authenticatorClient;
        }

        public async Task<int> Authenticate(string login, string password)
        {
            var authResponse = await authenticatorClient.CheckLoginAsync(new AuthRequest { Login = login, Password = password, RoleId = (int)Role.Merchendiser }).ResponseAsync;
            return authResponse.Status;
        }

        public async Task<bool> IsCorrect(string login, string password)
        {
            return await Authenticate(login, password) == (int)Role.Merchendiser;
        }
    }
}
