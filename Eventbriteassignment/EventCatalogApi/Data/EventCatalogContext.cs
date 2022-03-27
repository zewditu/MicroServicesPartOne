using EventCatalogApi.Domain;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EventCatalogApi.Data
{
    public class EventCatalogContext : DbContext
    {
        public EventCatalogContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Place> EventPlace { get; set; }
        public DbSet<Event> EventCatalogTable { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Place>(e =>
            {
                e.Property(p => p.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                e.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                e.Property(p => p.Aderess)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Event>(e =>
            {
                e.Property(e => e.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                e.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                e.Property(e => e.Desciprion)
                    .HasMaxLength(300);
                e.Property(e => e.Ticketprice)
                    .IsRequired();
                // Assume event has one place but a place has many events.
                // But there might be many to many relationship.
                e.HasOne(e => e.place)
                    .WithMany()
                    .HasForeignKey(e => e.EventPlaceId);
            });
        }

    }
}
