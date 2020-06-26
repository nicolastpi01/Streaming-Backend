using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Streaming.Infraestructura;
using Streaming.Infraestructura.Repositories.contracts;
using Streaming.Infraestructura.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                options.Audience = Configuration["Auth0:Audience"];
            });

            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyMethod().AllowAnyHeader()
                           .AllowAnyOrigin(); //.WithOrigins("http://localhost:3000").
                           //.AllowCredentials();
                }));

            services.AddDbContextPool<MediaContext>(options => options
                
                .UseMySql($"server=localhost;user id={Configuration["SQLUSER"]};Pwd={Configuration["SQLPASS"]};persistsecurityinfo=True;database={Configuration["DATABASENAME"]};", mySqlOptions => mySqlOptions
                
                    .ServerVersion(new Version(8, 0, 18), ServerType.MySql)
                    
            ));

            //services.AddDbContext<MediaContext>();
            services.AddScoped<DbContext, MediaContext>();
            services.AddTransient<IStreamRepository, StreamRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            //ConfigureAuth(services);
            services.AddControllers();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime)
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
                       .AllowAnyOrigin();
            });

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();
            });
        }

        public void ConfigureAuth(IServiceCollection services)
        {
            // Add authentication services
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect("Auth0", options => {
                // Configure the scope
                options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");

                // Set the correct name claim type
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "https://schemas.quickstarts.com/roles"
                };
            });
            
        }
    }
}
