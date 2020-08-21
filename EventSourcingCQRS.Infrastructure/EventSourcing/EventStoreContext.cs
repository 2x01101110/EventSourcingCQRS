using EventSourcingCQRS.BuildingBlocks.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.Infrastructure.EventSourcing
{
    public class EventStoreContext : DbContext
    {
        public EventStoreContext(DbContextOptions<EventStoreContext> options) : base(options) { }
        public EventStoreContext() { }

        public DbSet<EventData> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventData>().HasKey(x => x.EventId).HasName("EventId");
            modelBuilder.Entity<EventData>().Property(x => x.AggregateId).HasColumnName("AggregateId");
            modelBuilder.Entity<EventData>().Property(x => x.Timestamp).HasColumnName("Timestamp");
            modelBuilder.Entity<EventData>().Property(x => x.Type).HasColumnName("Type");
            modelBuilder.Entity<EventData>().Property(x => x.Data).HasColumnName("Data");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EventSourcingCQRS.EventStore;Trusted_Connection=True;MultipleActiveResultSets=true");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
