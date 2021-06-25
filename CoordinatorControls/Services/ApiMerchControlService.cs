using Domain.Core.Models;
using Domain.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoordinatorControls.Services
{
    public class ApiMerchControlService : IMerchControlService
    {
        public static string Protocol { get; set; } = "https";

        public static string Host { get; set; } = "localhost";

        public static string Method { get; set; } = "users";

        public static int Port { get; set; } = 5001;

        public async Task AddMerch(Authed<Merchendiser> merchendiser)
        {
            using HttpClient client = new HttpClient();

            var builder = new UriBuilder
            {
                Scheme = Protocol,
                Host = Host,
                Path = Method,
                Port = Port
            };

            var resp = await client.PostAsync(builder.Uri, new StringContent(JsonConvert.SerializeObject(merchendiser), Encoding.Default, "application/json"));

            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (!resp.IsSuccessStatusCode)
                throw new Exception();
        }

        public async Task<Merchendiser> GetMerch(int id, string login, string password)
        {
            using HttpClient client = new HttpClient();

            var builder = new UriBuilder
            {
                Scheme = Protocol,
                Host = Host,
                Path = Method + $"/{id}",
                Port = Port,
                Query = $"login={login}&password={password}"
            };

            var resp = await client.GetAsync(builder.Uri);

            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (!resp.IsSuccessStatusCode)
                throw new Exception();

            var stream = await resp.Content.ReadAsStringAsync();
            Merchendiser merchendiser = JsonConvert.DeserializeObject<Merchendiser>(stream);
            return merchendiser;
        }

        public async Task<List<Merchendiser>> Merches(string login, string password)
        {
            using HttpClient client = new HttpClient();

            var builder = new UriBuilder
            {
                Scheme = Protocol,
                Host = Host,
                Path = Method,
                Port = Port,
                Query = $"login={login}&password={password}"
            };

            var resp = await client.GetAsync(builder.Uri);

            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (!resp.IsSuccessStatusCode)
                throw new Exception();

            var stream = await resp.Content.ReadAsStringAsync();
            List<Merchendiser> merchendisers = JsonConvert.DeserializeObject<List<Merchendiser>>(stream);
            return merchendisers;
        }

        public async Task RemoveMerch(Authed<Merchendiser> merchendiser)
        {
            using HttpClient client = new HttpClient();

            var builder = new UriBuilder
            {
                Scheme = Protocol,
                Host = Host,
                Path = Method + $"/{merchendiser.InnerData.Id}",
                Port = Port,
                Query = $"login={merchendiser.Login}&password={merchendiser.Password}"
            };

            var resp = await client.DeleteAsync(builder.Uri);

            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (!resp.IsSuccessStatusCode)
                throw new Exception();
        }

        public async Task UpdateMerch(Authed<Merchendiser> merchendiser)
        {
            using HttpClient client = new HttpClient();

            var builder = new UriBuilder
            {
                Scheme = Protocol,
                Host = Host,
                Path = Method,
                Port = Port
            };

            var resp = await client.PutAsync(builder.Uri, new StringContent(JsonConvert.SerializeObject(merchendiser), Encoding.Default, "application/json"));

            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (!resp.IsSuccessStatusCode)
                throw new Exception();
        }

        public async Task EndShift(Authed<int> merchId)
        {
            var merch = await GetMerch(merchId.InnerData, merchId.Login, merchId.Password);
            merch.CurrentShift = null;
            merch.CurrentShiftId = null;

            var client = new HttpClient();
            var builder = new UriBuilder
            {
                Scheme = Protocol,
                Host = Host,
                Path = Method,
                Port = Port
            };

            var resp = await client.PutAsync(builder.Uri, new StringContent(JsonConvert.SerializeObject(new Authed<Merchendiser>
            {
                Login = merchId.Login,
                Password = merchId.Password,
                InnerData = merch
            }),
            Encoding.Default,
            "application/json"));

            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (!resp.IsSuccessStatusCode)
                throw new Exception();
        }
    }
}
