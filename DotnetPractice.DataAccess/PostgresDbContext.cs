using DotnetPractice.DataAccess.Models;
using DotnetPractice.DataAccess.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DotnetPractice.DataAccess
{
    public class PostgresDbContext : IdentityDbContext<User, Role, string>
    {
        public static Action<DbContextOptionsBuilder> Connection = (c => c.UseNpgsql(@"Server=localhost;Port=5432;Database=dotnetpracticedb;User Id=postgres;Password=1234"));
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(@"Server=localhost;Port=5432;Database=dotnetpracticedb;User Id=postgres;Password=1234")
            .UseSeeding((context, _) =>
            {
                //var userManager = context.GetService<UserManager<User>>(); 
                var seeder = new Seeder(context.GetService<RoleManager<Role>>());
                seeder.CreateRoles();
                Console.WriteLine("Seeding Done");
            })
            .UseAsyncSeeding(async (context, _, ct) =>
             {
                 //var userManager = context.GetService<UserManager<User>>(); 
                 var seeder = new Seeder(context.GetService<RoleManager<Role>>());
                 await seeder.CreateRolesAsync();
                 Console.WriteLine("Seeding Done");
             });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Product> Products { get; set; }



    }
    
}
