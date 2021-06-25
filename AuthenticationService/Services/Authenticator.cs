using AuthProtocol;
using Domain.Core.Models;
using Domain.Services.Interfaces;
using Grpc.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using static CoordInfoProtocol.CoordInfoRepository;
using static MerchInfoProtocol.MerchInfoRepository;

namespace AuthenticationService.Services
{
    public class Authenticator : AuthProtocol.Authenticator.AuthenticatorBase
    {
        public Authenticator(MerchInfoRepositoryClient mClient, CoordInfoRepositoryClient cClient, IEncryptor encryptor)
        {
            MClient = mClient;
            CClient = cClient;
            Encryptor = encryptor;
        }

        public MerchInfoRepositoryClient MClient { get; }
        public CoordInfoRepositoryClient CClient { get; }
        public IEncryptor Encryptor { get; }

        public override async Task<AuthResponse> CheckLogin(AuthRequest request, ServerCallContext context)
        {
            if (request.RoleId == (int)Role.Merchendiser)
            {
                MerchInfoRepositoryClient merchClient = MClient;
                var response = await merchClient.AllAsync(new MerchInfoProtocol.Request()).ResponseAsync;
                var merch = response.Users.FirstOrDefault(m => string.Compare(m.Login, request.Login, StringComparison.OrdinalIgnoreCase) == 0 &&
                string.Compare(m.Password, Encryptor.Encrypt(request.Password)) == 0);

                return new AuthResponse { Status = merch != null ? (int)Role.Merchendiser : -1 };
            }
            else if (request.RoleId == (int)Role.Coordinator)
            {
                CoordInfoRepositoryClient coordClient = CClient;
                var response = await coordClient.AllAsync(new CoordInfoProtocol.Request()).ResponseAsync;
                var coord = response.Users.FirstOrDefault(c => string.Compare(c.Login, request.Login, StringComparison.OrdinalIgnoreCase) == 0 &&
                string.Compare(c.Password, Encryptor.Encrypt(request.Password), StringComparison.Ordinal) == 0);

                return new AuthResponse { Status = coord != null ? (int)Role.Coordinator : -1 };
            }
            else
            {
                return new AuthResponse { Status = -1 };
            }
        }
    }
}
