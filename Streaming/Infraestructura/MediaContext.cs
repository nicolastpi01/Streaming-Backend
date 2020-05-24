using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Streaming.Infraestructura.Entities;

namespace Streaming.Infraestructura
{
    public class MediaContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "Streaming";
        public DbSet<MediaEntity> Medias { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        //public DbSet<UserEntity> Users { get; set; }


        public MediaContext(DbContextOptions<MediaContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //quisa reviente porque no va aca!!!
            modelBuilder.Entity<MediaEntity>(entity =>
            {
                entity.ToTable("Media");
                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Nombre).IsRequired();
                entity.Property(_ => _.Ruta).IsRequired();
                entity.Property(_ => _.Descripcion);
                //entity.Property(_ => _.Tags);
                entity.Property(_ => _.Autor);
                entity.Property(_ => _.Imagen).IsRequired();
            });

            modelBuilder.Entity<TagEntity>(entity =>
            {
                entity.ToTable("Tag");
                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Nombre).IsRequired();
                /*entity.HasOne(t => t.Media)
                    .WithMany(t => t.Tags)
                    .HasForeignKey(t => t.IdMedia);*/

            });

            modelBuilder.Entity<TagEntity>(entity =>
            {
                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Nombre).IsRequired();
                entity.HasOne(t => t.Media)
                    .WithMany(t => t.Tags)
                    .HasForeignKey(t => t.IdMedia);

            });
            
        }
    }
}
