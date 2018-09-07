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
    [Migration("20180821183955_Task10-01")]
    partial class Task1001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
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

                    b.Property<byte[]>("ImageContent");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Images");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Image");
                });

            modelBuilder.Entity("CookieBook.Domain.Models.User", b =>
                {
                    b.HasBaseType("CookieBook.Domain.Models.Base.Account");

                    b.Property<decimal>("UserEmail")
                        .HasConversion(new ValueConverter<decimal, decimal>(v => default(decimal), v => default(decimal), new ConverterMappingHints(precision: 20, scale: 0)));

                    b.Property<int?>("UserImageId");

                    b.HasIndex("UserImageId")
                        .IsUnique()
                        .HasFilter("[UserImageId] IS NOT NULL");

                    b.ToTable("User");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("CookieBook.Domain.Models.UserImage", b =>
                {
                    b.HasBaseType("CookieBook.Domain.Models.Base.Image");


                    b.ToTable("UserImage");

                    b.HasDiscriminator().HasValue("UserImage");
                });

            modelBuilder.Entity("CookieBook.Domain.Models.User", b =>
                {
                    b.HasOne("CookieBook.Domain.Models.UserImage", "UserImage")
                        .WithOne("User")
                        .HasForeignKey("CookieBook.Domain.Models.User", "UserImageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
