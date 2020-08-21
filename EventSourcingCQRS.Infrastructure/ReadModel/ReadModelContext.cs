using EventSourcingCQRS.Infrastructure.ReadModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingCQRS.Infrastructure.ReadModel
{
    public class ReadModelContext : DbContext
    {
        public ReadModelContext(DbContextOptions<ReadModelContext> options) : base(options) { }
        public ReadModelContext() { }

        public DbSet<OrderReadModel> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EventSourcingCQRS.ReadModel;Trusted_Connection=True;MultipleActiveResultSets=true");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
