﻿// <auto-generated />
using System;
using CookieBook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CookieBook.WebAPI.Migrations
{
    [DbContext(typeof(CookieContext))]
    partial class CookieContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
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

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsRestoreKeyFresh");

                    b.Property<decimal>("Login")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.Property<string>("Nick");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<string>("RestoreKey");

                    b.Property<DateTime?>("RestoreKeyUsedAt");

                    b.Property<string>("Role");

                    b.Property<byte[]>("Salt");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Accounts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Account");
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

            modelBuilder.Entity("CookieBook.Domain.Models.Rate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<int>("RecipeId");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<float>("Value");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Rates");
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

            modelBuilder.Entity("CookieBook.Domain.Models.RecipeImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("ImageContent");

                    b.Property<int?>("RecipeId");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId")
                        .IsUnique()
                        .HasFilter("[RecipeId] IS NOT NULL");

                    b.ToTable("RecipeImages");
                });

            modelBuilder.Entity("CookieBook.Domain.Models.UserImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("ImageContent");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("UserImages");
                });

            modelBuilder.Entity("CookieBook.Domain.Models.Admin", b =>
                {
                    b.HasBaseType("CookieBook.Domain.Models.Base.Account");


                    b.ToTable("Admin");

                    b.HasDiscriminator().HasValue("Admin");

                    b.HasData(
                        new { Id = 1, CreatedAt = new DateTime(2020, 12, 29, 17, 24, 44, 996, DateTimeKind.Utc), IsActive = true, IsRestoreKeyFresh = true, Login = 4116719210845653519m, Nick = "Ronald", PasswordHash = new byte[] { 239, 230, 211, 6, 157, 112, 17, 26, 43, 201, 139, 219, 136, 21, 54, 113, 157, 34, 215, 228, 250, 35, 152, 245, 103, 172, 179, 73, 149, 50, 250, 61, 185, 186, 252, 9, 254, 199, 162, 22, 156, 174, 93, 63, 22, 209, 219, 46, 195, 180, 81, 179, 5, 95, 20, 229, 76, 39, 61, 150, 61, 112, 172, 255 }, RestoreKey = "?DjC14!5G4vA", Role = "admin", Salt = new byte[] { 250, 127, 13, 50, 20, 151, 124, 65, 139, 37, 176, 20, 132, 169, 244, 110, 235, 94, 207, 250, 254, 38, 54, 255, 193, 197, 204, 52, 88, 63, 57, 218, 245, 220, 231, 218, 147, 104, 138, 243, 191, 236, 204, 53, 140, 135, 34, 202, 208, 17, 122, 174, 102, 140, 181, 88, 116, 222, 237, 5, 90, 45, 128, 184, 103, 121, 7, 216, 196, 227, 61, 119, 198, 92, 164, 251, 182, 23, 72, 49, 186, 115, 171, 235, 182, 174, 152, 66, 18, 244, 234, 183, 145, 135, 223, 74, 254, 201, 226, 199, 52, 194, 52, 8, 131, 89, 13, 49, 172, 207, 227, 236, 106, 51, 223, 105, 140, 36, 78, 183, 220, 97, 118, 220, 44, 109, 202, 231 }, UpdatedAt = new DateTime(2020, 12, 29, 17, 24, 44, 996, DateTimeKind.Utc) }
                    );
                });

            modelBuilder.Entity("CookieBook.Domain.Models.User", b =>
                {
                    b.HasBaseType("CookieBook.Domain.Models.Base.Account");

                    b.Property<decimal>("UserEmail")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.ToTable("User");

                    b.HasDiscriminator().HasValue("User");
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

            modelBuilder.Entity("CookieBook.Domain.Models.Rate", b =>
                {
                    b.HasOne("CookieBook.Domain.Models.Recipe", "Recipe")
                        .WithMany("Rates")
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
                        .HasForeignKey("CookieBook.Domain.Models.RecipeImage", "RecipeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("CookieBook.Domain.Models.UserImage", b =>
                {
                    b.HasOne("CookieBook.Domain.Models.User", "User")
                        .WithOne("UserImage")
                        .HasForeignKey("CookieBook.Domain.Models.UserImage", "UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
