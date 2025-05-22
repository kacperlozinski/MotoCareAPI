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
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Car
            modelBuilder.Entity<Car>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Car>()
                .Property(c => c.Brand)
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

            // Appointment
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

            // Customer
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);

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

            modelBuilder.Entity<Customer>()
                .Property(c => c.Note)
                .HasMaxLength(200);

            // Service
            modelBuilder.Entity<Service>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Service>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Service>()
                .Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Service>()
                .Property(s => s.Price)
                .IsRequired()
                .HasColumnType("decimal(8,2)");

            modelBuilder.Entity<Service>()
                .Property(s => s.LastPriceUpdate)
                .IsRequired();

            modelBuilder.Entity<Service>()
                .HasOne(s => s.Category)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.ServiceCategoryId)
                .OnDelete(DeleteBehavior.Restrict); // <== kluczowa zmiana

            // ServiceCategory
            modelBuilder.Entity<ServiceCategory>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<ServiceCategory>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
