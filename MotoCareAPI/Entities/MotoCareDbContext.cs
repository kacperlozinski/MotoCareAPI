using Microsoft.EntityFrameworkCore;

namespace MotoCareAPI.Entities
{
    public class MotoCareDbContext : DbContext
    {
        public MotoCareDbContext(DbContextOptions<MotoCareDbContext> options) : base(options)
        {
        }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>()
                .HasKey(c => c.CarId);

            modelBuilder.Entity<Car>()
                .Property(c => c.Make)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Car>()
                .Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Car>()
                .Property(c => c.LicensePlate)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Car>()
                .HasOne<Customer>()
                .WithMany()
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Appointment>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Appointment>()
                .Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Appointment>()
                .Property(a => a.Status)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Appointment>()
                .HasOne<Customer>()
                .WithMany()
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne<Car>()
                .WithMany()
                .HasForeignKey(a => a.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Customer>()
                .Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Customer>()
                .Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Customer>()
                .Property(c => c.PhoneNumber)
                .HasMaxLength(15);
        }
    }
}
