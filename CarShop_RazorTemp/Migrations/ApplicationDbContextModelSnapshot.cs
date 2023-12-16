﻿// <auto-generated />
using CarShop_RazorTemp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarShop_RazorTemp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CarShop_RazorTemp.Models.CarShop.Models.Category", b =>
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
#pragma warning restore 612, 618
        }
    }
}
