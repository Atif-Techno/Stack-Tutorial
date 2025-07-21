using Microsoft.EntityFrameworkCore;
using StackTutorial.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace StackTutorial.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<TopicModel> Topics { get; set; }
        public DbSet<ContentModel> Contents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<CategoryModel>(entity =>
            {
                entity.HasKey(e => e.CategoryID);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Slug).HasMaxLength(120);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.MetaDescription).HasMaxLength(160);

                entity.HasMany(e => e.Topics)
                    .WithOne(t => t.Category)  // Specify navigation property
                    .HasForeignKey(t => t.CategoryID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure TopicModel
            modelBuilder.Entity<TopicModel>(entity =>
            {
                entity.HasKey(e => e.TopicID);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Slug).HasMaxLength(120);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.MetaDescription).HasMaxLength(160);
                entity.Property(e => e.Order).HasDefaultValue(0);

                entity.HasMany(e => e.Contents)
                    .WithOne(c => c.Topic)  // Specify navigation property
                    .HasForeignKey(c => c.TopicID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure ContentModel
            modelBuilder.Entity<ContentModel>(entity =>
            {
                entity.HasKey(e => e.ContentID);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Slug).HasMaxLength(120);
                entity.Property(e => e.Body).IsRequired();
                entity.Property(e => e.MetaDescription).HasMaxLength(160);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}