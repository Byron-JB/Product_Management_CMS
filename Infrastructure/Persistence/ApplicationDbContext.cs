using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    // ApplicationDbContext derives from EF Core DbContext and exposes DbSets for domain entities.
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.iId);
                b.Property(u => u.strEmail).IsRequired().HasMaxLength(256);
                b.HasIndex(u => u.strEmail).IsUnique();
                b.Property(u => u.strFirstName).HasMaxLength(100);
                b.Property(u => u.strLastName).HasMaxLength(100);

                // Audit relations
                b.HasOne(u => u.AddedByUser).WithMany().HasForeignKey(u => u.iAddedBy).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(u => u.ModifiedByUser).WithMany().HasForeignKey(u => u.iModifiedBy).OnDelete(DeleteBehavior.Restrict);
            });

            // ProductType
            modelBuilder.Entity<ProductType>(b =>
            {
                b.HasKey(pt => pt.iId);
                b.Property(pt => pt.strType).IsRequired().HasMaxLength(100);
                b.Property(pt => pt.strDescription).HasMaxLength(1000);

                b.HasOne(pt => pt.AddedByUser).WithMany().HasForeignKey(pt => pt.iAddedBy).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(pt => pt.ModifiedByUser).WithMany().HasForeignKey(pt => pt.iModifiedBy).OnDelete(DeleteBehavior.Restrict);
            });

            // ProductSize
            modelBuilder.Entity<ProductSize>(b =>
            {
                b.HasKey(ps => ps.iId);
                b.Property(ps => ps.strSize).IsRequired().HasMaxLength(50);

                b.HasOne(ps => ps.AddedByUser).WithMany().HasForeignKey(ps => ps.iAddedBy).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(ps => ps.ModifiedByUser).WithMany().HasForeignKey(ps => ps.iModifiedBy).OnDelete(DeleteBehavior.Restrict);
            });

            // Product
            modelBuilder.Entity<Product>(b =>
            {
                b.HasKey(p => p.iId);
                b.Property(p => p.strName).IsRequired().HasMaxLength(200);
                b.Property(p => p.strDescription).HasMaxLength(1000);
                b.Property(p => p.strColor).HasMaxLength(50);
                b.Property(p => p.dblprice).HasColumnType("decimal(18,2)");

                b.HasOne(p => p.ProductType).WithMany().HasForeignKey(p => p.ProductTypeId).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(p => p.ProductSize).WithMany().HasForeignKey(p => p.ProductSizeId).OnDelete(DeleteBehavior.Restrict);

                b.HasOne(p => p.AddedByUser).WithMany().HasForeignKey(p => p.iAddedBy).OnDelete(DeleteBehavior.Restrict);
                b.HasOne(p => p.ModifiedByUser).WithMany().HasForeignKey(p => p.iModifiedBy).OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void UpdateAuditFields()
        {
            var utcNow = DateTime.UtcNow;

            var entries = ChangeTracker.Entries<AuditableEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.dtCreatedDate = utcNow;
                    entry.Entity.dtModifiedDate = utcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    // Do not overwrite created date on update
                    entry.Entity.dtModifiedDate = utcNow;
                }
            }
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAuditFields();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
