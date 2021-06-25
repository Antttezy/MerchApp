using Domain.Core.Models;
using MerchendiserApi.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MerchendiserApi.Client
{
    public class Authenticator : IAuthenticator
    {
        readonly protected string uri = Settings.Host + "/merch/login";
        readonly protected string type = "application/json";

        public async Task<bool> IsCorrect(string login, string password)
        {
            using var client = new HttpClient();
            var auth = new Authed
            {
                Login = login,
                Password = password
            };

            var data = JsonConvert.SerializeObject(auth);
            var response = await client.PostAsync(uri, new StringContent(data, Encoding.Default, type));

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException();

            var result = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
            return result;
        }
    }
}
