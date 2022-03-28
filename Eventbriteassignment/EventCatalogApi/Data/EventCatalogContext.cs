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
        //what
        public DbSet<Place> EventPlace { get; set; }
        public DbSet<Event> EventCatalogTable { get; set; }
        public DbSet<EventCategory> EventCategory { get; set; }


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

            modelBuilder.Entity<EventCategory>(c =>
            {
                c.Property(p => p.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Event>(e =>
            {
                e.Property(p => p.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                e.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                e.Property(p => p.Desciprion)
                    .HasMaxLength(300);
                e.Property(p => p.Ticketprice)
                    .IsRequired();
                // Assume event has one place but a place has many events.
                // But there might be many to many relationship.
                e.HasOne(p => p.Place)
                    .WithMany()
                    .HasForeignKey(e => e.EventPlaceId);

                e.HasOne(p => p.Category)
                    .WithMany()
                    .HasForeignKey(p => p.EventCategoryId);
            });
        }

    }
    
}
