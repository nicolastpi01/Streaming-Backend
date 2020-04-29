using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Streaming.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Infraestructura
{
    public class MediaContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "Media";

        public DbSet<MediaEntity> Media { get; set; }
        public MediaContext(DbContextOptions<MediaContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MediaEntity>(entity =>
            {
                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.monedaOrigen).IsRequired();
                entity.Property(_ => _.monedaDestino).IsRequired();
                entity.Property(_ => _.porcentaje).IsRequired();
            });
        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySql("server=localhost;database=databaseStreaming;user=user;password=password");
            
            optionsBuilder.UseMySql("server=localhost;user id=root;persistsecurityinfo=True;database=mysql");
            base.OnConfiguring(optionsBuilder);
        } 


    }

    /*
    public class CommonContextDesignFactory : IDesignTimeDbContextFactory<MediaContext>
    {
        public MediaContext CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<MediaContext>()
            .UseMySql("server=localhost;user id=root; password=dinocrisis;persistsecurityinfo=True;database=mysql");  
            //.UseSqlServer(@"Data Source = AD_EXTO_TOPS,4000; Initial Catalog = Tops; User ID = usr_tops; Password = 123456789; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            return new MediaContext(optionsBuilder.Options);
        }
    } */
}


