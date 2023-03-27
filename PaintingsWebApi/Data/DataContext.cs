using Microsoft.EntityFrameworkCore;
using PaintingsWebApi.Models;
using System.Diagnostics.Metrics;

namespace PaintingsWebApi.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Gallery> Artists { get; set; }
        public DbSet<Painting> Paintings { get; set; }
        public DbSet<PaintingCategory> PaintingsCategories { get; set; }
        public DbSet<PaintingGallery> PaintingsGalleries { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaintingCategory>()
                    .HasKey(pc => new { pc.PaintingId, pc.CategoryId });
            modelBuilder.Entity<PaintingCategory>()
                    .HasOne(p => p.Painting)
                    .WithMany(pc => pc.PaintingsCategories)
                    .HasForeignKey(p => p.PaintingId);
            modelBuilder.Entity<PaintingCategory>()
                    .HasOne(p => p.Category)
                    .WithMany(pc => pc.PaintingsCategories)
                    .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<PaintingGallery>()
                    .HasKey(pc => new { pc.PaintingId, pc.GalleryId });
            modelBuilder.Entity<PaintingGallery>()
                    .HasOne(p => p.Painting)
                    .WithMany(pc => pc.PaintingsGalleries)
                    .HasForeignKey(p => p.PaintingId);
            modelBuilder.Entity<PaintingGallery>()
                    .HasOne(p => p.Gallery)
                    .WithMany(pc => pc.PaintingsGalleries)
                    .HasForeignKey(c => c.GalleryId);

        }
    }
}
