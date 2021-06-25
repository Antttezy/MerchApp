using Domain.Core.Models;
using MerchendiserApi.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MerchendiserApi.Client
{
    public class ShopReader : IShopReader
    {
        readonly protected string uri = Settings.Host + "/merch/shops?login={0}&password={1}";
        readonly protected string type = "application/json";

        public async Task<List<Shop>> GetShops(string login, string password)
        {
            using var client = new HttpClient();
            var request = string.Format(uri, login, password);

            var response = await client.GetAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = JsonConvert.DeserializeObject<List<Shop>>(await response.Content.ReadAsStringAsync());
            return result;
        }
    }
}
