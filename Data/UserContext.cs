using Domain.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class UserContext : DbContext
    {
        public DbSet<Merchendiser> Merchendisers { get; set; }

        public DbSet<Coordinator> Coordinators { get; set; }

        public DbSet<Shop> Shops { get; set; }

        public DbSet<Workshift> Workshifts { get; set; }

        public UserContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workshift>()
                .HasOne(a => a.Merchendiser).WithOne(a => a.CurrentShift)
                .HasForeignKey<Merchendiser>(a => a.CurrentShiftId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Merchendiser>()
                .HasOne(a => a.CurrentShift).WithOne(a => a.Merchendiser)
                .HasForeignKey<Workshift>(a => a.MerchendiserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
