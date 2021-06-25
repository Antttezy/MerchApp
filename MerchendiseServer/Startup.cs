using Domain.Core.Models;
using Domain.Services.Interfaces;
using MerchendiseServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace MerchendiseServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddTransient<IEncryptor, MD5Encryptor>();
            services.AddTransient<IAuthenticator<Coordinator>, CoordinatorAuthenticator>();
            services.AddTransient<IAuthenticator<Merchendiser>, MerchendiserAuthenticator>();

            services.AddTransient<DatabaseSeeder>();

            services.AddGrpcClient<AuthProtocol.Authenticator.AuthenticatorClient>(o => o.Address = new Uri(Configuration["Services:AuthRPC"]));
            services.AddGrpcClient<MerchInfoProtocol.MerchInfoRepository.MerchInfoRepositoryClient>(o => o.Address = new Uri(Configuration["Services:DatabaseRPC"]));
            services.AddGrpcClient<CoordInfoProtocol.CoordInfoRepository.CoordInfoRepositoryClient>(o => o.Address = new Uri(Configuration["Services:DatabaseRPC"]));
            services.AddGrpcClient<ShopProtocol.ShopRepository.ShopRepositoryClient>(o => o.Address = new Uri(Configuration["Services:DatabaseRPC"]));
            services.AddGrpcClient<WorkshiftProtocol.WorkshiftRepository.WorkshiftRepositoryClient>(o => o.Address = new Uri(Configuration["Services:DatabaseRPC"]));

            services.AddTransient<IRepository<Merchendiser>, UserRepository>();
            services.AddTransient<IRepository<Coordinator>, UserRepository>();
            services.AddTransient<IRepository<Shop>, ShopRepository>();
            services.AddTransient<IRepository<Workshift>, WorkshiftRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
