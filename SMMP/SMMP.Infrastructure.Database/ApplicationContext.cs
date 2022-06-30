using System;
using Microsoft.EntityFrameworkCore;
using SMMP.Core.Models;
using SMMP.Core.Models.Authentication;
using SMMP.Infrastructure.Database.Interfaces;

namespace SMMP.Infrastructure.Database
{
    public class ApplicationContext : DbContext, IDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Execution> Executions { get; set; }

        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
