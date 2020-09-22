using Domain.Models;
using Domain.Models.Entities;
using Domain.Models.Entities.Association;
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
        public DbSet<FishAquarium> FishAquatiums { get; set; }
        public DbSet<FishPhysicalStatistic> FishPhysicalStatistics { get; set; }

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
        }

        private static void ConfigureEnums(ModelBuilder builder)
        {

        }

        private static void ConfigureCompositeKeys(ModelBuilder builder)
        {
        }

        private static void ConfigureCascadeProperties(ModelBuilder builder)
        {
            builder.Entity<FishAquarium>()
                .HasMany<Fish>()
                .WithOne(sq => sq.FishAquarium)
                .HasForeignKey(x => x.SolvedTestId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}