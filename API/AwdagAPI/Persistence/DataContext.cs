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
        public DbSet<Fish> Fishes { get; set; }
        public DbSet<PhysicalStatistic> PhysicalStatistics { get; set; }
        public DbSet<Aquarium> Aquariums { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasKey(x => x.Id);

            ConfigureCompositeKeys(builder);
            ConfigureCascadeProperties(builder);
            ConfigureEnums(builder);

            builder.Entity<Fish>()
                .HasOne(a => a.PhysicalStatistic)
                .WithOne(b => b.Fish)
                .HasForeignKey<PhysicalStatistic>(b => b.FishId);

            builder.Entity<Aquarium>()
                .HasMany(c => c.Fishes)
                .WithOne(e => e.Aquarium)
                .OnDelete(DeleteBehavior.Cascade);
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