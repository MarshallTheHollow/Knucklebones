using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Knucklebones.Models;

namespace Knucklebones.Models
{
    public class ApplicationContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminLogin = "MarshallTheHollow";
            string adminPassword = "123456";
            User user = new User { Id = 1, Name = adminLogin, Password = adminPassword };
            modelBuilder.Entity<User>().HasData(new User[] { user });

            base.OnModelCreating(modelBuilder);
        }
    }
}
