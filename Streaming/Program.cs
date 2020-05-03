using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetEnv;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Streaming.Infraestructura;

namespace Streaming
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = InitWebHost(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                //var context = services.ServiceProvider.GetService<MediaContext>();
                var context = services.GetService<MediaContext>();
                //DataSeeder.SeedCountries(context);
                new MediaContextSeed().SeedAsync(context);
            }
            host.Run();
        }

        public static IHostBuilder InitWebHost(string[] args){
            Env.Load();
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>{
                    webBuilder.UseStartup<Startup>();
                 });
        }



    }
}



