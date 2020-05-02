using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Streaming.Infraestructura.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Streaming.Infraestructura.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        // UserManager?
        public UserRepository(DbContext context) : base(context)
        {
        }
    }

    public class UserContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "User";
        public DbSet<UserEntity> Users { get; set; }
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(_ => _.Id);
                entity.Property(_ => _.Alias).IsRequired();
                entity.Property(_ => _.Mail).IsRequired();
            });
        }

        public static async Task Init(IServiceProvider serviceProvider,List<string> userList)
        {/*
                var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

                foreach (var userName in userList)
                {
                    var userPassword = GenerateSecurePassword();
                    var userId = await EnsureUser(userManager, userName, userPassword);

                    NotifyUser(userName, userPassword);
                }*/
        }

        private static async Task<string> EnsureUser(UserManager<IdentityUser> userManager, string userName, string userPassword)
        {
                var user = await userManager.FindByNameAsync(userName);

                if (user == null)
                {
                    user = new IdentityUser(userName)
                    {
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(user, userPassword);
                }

                return user.Id;
            }

        }
}
