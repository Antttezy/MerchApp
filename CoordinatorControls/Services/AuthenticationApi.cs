using Domain.Core.Models;
using Domain.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoordinatorControls.Services
{
    public class AuthenticationApi : IAuthenticator<Coordinator>
    {
        private readonly static UriBuilder builder = new UriBuilder
        {
            Scheme = "https",
            Host = "localhost",
            Path = "coord/login",
            Port = 5001
        };

        public async Task<bool> IsCorrect(string login, string password)
        {
            var client = new HttpClient();
            var resp = await client.PostAsync(
                builder.Uri,
                new StringContent(JsonConvert.SerializeObject(new Authed
                {
                    Login = login,
                    Password = password
                }),
                Encoding.Default,
                "application/json"
            ));

            if (!resp.IsSuccessStatusCode)
                throw new HttpRequestException();

            var result = JsonConvert.DeserializeObject<bool>(await resp.Content.ReadAsStringAsync());
            return result;
        }
    }
}
