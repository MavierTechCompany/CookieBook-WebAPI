using System.Linq;
using CookieBook.Domain.Models;
using CookieBook.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace CookieBook.Infrastructure.Data
{
    public class CookieContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<UserImage> UserImages { get; set; }

        public CookieContext(DbContextOptions<CookieContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                    .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<User>()
                .HasOne(x => x.UserImage)
                .WithOne(y => y.User)
                .HasForeignKey<UserImage>(y => y.UserRef)
                .IsRequired(false);
        }
    }
}