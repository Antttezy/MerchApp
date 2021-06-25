using Domain.Services.Interfaces;
using System.Linq;
using System.Text;

namespace AuthenticationService.Services
{
    public class MD5Encryptor : IEncryptor
    {
        public string Encrypt(string message)
        {
            using var hash = System.Security.Cryptography.MD5.Create();
            return string.Join(string.Empty,
                hash.ComputeHash(Encoding.Default.GetBytes(message))
                .Select(b => b.ToString("x2")));
        }
    }
}
