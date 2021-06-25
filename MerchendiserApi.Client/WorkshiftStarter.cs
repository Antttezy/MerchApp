using Domain.Core.Models;
using MerchendiserApi.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MerchendiserApi.Client
{
    public class WorkshiftStarter : IWorkshiftStarter
    {
        readonly protected string uri = Settings.Host + "/merch/start";
        readonly protected string type = "application/json";

        public async Task StartWorkshift(Authed<int> shopId)
        {
            using var client = new HttpClient();
            var data = JsonConvert.SerializeObject(shopId);

            var response = await client.PostAsync(uri,
                    new StringContent(data,
                        Encoding.Default,
                        type
                    )
                );

            if (!response.IsSuccessStatusCode)
                throw new Exception();
        }
    }
}
