using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Streaming.Infraestructura.Entities;

namespace Streaming.Infraestructura
{
    public class MediaContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "Media";
        public DbSet<MediaEntity> Medias { get; set; }
        //public DbSet<UserEntity> Users { get; set; }

        public MediaContext(DbContextOptions<MediaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MediaEntity>(entity =>
            {
                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Nombre).IsRequired();
                entity.Property(_ => _.Ruta).IsRequired();
            });
            /*
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Alias).IsRequired();
                entity.Property(_ => _.Mail).IsRequired();
            });*/
        }
    }
     
 }

