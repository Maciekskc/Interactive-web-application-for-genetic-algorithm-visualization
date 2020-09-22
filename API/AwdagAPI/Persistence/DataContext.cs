using Domain.Models;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        // BASIC TABELS
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<MaintenanceMessage> MaintenanceMessages { get; set; }

        /// ASSOCIATIONS
 

        // TABLES 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasKey(x => x.Id);

            ConfigureCompositeKeys(builder);
            ConfigureCascadeProperties(builder);
            ConfigureEnums(builder);
        }

        private static void ConfigureEnums(ModelBuilder builder)
        {

        }

        private static void ConfigureCompositeKeys(ModelBuilder builder)
        {
        }

        private static void ConfigureCascadeProperties(ModelBuilder builder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}