using BTLweb.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BTLweb.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Articles> Articles { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<Newsletters> Newsletters { get; set; }
        public DbSet<Comments> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Categories>().ToTable("Categories");
            modelBuilder.Entity<Articles>().ToTable("Articles");
            modelBuilder.Entity<Subscriptions>().ToTable("Subscriptions");
            modelBuilder.Entity<Newsletters>().ToTable("Newsletters");
            modelBuilder.Entity<Comments>().ToTable("Comments");

            base.OnModelCreating(modelBuilder);
        }
    }
}
