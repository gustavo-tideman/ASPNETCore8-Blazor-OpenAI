using ASPNETCore8.Blazor.OpenAI.Components.Player;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore8.Blazor.OpenAI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Video> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Video>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Url).IsRequired();
                entity.Property(e => e.Slug).IsRequired();
                entity.Property(e => e.Transcript).IsRequired();
            });
        }
    }
}