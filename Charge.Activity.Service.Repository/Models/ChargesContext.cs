﻿using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Charge.Activity.Service.Repository.Entity.Models {
    public partial class ChargesContext : DbContext {
        public ChargesContext() {
        }

        public ChargesContext(DbContextOptions<ChargesContext> options)
            : base(options) {
        }

        public virtual DbSet<Activities> Activities { get; set; }
        public virtual DbSet<Charges> Charges { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if(!optionsBuilder.IsConfigured) {
                IConfiguration configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();

                var connectionString = configuration.GetConnectionString("SqlFormacion");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Activities>(entity => {
                entity.HasKey(e => e.IdActivity);

                entity.Property(e => e.DateReception)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateSend).HasColumnType("datetime");

                entity.Property(e => e.Identifier)
                    .IsRequired()
                    .HasColumnName("identifier")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Charges>(entity => {
                entity.HasKey(e => e.IdCharge);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Concept)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateUpdate).HasColumnType("datetime");

                entity.Property(e => e.Identifier)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
