using Domain.Core.Models;
using Domain.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoordinatorControls.Services
{
    public class ApiWorkshiftControlService : IWorkshiftControlService
    {
        public static string Protocol { get; set; } = "https";

        public static string Host { get; set; } = "localhost";

        public static string Method { get; set; } = "shifts";

        public static int Port { get; set; } = 5001;


        private readonly UriBuilder builder = new UriBuilder
        {
            Scheme = Protocol,
            Host = Host,
            Path = Method,
            Port = Port
        };

        public async Task<Workshift> GetWorkshift(Authed<int> id) //TODO: Api рабочих смен, отображение в панели координатора
        {
            var client = new HttpClient();
            var b = new UriBuilder(builder.Uri)
            {
                Path = Method + $"/{id.InnerData}",
                Query = $"login={id.Login}&password={id.Password}"
            };

            var resp = await client.GetAsync(b.Uri);

            if (resp.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (!resp.IsSuccessStatusCode)
                throw new Exception();

            var data = await resp.Content.ReadAsStringAsync();
            var shift = JsonConvert.DeserializeObject<Workshift>(data);
            return shift;
        }

        public async Task RemoveWorkshift(Authed<Workshift> workshift)
        {
            var client = new HttpClient();
            var b = new UriBuilder(builder.Uri)
            {
                Path = Method + $"/{workshift.InnerData.Id}",
                Query = $"login={workshift.Login}&password={workshift.Password}"
            };

            var resp = await client.DeleteAsync(b.Uri);

            if (resp.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (!resp.IsSuccessStatusCode)
                throw new Exception();
        }

        public async Task UpdateWorkshift(Authed<Workshift> workshift)
        {
            var client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(workshift, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }), Encoding.Default, "application/json");

            var resp = await client.PutAsync(builder.Uri, content);

            if (resp.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (!resp.IsSuccessStatusCode)
                throw new Exception();
        }

        public async Task EndWorkshift(Authed<int> id)
        {
            var shift = await GetWorkshift(id);
            shift.EndTime = DateTime.Now;

            var client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(new Authed<Workshift>
            {
                Login = id.Login,
                Password = id.Password,
                InnerData = shift
            }),
            Encoding.Default,
            "application/json");

            var resp = await client.PutAsync(builder.Uri, content);

            if (resp.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (!resp.IsSuccessStatusCode)
                throw new Exception();
        }

        public async Task<List<Workshift>> Workshifts(Authed user)
        {
            var client = new HttpClient();
            var b = new UriBuilder(builder.Uri)
            {
                Query = $"login={user.Login}&password={user.Password}"
            };

            var resp = await client.GetAsync(b.Uri);

            if (resp.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException();

            if (!resp.IsSuccessStatusCode)
                throw new Exception();

            var data = await resp.Content.ReadAsStringAsync();
            var shifts = JsonConvert.DeserializeObject<List<Workshift>>(data);
            return shifts;
        }
    }
}
