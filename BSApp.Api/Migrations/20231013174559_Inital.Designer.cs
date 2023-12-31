﻿// <auto-generated />
using BSApp.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BSApp.Api.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231013174559_Inital")]
    partial class Inital
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BSApp.Entities.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Price = 16.99m,
                            Title = "Küçük Prens"
                        },
                        new
                        {
                            Id = 2,
                            Price = 23.99m,
                            Title = "Beyaz Diş"
                        },
                        new
                        {
                            Id = 3,
                            Price = 32.99m,
                            Title = "Sait Faik Abasıyanık'tan Hikayeler"
                        },
                        new
                        {
                            Id = 4,
                            Price = 12.99m,
                            Title = "Daha Adil Bir Dünya Mümkün"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
