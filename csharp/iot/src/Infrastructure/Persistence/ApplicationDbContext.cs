using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Infrastructure.Persistence
{
    public partial class ApplicationDbContext : DbContext, IAppclicationDbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }     

        public virtual DbSet<RawData> RawSet { get; set; }
        public virtual DbSet<Detail> DetailSet { get; set; }
        public virtual DbSet<Minute> MinuteSet { get; set; }
        public virtual DbSet<Hour> HourSet { get; set; }
        
        public virtual DbSet<Price> PriceSet { get; set; }
        public virtual DbSet<PriceDetail> PriceDetailSet { get; set; }
        public virtual DbSet<ExchengeRate> ExchangeRateSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sqlTimeout = 600;
            if (!optionsBuilder.IsConfigured)
            {
                var connString = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_AMS");
                var connectionString = _configuration.GetConnectionString("SQLAZURECONNSTR_AMS");
                if (string.IsNullOrWhiteSpace(connectionString))
                    connectionString = connString;

                optionsBuilder.UseSqlServer(connectionString,
                        opts => opts.CommandTimeout(sqlTimeout))
                    .EnableSensitiveDataLogging(true)
                    .EnableDetailedErrors(true);
            }

            optionsBuilder.LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }
        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Modified = DateTime.Now;
                        break;
            
                    case EntityState.Modified:
                        entry.Entity.Modified = DateTime.Now;
                        break;
                }
            }
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
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

            modelBuilder.Entity<Minute>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable(("Minute"));
                entity.Property(e => e.ValueNum).HasPrecision(12, 5);
            });
            
            modelBuilder.Entity<Hour>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable(("Hour"));
                entity.Property(e => e.ValueNum).HasPrecision(12, 5);
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.HasKey(key => key.PriceId);
                entity.ToTable("price");
                entity.Property(p => p.Currency).HasMaxLength(5).IsUnicode(false);
                entity.Property(p => p.Unit).HasMaxLength(5).IsUnicode(false);
                entity.Property(p => p.InDomain).HasMaxLength(20).IsUnicode(false);
                entity.Property(p => p.OutDomain).HasMaxLength(20).IsUnicode(false);
                entity.Property(p => p.Average).HasPrecision(12, 5);
                entity.Property(p => p.Max).HasPrecision(12, 5);
                entity.Property(p => p.Min).HasPrecision(12, 5);

                entity.HasMany(p => p.PriceDetailList)
                    .WithOne(e => e.Price);
            });

            modelBuilder.Entity<PriceDetail>(entity =>
            {
                entity.HasKey(key => key.PriceDetailId);
                entity.ToTable("price_detail");
                entity.Property(e => e.Amount).HasPrecision(12, 5);
            });

            modelBuilder.Entity<ExchengeRate>(entity =>
            {
                entity.HasKey(key => key.ExchangeRateId);
                entity.ToTable("exchange_rate");
                entity.Property(p => p.ExchangeRate).HasPrecision(12, 5);
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
