using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Streaming.Infraestructura.Repositories
{
    public class MediaContextDesignFactory : IDesignTimeDbContextFactory<MediaContext>
    {
        public MediaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MediaContext>()
           .UseMySql("server = localhost; user id = root; password=dinocrisis; persistsecurityinfo = True; database = mysql; allowuservariables = True");
            return new MediaContext(optionsBuilder.Options);
        }
    }
}
