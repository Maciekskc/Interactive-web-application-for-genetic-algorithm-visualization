using Domain.Models;
using Domain.Models.Entities;
using Domain.Models.Entities.Association;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
        public DbSet<ImageCategory> ImageCategories { get; set; }
        public DbSet<PersonalizedVoucherUser> PersonalizedVoucherUsers { get; set; }
        public DbSet<ProductTypeParameterProductType> ProductTypeParameterProductTypes { get; set; }

        // TABLES
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AnonymousAddress> AnonymousAddresses { get; set; }
        public DbSet<AnonymousOrder> AnonymousOrders { get; set; }
        public DbSet<ArchivedPersonalData> ArchivedPersonalData { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<InvoiceCustomer> InvoiceCustomers { get; set; }
        public DbSet<InvoiceData> InvoiceData { get; set; }
        public DbSet<InvoiceVendor> InvoiceVendors { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PersonalizedVoucher> PersonalizedVouchers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductTransformation> ProductTransformations { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductTypeParameter> ProductTypeParameters { get; set; }
        public DbSet<ProductTypePricing> ProductTypePricings { get; set; }
        public DbSet<PublicVoucher> PublicVouchers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<UsedPersonalizedVoucher> UsedPersonalizedVouchers { get; set; }
        public DbSet<UsedPublicVoucher> UsedPublicVouchers { get; set; }

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
            builder.Entity<ImageCategory>().HasKey(ic => new { ic.CategoryId, ic.ImageId});
            builder.Entity<ProductTypeParameterProductType>().HasKey(ptppt => new { ptppt.ProductTypeId, ptppt.ProductTypeParameterId});
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