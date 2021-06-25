using Domain.Core.Models;
using MerchendiserApi.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MerchendiserApi.Client
{
    public class WorkshiftEnder : IWorkshiftEnder
    {
        readonly protected string uri = Settings.Host + "/merch/end";
        readonly protected string type = "application/json";

        public async Task EndWorkshift(Authed info)
        {
            using var client = new HttpClient();
            var data = JsonConvert.SerializeObject(info);

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
