using System.Linq;
using CookieBook.Domain.Models;
using CookieBook.Domain.Models.Base;
using CookieBook.Domain.Models.Intermediate;
using Microsoft.EntityFrameworkCore;

namespace CookieBook.Infrastructure.Data
{
    public class CookieContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<RecipeImage> RecipeImages { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<RecipeCategory> RecipeCategories { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public CookieContext(DbContextOptions<CookieContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                    .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Recipe>()
                .HasOne(x => x.RecipeImage)
                .WithOne(y => y.Recipe)
                .IsRequired(false);

            modelBuilder.Entity<Recipe>()
                .HasMany(x => x.Components)
                .WithOne(y => y.Recipe);

            modelBuilder.Entity<User>()
                .HasOne(x => x.UserImage)
                .WithOne(y => y.User)
                .IsRequired(false);

            modelBuilder.Entity<User>()
                .HasMany(x => x.Recipes)
                .WithOne(y => y.User)
                .IsRequired();

            modelBuilder.Entity<RecipeCategory>()
                .HasKey(xy => new { xy.RecipeId, xy.CategoryId });

            modelBuilder.Entity<RecipeCategory>()
                .HasOne(xy => xy.Recipe)
                .WithMany(x => x.RecipeCategories)
                .HasForeignKey(xy => xy.RecipeId);

            modelBuilder.Entity<RecipeCategory>()
                .HasOne(xy => xy.Category)
                .WithMany(y => y.RecipeCategories)
                .HasForeignKey(xy => xy.CategoryId);
        }
    }
}