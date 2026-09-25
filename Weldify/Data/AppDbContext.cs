using Microsoft.EntityFrameworkCore;
using Weldify.Data.Entities;

namespace Weldify.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();

        public DbSet<Job> Jobs => Set<Job>();

        public DbSet<JobItem> JobItems => Set<JobItem>();

        public DbSet<JobImage> JobImages => Set<JobImage>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasMany(x => x.Jobs)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Job>()
                .HasMany(x => x.Items)
                .WithOne(x => x.Job)
                .HasForeignKey(x => x.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Job>()
                .HasMany(x => x.Images)
                .WithOne(x => x.Job)
                .HasForeignKey(x => x.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Job>()
                .HasIndex(x => x.JobNumber)
                .IsUnique();
        }
    }
}
