﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VolunteeringWebsite.Models
{
    public partial class VolunteeringDatabaseContext : DbContext
    {
        public VolunteeringDatabaseContext()
        {
        }

        public VolunteeringDatabaseContext(DbContextOptions<VolunteeringDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=VolunteeringDatabase;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_city_country");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Iso).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.StreetName).IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Location)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("fk_location_city");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.Activities).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("fk_project_location");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("fk_project_topic");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}