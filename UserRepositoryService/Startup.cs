using Domain.Core.Models;
using Domain.Services.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserRepositoryService.Services;

namespace UserRepositoryService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddDbContext<UserContext>(options => options.UseSqlServer(Configuration.GetConnectionString("StaffContext")));
            services.AddTransient<IRepository<Merchendiser>, MerchendiserDbRepository>();
            services.AddTransient<IRepository<Coordinator>, CoordinatorDbRepository>();
            services.AddTransient<IRepository<Shop>, ShopDbRepository>();
            services.AddTransient<IRepository<Workshift>, WorkshiftDbRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<MerchendiserService>();
                endpoints.MapGrpcService<CoordinatorService>();
                endpoints.MapGrpcService<ShopService>();
                endpoints.MapGrpcService<WorkshiftService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
