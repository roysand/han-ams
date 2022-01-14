﻿using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace Infrastructure.Persistence
{
    public partial class ApplicationDbContext : DbContext, IAppclicationDbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        // public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        //     : base(options)
        // {
        //     _configuration = configuration;
        // }        

        public virtual DbSet<RawData> RawSet { get; set; }
        public virtual DbSet<Detail> DetailSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sqlTimeout = 600;
            if (!optionsBuilder.IsConfigured)
            {
                var connectinString = _configuration.GetConnectionString("SQLAZURECONNSTR_AMS");
                optionsBuilder.UseSqlServer(connectinString,
                        opts => opts.CommandTimeout(sqlTimeout))
                    .EnableSensitiveDataLogging(true)
                    .EnableDetailedErrors(true);
            }

            optionsBuilder.LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RawData>(entity =>
            {
                entity.HasKey(key => key.MeasurementId);
                entity.ToTable("raw");
            });

            modelBuilder.Entity<Detail>(entity =>
            {
                entity.HasKey(key => key.Id);
                entity.ToTable("detail");
                entity.Property(e => e.ValueNum).HasPrecision(12,5);
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}