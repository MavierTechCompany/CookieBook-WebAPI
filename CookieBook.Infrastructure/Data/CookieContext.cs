using System.Linq;
using CookieBook.Domain.Models;
using CookieBook.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace CookieBook.Infrastructure.Data
{
    public class CookieContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserImage> UserImages { get; set; }

        public CookieContext(DbContextOptions<CookieContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                    .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<UserImage>()
                .HasOne(x => x.User)
                .WithOne(c => c.UserImage)
                .HasForeignKey<User>(x => x.UserImageId);
        }
    }
}