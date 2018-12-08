using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Breweries.Models.DB
{
    public partial class BreweryDatabaseContext : DbContext
    {
        public BreweryDatabaseContext()
        {
        }

        public BreweryDatabaseContext(DbContextOptions<BreweryDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brewery> Brewery { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brewery>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(36)
                    .ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Telephone).HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Website).HasMaxLength(50);
            });
        }
    }
}
