﻿// <auto-generated />
using System;
using CarShop.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarShop.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240219154835_addOrderHeadersAndOrderDetailsToDb")]
    partial class addOrderHeadersAndOrderDetailsToDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarShop.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Toyota"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Subaru"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "BMW"
                        },
                        new
                        {
                            Id = 4,
                            DisplayOrder = 4,
                            Name = "Mercedes"
                        },
                        new
                        {
                            Id = 5,
                            DisplayOrder = 5,
                            Name = "Mitsubishi"
                        },
                        new
                        {
                            Id = 6,
                            DisplayOrder = 6,
                            Name = "Nissan"
                        },
                        new
                        {
                            Id = 7,
                            DisplayOrder = 7,
                            Name = "Opel"
                        },
                        new
                        {
                            Id = 8,
                            DisplayOrder = 8,
                            Name = "Renault"
                        },
                        new
                        {
                            Id = 9,
                            DisplayOrder = 9,
                            Name = "Honda"
                        },
                        new
                        {
                            Id = 10,
                            DisplayOrder = 10,
                            Name = "Audi"
                        },
                        new
                        {
                            Id = 11,
                            DisplayOrder = 11,
                            Name = "Volvo"
                        },
                        new
                        {
                            Id = 12,
                            DisplayOrder = 12,
                            Name = "Kia"
                        },
                        new
                        {
                            Id = 13,
                            DisplayOrder = 13,
                            Name = "Hyundai"
                        });
                });

            modelBuilder.Entity("CarShop.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Седан"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Купе"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Хэтчбек"
                        },
                        new
                        {
                            Id = 4,
                            DisplayOrder = 4,
                            Name = "Лифтбек"
                        },
                        new
                        {
                            Id = 5,
                            DisplayOrder = 5,
                            Name = "Фастбек"
                        },
                        new
                        {
                            Id = 6,
                            DisplayOrder = 6,
                            Name = "Универсал"
                        },
                        new
                        {
                            Id = 7,
                            DisplayOrder = 7,
                            Name = "Кроссовер"
                        },
                        new
                        {
                            Id = 8,
                            DisplayOrder = 8,
                            Name = "Внедорожник"
                        },
                        new
                        {
                            Id = 9,
                            DisplayOrder = 9,
                            Name = "Пикап"
                        },
                        new
                        {
                            Id = 10,
                            DisplayOrder = 10,
                            Name = "Легковой фургон"
                        },
                        new
                        {
                            Id = 11,
                            DisplayOrder = 11,
                            Name = "Минивен"
                        },
                        new
                        {
                            Id = 12,
                            DisplayOrder = 12,
                            Name = "Компактвен"
                        },
                        new
                        {
                            Id = 13,
                            DisplayOrder = 13,
                            Name = "Микровен"
                        },
                        new
                        {
                            Id = 14,
                            DisplayOrder = 14,
                            Name = "Кабриолет"
                        },
                        new
                        {
                            Id = 15,
                            DisplayOrder = 15,
                            Name = "Родстер"
                        },
                        new
                        {
                            Id = 16,
                            DisplayOrder = 16,
                            Name = "Тарга"
                        },
                        new
                        {
                            Id = 17,
                            DisplayOrder = 17,
                            Name = "Ландо"
                        },
                        new
                        {
                            Id = 18,
                            DisplayOrder = 18,
                            Name = "Лимузин"
                        });
                });

            modelBuilder.Entity("CarShop.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddres")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Tech City",
                            Name = "Tech Solution",
                            PhoneNumber = "7777777777",
                            PostalCode = "123456",
                            State = "IL",
                            StreetAddres = "123 Tech st"
                        },
                        new
                        {
                            Id = 2,
                            City = "NY",
                            Name = "High tech",
                            PhoneNumber = "8888888888",
                            PostalCode = "32165",
                            State = "NY",
                            StreetAddres = "15 High st"
                        },
                        new
                        {
                            Id = 3,
                            City = "Big city",
                            Name = "New systems",
                            PhoneNumber = "3333333333",
                            PostalCode = "246515",
                            State = "Oh",
                            StreetAddres = "22 New st"
                        });
                });

            modelBuilder.Entity("CarShop.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("OrderHeaderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderHeaderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("CarShop.Models.OrderHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Carrier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nane")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("OrderTotal")
                        .HasColumnType("float");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("PaymentDueDate")
                        .HasColumnType("date");

                    b.Property<string>("PaymentIntendId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ShippingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrackingNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("OrderHeaders");
                });

            modelBuilder.Entity("CarShop.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("CarName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("basicEquipmentPrice")
                        .HasColumnType("float");

                    b.Property<double>("fullEquipmentPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandId = 3,
                            CarName = "BMW 318i",
                            CategoryId = 7,
                            Description = "Мощность двигателя: 156 л.с\nРасход топлива: 6,3 л/100 км",
                            ImageURL = "",
                            basicEquipmentPrice = 120000.0,
                            fullEquipmentPrice = 2000000.0
                        },
                        new
                        {
                            Id = 2,
                            BrandId = 3,
                            CarName = "BMW X6 M50i",
                            CategoryId = 1,
                            Description = "Мощность двигателя: 480 л.с.\nРасход топлива в л/100км (смешанный цикл): 11.5",
                            ImageURL = "",
                            basicEquipmentPrice = 1000000.0,
                            fullEquipmentPrice = 180000.0
                        },
                        new
                        {
                            Id = 3,
                            BrandId = 4,
                            CarName = "Mercedes-Benz C118/X118",
                            CategoryId = 6,
                            Description = "Мощность двигателя: 421 л.с.\nРасход топлива: 9.2 на 100 км",
                            ImageURL = "",
                            basicEquipmentPrice = 1500000.0,
                            fullEquipmentPrice = 2000000.0
                        },
                        new
                        {
                            Id = 4,
                            BrandId = 6,
                            CarName = "Nissan Tiida",
                            CategoryId = 3,
                            Description = "Мощность двигателя: 128 л.с.\nРасход топлива: 4.7-10.1 л/100 км",
                            ImageURL = "",
                            basicEquipmentPrice = 700000.0,
                            fullEquipmentPrice = 850000.0
                        },
                        new
                        {
                            Id = 5,
                            BrandId = 1,
                            CarName = "Toyota Sequoia",
                            CategoryId = 8,
                            Description = "Мощность двигателя: 437 л.с.n\nРасход топлива: 11.7-23 л/100 км",
                            ImageURL = "",
                            basicEquipmentPrice = 1200000.0,
                            fullEquipmentPrice = 1400000.0
                        },
                        new
                        {
                            Id = 6,
                            BrandId = 12,
                            CarName = "Kia XCeed",
                            CategoryId = 2,
                            Description = "Мощность двигателя: 200 л.с.n\nРасход топлива: 7.1 л/100 км",
                            ImageURL = "",
                            basicEquipmentPrice = 600000.0,
                            fullEquipmentPrice = 750000.0
                        },
                        new
                        {
                            Id = 7,
                            BrandId = 9,
                            CarName = "Honda Civic 1.0",
                            CategoryId = 1,
                            Description = "Мощность двигателя: 129 л.с.n\nРасход топлива: 5.5 л/100 км",
                            ImageURL = "",
                            basicEquipmentPrice = 600000.0,
                            fullEquipmentPrice = 750000.0
                        },
                        new
                        {
                            Id = 8,
                            BrandId = 13,
                            CarName = "HYUNDAI Tucson",
                            CategoryId = 7,
                            Description = "Мощность двигателя: 185 л.с.n\nРасход топлива: 5.4 л/100 км",
                            ImageURL = "",
                            basicEquipmentPrice = 600000.0,
                            fullEquipmentPrice = 750000.0
                        });
                });

            modelBuilder.Entity("CarShop.Models.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CountBasic")
                        .HasColumnType("int");

                    b.Property<int>("CountFull")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ProductId");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CarShop.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddres")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("CompanyId");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("CarShop.Models.OrderDetail", b =>
                {
                    b.HasOne("CarShop.Models.OrderHeader", "OrderHeader")
                        .WithMany()
                        .HasForeignKey("OrderHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarShop.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderHeader");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CarShop.Models.OrderHeader", b =>
                {
                    b.HasOne("CarShop.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("CarShop.Models.Product", b =>
                {
                    b.HasOne("CarShop.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarShop.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CarShop.Models.ShoppingCart", b =>
                {
                    b.HasOne("CarShop.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("CarShop.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarShop.Models.ApplicationUser", b =>
                {
                    b.HasOne("CarShop.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });
#pragma warning restore 612, 618
        }
    }
}
