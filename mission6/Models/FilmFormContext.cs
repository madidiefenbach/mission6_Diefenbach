using Microsoft.EntityFrameworkCore;

namespace mission6.Models
{
    public class FilmFormContext : DbContext
    {
        public FilmFormContext(DbContextOptions<FilmFormContext> options) : base(options)
        {
        }

        public DbSet<FilmForm> FilmForms { get; set; }
    }
}