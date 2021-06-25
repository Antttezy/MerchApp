using Domain.Core.Models;
using MerchendiserApi.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MerchendiserApi.Client
{
    public class WorkshiftReader : IWorkshiftReader
    {
        readonly protected string uri = Settings.Host + "/merch/shifts?login={0}&password={1}";
        readonly protected string type = "application/json";

        public async Task<List<Workshift>> GetWorkshifts(string login, string password)
        {
            using var client = new HttpClient();
            var request = string.Format(uri, login, password);

            var response = await client.GetAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = JsonConvert.DeserializeObject<List<Workshift>>(await response.Content.ReadAsStringAsync());
            return result;
        }
    }
}
