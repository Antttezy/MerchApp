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
    public class ApiShopControlService : IShopControlService
    {
        public static string Protocol { get; set; } = "https";

        public static string Host { get; set; } = "localhost";

        public static string Method { get; set; } = "shops";

        public static int Port { get; set; } = 5001;


        private readonly UriBuilder builder = new UriBuilder
        {
            Scheme = Protocol,
            Host = Host,
            Path = Method,
            Port = Port
        };

        public async Task AddShop(Authed<Shop> item)
        {
            using var client = new HttpClient();
            var resp = await client.PostAsync(builder.Uri, new StringContent(JsonConvert.SerializeObject(item), Encoding.Default, "application/json"));

            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (!resp.IsSuccessStatusCode)
                throw new Exception();
        }

        public async Task<Shop> GetShop(Authed<int> id)
        {
            using var client = new HttpClient();
            var b = new UriBuilder(builder.Uri);

            b.Path = Method + $"/{id.InnerData}";
            b.Query = $"login={id.Login}&password={id.Password}";
            var resp = await client.GetAsync(b.Uri);

            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (!resp.IsSuccessStatusCode)
                throw new Exception();

            var data = await resp.Content.ReadAsStringAsync();
            Shop shop = JsonConvert.DeserializeObject<Shop>(data);
            return shop;
        }

        public async Task RemoveShop(Authed<Shop> item)
        {
            using var client = new HttpClient();
            var b = new UriBuilder(builder.Uri);

            b.Path = Method + $"/{item.InnerData.Id}";
            b.Query = $"login={item.Login}&password={item.Password}";
            var resp = await client.DeleteAsync(b.Uri);

            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (!resp.IsSuccessStatusCode)
                throw new Exception();
        }

        public async Task<List<Shop>> Shops(Authed user)
        {
            using var client = new HttpClient();
            var b = new UriBuilder(builder.Uri);
            b.Query = $"login={user.Login}&password={user.Password}";

            var resp = await client.GetAsync(b.Uri);

            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (!resp.IsSuccessStatusCode)
                throw new Exception();

            var data = await resp.Content.ReadAsStringAsync();
            var shops = JsonConvert.DeserializeObject<List<Shop>>(data);
            return shops;
        }

        public async Task UpdateShop(Authed<Shop> item)
        {
            using var client = new HttpClient();
            var resp = await client.PutAsync(builder.Uri, new StringContent(JsonConvert.SerializeObject(item), Encoding.Default, "application/json"));

            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (!resp.IsSuccessStatusCode)
                throw new Exception();
        }
    }
}
