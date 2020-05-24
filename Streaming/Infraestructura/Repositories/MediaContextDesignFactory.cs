using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Streaming.Infraestructura.Repositories
{
    public class MediaContextDesignFactory : IDesignTimeDbContextFactory<MediaContext>
    {
        public MediaContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MediaContext>()
                .UseMySql(configuration["ConexionMySql"]);
            return new MediaContext(optionsBuilder.Options);
        }
    }
}
