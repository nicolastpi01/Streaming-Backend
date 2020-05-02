using DotNetEnv;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                var context = services.GetService<MediaContext>();
                //var config = host.Services.GetRequiredService<IConfiguration>();
                //var userList = config.GetSection("userList").Get<List<string>>();

                new MediaContextSeed().SeedAsync(context)
                .Wait();
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



