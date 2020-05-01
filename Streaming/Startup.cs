using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Streaming.Infraestructura;
using Streaming.Infraestructura.Repositories;
using Streaming.Infraestructura.Repositories.contracts;

namespace Streaming
{
    public class Startup
    {

        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // other service configurations go here
            // replace "YourDbContext" with the class name of your DbContext

            services.AddDbContext<MediaContext>(options => options
                // replace with your connection string
                .UseMySql("server=localhost;user id=root; password=dinocrisis;persistsecurityinfo=True;database=mysql", mySqlOptions => mySqlOptions
                    // replace with your Server Version and Type
                    .ServerVersion(new Version(8, 0, 19), ServerType.MySql)
                    .DisableBackslashEscaping()
             ));  

            


            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyMethod().AllowAnyHeader()
                           .AllowAnyOrigin(); //.WithOrigins("http://localhost:3000").

                    //.AllowCredentials();
                }));

            
            services.AddScoped<DbContext, MediaContext>();
            services.AddTransient<IMediaRepository, MediaRepository>();
            services.AddControllers();
            services.AddMvc();
         }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors( builder =>
            {
                builder.AllowAnyMethod().AllowAnyHeader()
                       .AllowAnyOrigin(); //.WithOrigins("http://localhost:3000").

                //.AllowCredentials();
            });

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllers();
            });

        }
    }


}
