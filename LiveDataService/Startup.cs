using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using LiveDataService.LiveParameters.Hubs;
using LiveDataService.LiveParameters.Services;
using LiveDataService.Consumer.Services;
using LiveDataService.Mongo.Models;
using ZstdSharp.Unsafe;
using LiveDataService.Mongo.Services;
using AuthService.Middlewares.Extentions;

namespace LiveDataService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<KafkaConsumerService>();
            services.AddSingleton<ClientConnectionHandler>();
            services.AddSingleton<ParametersDistributionService>();
            services.AddSingleton<JsonUtilsService>();
            services.AddSingleton<MongoFramesService>();
            services.AddSingleton<TeleProcessorService>();
            services.AddHostedService<StartupService>();

            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("http://localhost:4200");
            }));
            services.AddSignalR()
                .AddJsonProtocol(options => {
                    options.PayloadSerializerOptions.PropertyNamingPolicy = null;
                });
            services.AddTokenAuthentication(new string[] {ParametersHub.Endpoint});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseTokenMessage();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ParametersHub>(ParametersHub.Endpoint);
                endpoints.MapControllers();
            });
        }
    }
}
