﻿// <auto-generated />
using System;
using CookieBook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CookieBook.WebAPI.Migrations
{
    [DbContext(typeof(CookieContext))]
    [Migration("20190923145931_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CookieBook.Domain.Models.Base.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<decimal>("Login")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.Property<string>("Nick");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<string>("RestoreKey");

                    b.Property<string>("Role");

                    b.Property<byte[]>("Salt");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Accounts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Account");
                });

            modelBuilder.Entity("CookieBook.Domain.Models.Base.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("ImageContent");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Images");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Image");
                });

            modelBuilder.Entity("CookieBook.Domain.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CookieBook.Domain.Models.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Name");

                    b.Property<int?>("RecipeId");

                    b.Property<int>("Unit");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Components");
                });

            modelBuilder.Entity("CookieBook.Domain.Models.Intermediate.RecipeCategory", b =>
                {
                    b.Property<int>("RecipeId");

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("RecipeId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("RecipeCategories");
                });

            modelBuilder.Entity("CookieBook.Domain.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<bool>("IsGlutenFree");

                    b.Property<bool>("IsLactoseFree");

                    b.Property<bool>("IsVegan");

                    b.Property<bool>("IsVegetarian");

                    b.Property<string>("Name");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int?>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("CookieBook.Domain.Models.User", b =>
                {
                    b.HasBaseType("CookieBook.Domain.Models.Base.Account");

                    b.Property<decimal>("UserEmail")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.ToTable("User");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("CookieBook.Domain.Models.RecipeImage", b =>
                {
                    b.HasBaseType("CookieBook.Domain.Models.Base.Image");

                    b.Property<int?>("RecipeRef");

                    b.HasIndex("RecipeRef")
                        .IsUnique()
                        .HasFilter("[RecipeRef] IS NOT NULL");

                    b.ToTable("RecipeImage");

                    b.HasDiscriminator().HasValue("RecipeImage");
                });

            modelBuilder.Entity("CookieBook.Domain.Models.UserImage", b =>
                {
                    b.HasBaseType("CookieBook.Domain.Models.Base.Image");

                    b.Property<int?>("UserRef");

                    b.HasIndex("UserRef")
                        .IsUnique()
                        .HasFilter("[UserRef] IS NOT NULL");

                    b.ToTable("UserImage");

                    b.HasDiscriminator().HasValue("UserImage");
                });

            modelBuilder.Entity("CookieBook.Domain.Models.Component", b =>
                {
                    b.HasOne("CookieBook.Domain.Models.Recipe", "Recipe")
                        .WithMany("Components")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CookieBook.Domain.Models.Intermediate.RecipeCategory", b =>
                {
                    b.HasOne("CookieBook.Domain.Models.Category", "Category")
                        .WithMany("RecipeCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CookieBook.Domain.Models.Recipe", "Recipe")
                        .WithMany("RecipeCategories")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CookieBook.Domain.Models.Recipe", b =>
                {
                    b.HasOne("CookieBook.Domain.Models.User", "User")
                        .WithMany("Recipes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CookieBook.Domain.Models.RecipeImage", b =>
                {
                    b.HasOne("CookieBook.Domain.Models.Recipe", "Recipe")
                        .WithOne("RecipeImage")
                        .HasForeignKey("CookieBook.Domain.Models.RecipeImage", "RecipeRef")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CookieBook.Domain.Models.UserImage", b =>
                {
                    b.HasOne("CookieBook.Domain.Models.User", "User")
                        .WithOne("UserImage")
                        .HasForeignKey("CookieBook.Domain.Models.UserImage", "UserRef")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
