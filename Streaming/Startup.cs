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
using Streaming.Domain;

using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;

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

        }

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
                       .AllowAnyOrigin();

                //.AllowCredentials();
            });

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<AsyncEnumerableHub>("/AsyncEnumerableHub");
                endpoints.MapControllers();
            });
        }
    }
}
