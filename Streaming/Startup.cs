using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Streaming
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
            
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyMethod().AllowAnyHeader()
                           .AllowAnyOrigin(); //.WithOrigins("http://localhost:3000").

                    //.AllowCredentials();
                }));

            //services.AddSignalRCore();
            services.AddControllers();
            services.AddMvc();
            //services.AddScoped<IAzureVideoStreamService, AzureVideoStreamService>();
            //services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors( builder =>
            {
                builder.AllowAnyMethod().AllowAnyHeader()
                       .AllowAnyOrigin(); //.WithOrigins("http://localhost:3000").

                //.AllowCredentials();
            });

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<AsyncEnumerableHub>("/AsyncEnumerableHub");
                endpoints.MapControllers();
            });

            /*
            hostApplicationLifetime.ApplicationStarted.Register(() =>
            {

                var serviceProvider = app.ApplicationServices;
                var Hub = (IHubContext<AsyncEnumerableHub>)serviceProvider.GetService(typeof(IHubContext<AsyncEnumerableHub>));

                var timer = new System.Timers.Timer(1000);
                timer.Enabled = true;
                timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e) {
                    Hub.Clients.All.SendAsync("setTime", DateTime.Now.ToString("dddd d MMMM yyyy HH:mm:ss"));
                };
                timer.Start();
            }); */
        }
    }
}
