using Microsoft.EntityFrameworkCore;
using mission6_Diefenbach.Models;

namespace mission6.Models
{
    public class FilmFormContext : DbContext
    {
        public FilmFormContext(DbContextOptions<FilmFormContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly map Movie to Movies table and columns (matches SQLite schema)
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movies");
                entity.HasKey(e => e.MovieID);
                entity.Property(e => e.MovieID).HasColumnName("MovieId");
                entity.Property(e => e.CategoryID).HasColumnName("CategoryId");
                entity.Property(e => e.CopiedToPlex).HasColumnName("CopiedToPlex");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(e => e.CategoryID);
                entity.Property(e => e.CategoryID).HasColumnName("CategoryId");
            });
        }
    }
}