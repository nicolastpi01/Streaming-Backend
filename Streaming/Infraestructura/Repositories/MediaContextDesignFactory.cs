using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Streaming.Infraestructura.Repositories
{
    public class MediaContextDesignFactory : IDesignTimeDbContextFactory<MediaContext>
    {
        public MediaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MediaContext>()
           .UseMySql("server = localhost; user id = root; password=QLN*t~6gj=9hz-XU; persistsecurityinfo = True; database = TIP_STREAMING; allowuservariables = True");
            return new MediaContext(optionsBuilder.Options);
        }
    }
}
