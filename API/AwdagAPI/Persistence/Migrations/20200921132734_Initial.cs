using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnonymousAddresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymousAddresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 255, nullable: false),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 512, nullable: false),
                    AdobeStockImageId = table.Column<int>(nullable: true),
                    IsFeatured = table.Column<bool>(nullable: false),
                    ImageProvider = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceVendors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceVendors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalizedVouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    UsesPerUser = table.Column<int>(nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false),
                    DiscountInPercents = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalizedVouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypeParameters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    TaxAmount = table.Column<int>(nullable: false),
                    TaxType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PublicVouchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    UseLimit = table.Column<int>(nullable: false),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false),
                    DiscountInPercents = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicVouchers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnonymousOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false),
                    CurrencyType = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    FinishedAt = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    AnonymousAddressId = table.Column<int>(nullable: false),
                    ShipmentId = table.Column<Guid>(nullable: false),
                    PaymentId = table.Column<Guid>(nullable: false),
                    InvoiceDataId = table.Column<Guid>(nullable: true),
                    UsedPublicVoucherId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnonymousOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnonymousOrders_AnonymousAddresses_AnonymousAddressId",
                        column: x => x.AnonymousAddressId,
                        principalTable: "AnonymousAddresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArchivedPersonalData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserData = table.Column<int>(nullable: false),
                    OldValue = table.Column<string>(nullable: true),
                    NewValue = table.Column<string>(nullable: true),
                    ChangedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedPersonalData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArchivedPersonalData_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    TaxNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceCustomers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Token = table.Column<string>(nullable: false),
                    JwtId = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    Used = table.Column<bool>(nullable: false),
                    Invalidated = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Token);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImageCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageCategories", x => new { x.CategoryId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_ImageCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageCategories_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalizedVoucherUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    PersonalizedVoucherId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalizedVoucherUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalizedVoucherUsers_PersonalizedVouchers_PersonalizedVoucherId",
                        column: x => x.PersonalizedVoucherId,
                        principalTable: "PersonalizedVouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalizedVoucherUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypeParameterProductTypes",
                columns: table => new
                {
                    ProductTypeId = table.Column<int>(nullable: false),
                    ProductTypeParameterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypeParameterProductTypes", x => new { x.ProductTypeId, x.ProductTypeParameterId });
                    table.ForeignKey(
                        name: "FK_ProductTypeParameterProductTypes_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTypeParameterProductTypes_ProductTypeParameters_ProductTypeParameterId",
                        column: x => x.ProductTypeParameterId,
                        principalTable: "ProductTypeParameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypePricings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false),
                    Multiplier = table.Column<float>(nullable: false),
                    ProductTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypePricings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTypePricings_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false),
                    CurrencyType = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    FinishedAt = table.Column<DateTime>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    AddressId = table.Column<int>(nullable: false),
                    ShipmentId = table.Column<Guid>(nullable: false),
                    PaymentId = table.Column<Guid>(nullable: false),
                    InvoiceDataId = table.Column<Guid>(nullable: true),
                    UserPersonalizedVoucherId = table.Column<Guid>(nullable: true),
                    UsedPublicVoucherId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceData",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    PaymentType = table.Column<int>(nullable: false),
                    InvoiceCustomerId = table.Column<int>(nullable: false),
                    InvoiceVendorId = table.Column<int>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true),
                    AnonymousOrderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceData_AnonymousOrders_AnonymousOrderId",
                        column: x => x.AnonymousOrderId,
                        principalTable: "AnonymousOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceData_InvoiceCustomers_InvoiceCustomerId",
                        column: x => x.InvoiceCustomerId,
                        principalTable: "InvoiceCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceData_InvoiceVendors_InvoiceVendorId",
                        column: x => x.InvoiceVendorId,
                        principalTable: "InvoiceVendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceData_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true),
                    AnonymousOrderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AnonymousOrders_AnonymousOrderId",
                        column: x => x.AnonymousOrderId,
                        principalTable: "AnonymousOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PriceInEuro = table.Column<decimal>(type: "Money", nullable: false),
                    OrderId = table.Column<Guid>(nullable: true),
                    AnonymousOrderId = table.Column<Guid>(nullable: true),
                    ProductTypeId = table.Column<int>(nullable: false),
                    ProductImageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_AnonymousOrders_AnonymousOrderId",
                        column: x => x.AnonymousOrderId,
                        principalTable: "AnonymousOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true),
                    AnonymousOrderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_AnonymousOrders_AnonymousOrderId",
                        column: x => x.AnonymousOrderId,
                        principalTable: "AnonymousOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsedPersonalizedVouchers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalizedVoucherId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedPersonalizedVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsedPersonalizedVouchers_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsedPersonalizedVouchers_PersonalizedVouchers_PersonalizedVoucherId",
                        column: x => x.PersonalizedVoucherId,
                        principalTable: "PersonalizedVouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsedPublicVouchers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicVoucherId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: true),
                    AnonymousOrderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedPublicVouchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsedPublicVouchers_AnonymousOrders_AnonymousOrderId",
                        column: x => x.AnonymousOrderId,
                        principalTable: "AnonymousOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsedPublicVouchers_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsedPublicVouchers_PublicVouchers_PublicVoucherId",
                        column: x => x.PublicVoucherId,
                        principalTable: "PublicVouchers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    ImageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTransformations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CropX1 = table.Column<float>(nullable: false),
                    CropX2 = table.Column<float>(nullable: false),
                    CropY1 = table.Column<float>(nullable: false),
                    CropY2 = table.Column<float>(nullable: false),
                    BlackAndWhite = table.Column<bool>(nullable: false),
                    Sepia = table.Column<bool>(nullable: false),
                    MirrorHorizontal = table.Column<bool>(nullable: false),
                    MirrorVertical = table.Column<bool>(nullable: false),
                    Effect1 = table.Column<byte>(nullable: false),
                    Effect2 = table.Column<byte>(nullable: false),
                    Effect3 = table.Column<bool>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTransformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductTransformations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousOrders_AnonymousAddressId",
                table: "AnonymousOrders",
                column: "AnonymousAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ArchivedPersonalData_UserId",
                table: "ArchivedPersonalData",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ImageCategories_ImageId",
                table: "ImageCategories",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceCustomers_UserId",
                table: "InvoiceCustomers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceData_AnonymousOrderId",
                table: "InvoiceData",
                column: "AnonymousOrderId",
                unique: true,
                filter: "[AnonymousOrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceData_InvoiceCustomerId",
                table: "InvoiceData",
                column: "InvoiceCustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceData_InvoiceVendorId",
                table: "InvoiceData",
                column: "InvoiceVendorId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceData_OrderId",
                table: "InvoiceData",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AnonymousOrderId",
                table: "Payments",
                column: "AnonymousOrderId",
                unique: true,
                filter: "[AnonymousOrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalizedVoucherUsers_PersonalizedVoucherId",
                table: "PersonalizedVoucherUsers",
                column: "PersonalizedVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalizedVoucherUsers_UserId",
                table: "PersonalizedVoucherUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ImageId",
                table: "ProductImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_AnonymousOrderId",
                table: "Products",
                column: "AnonymousOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTransformations_ProductId",
                table: "ProductTransformations",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypeParameterProductTypes_ProductTypeParameterId",
                table: "ProductTypeParameterProductTypes",
                column: "ProductTypeParameterId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypePricings_ProductTypeId",
                table: "ProductTypePricings",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_AnonymousOrderId",
                table: "Shipments",
                column: "AnonymousOrderId",
                unique: true,
                filter: "[AnonymousOrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_OrderId",
                table: "Shipments",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsedPersonalizedVouchers_OrderId",
                table: "UsedPersonalizedVouchers",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsedPersonalizedVouchers_PersonalizedVoucherId",
                table: "UsedPersonalizedVouchers",
                column: "PersonalizedVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_UsedPublicVouchers_AnonymousOrderId",
                table: "UsedPublicVouchers",
                column: "AnonymousOrderId",
                unique: true,
                filter: "[AnonymousOrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsedPublicVouchers_OrderId",
                table: "UsedPublicVouchers",
                column: "OrderId",
                unique: true,
                filter: "[OrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsedPublicVouchers_PublicVoucherId",
                table: "UsedPublicVouchers",
                column: "PublicVoucherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchivedPersonalData");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ImageCategories");

            migrationBuilder.DropTable(
                name: "InvoiceData");

            migrationBuilder.DropTable(
                name: "MaintenanceMessages");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PersonalizedVoucherUsers");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductTransformations");

            migrationBuilder.DropTable(
                name: "ProductTypeParameterProductTypes");

            migrationBuilder.DropTable(
                name: "ProductTypePricings");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "UsedPersonalizedVouchers");

            migrationBuilder.DropTable(
                name: "UsedPublicVouchers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "InvoiceCustomers");

            migrationBuilder.DropTable(
                name: "InvoiceVendors");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductTypeParameters");

            migrationBuilder.DropTable(
                name: "PersonalizedVouchers");

            migrationBuilder.DropTable(
                name: "PublicVouchers");

            migrationBuilder.DropTable(
                name: "AnonymousOrders");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "AnonymousAddresses");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
