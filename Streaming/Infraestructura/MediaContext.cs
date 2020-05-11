using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Streaming.Infraestructura.Entities;

namespace Streaming.Infraestructura
{
    public class MediaContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "Media";
        public DbSet<MediaEntity> Medias { get; set; }

        public DbSet<TagEntity> Tags { get; set; }

        //public DbSet<UserEntity> Users { get; set; }

        public MediaContext(DbContextOptions<MediaContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MediaEntity>(entity =>
            {
                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Nombre).IsRequired();
                entity.Property(_ => _.Ruta).IsRequired();
                entity.Property(_ => _.Descripcion);
                //entity.Property(_ => _.Tags);
                entity.Property(_ => _.Autor);
            });

            modelBuilder.Entity<TagEntity>(entity =>
            {
                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Nombre).IsRequired();
                //entity.HasOne(p => p.Media)
                //.WithMany(b => b.Tags);
                //.HasForeignKey(p => p.MediaId);

            });
            
        }
    }

    public class CommonContextDesignFactory : IDesignTimeDbContextFactory<MediaContext>
    {
        public MediaContext CreateDbContext(string[] args)
        {
             var optionsBuilder = new DbContextOptionsBuilder<MediaContext>()
            .UseMySql("server = localhost; user id = root; password=dinocrisis; persistsecurityinfo = True; database = mysql; allowuservariables = True");
            return new MediaContext(optionsBuilder.Options);
        }
    }

}

