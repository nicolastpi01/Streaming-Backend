using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Domain
{
    public class MediaContext : DbContext
    {
        public DbSet<Media> Media { get; set; }

        public MediaContext(DbContextOptions<MediaContext> dbContextOptions) : base(dbContextOptions)
        {
            if (Media.Count() == 0)
            {

            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            optionsBuilder.UseMySql("server=localhost;user id=root;persistsecurityinfo=True;database=tip_streaming;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Media>(entity =>
            {
                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Nombre).IsRequired();
                entity.Property(_ => _.Ruta).IsRequired();
            });
        }
    }
}
