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

        /// ASSOCIATIONS
         public DbSet<ParentChild> ParentChild { get; set; }

        // TABLES 
        public DbSet<Fish> Fishes { get; set; }
        public DbSet<PhysicalStatistic> PhysicalStatistics { get; set; }
        public DbSet<Aquarium> Aquariums { get; set; }
        public DbSet<SetOfMutations> SetOfMutations { get; set; }
        public DbSet<LifeParameters> LifeParameters { get; set; }
        public DbSet<LifeTimeStatistic> LifeTimeStatistic { get; set; }
        public DbSet<Food> Foods { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasKey(x => x.Id);

            ConfigureCompositeKeys(builder);
            ConfigureCascadeProperties(builder);
            ConfigureEnums(builder);

            builder.Entity<ParentChild>()
                .HasOne<Fish>(x => x.Parent)
                .WithMany(x => x.Childs)
                .HasForeignKey(x => x.ChildId)
                .IsRequired();


            //dodaj  w 2 drugą strone


            builder.Entity<LifeTimeStatistic>()
                .HasOne<Fish>(x => x.Fish)
                .WithOne(x => x.LifeTimeStatistic)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<PhysicalStatistic>()
                .HasOne<Fish>(x => x.Fish)
                .WithOne(x => x.PhysicalStatistic)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<LifeParameters>()
                .HasOne<Fish>(x => x.Fish)
                .WithOne(x => x.LifeParameters)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<SetOfMutations>()
                .HasOne<Fish>(x => x.Fish)
                .WithOne(x => x.SetOfMutations)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void ConfigureEnums(ModelBuilder builder)
        {
        }

        private static void ConfigureCompositeKeys(ModelBuilder builder)
        {
            builder.Entity<ParentChild>()
                .HasKey(o => new { o.ParentId, o.ChildId });
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