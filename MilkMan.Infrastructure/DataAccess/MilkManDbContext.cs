using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MilkMan.Domain.Entities;
using MilkMan.Infrastructure.DataAccess.Configurations;


namespace MilkMan.Infrastructure.Data
{
    public class MilkManDbContext : IdentityDbContext<ApplicationUser>
    {
        public MilkManDbContext(DbContextOptions<MilkManDbContext> dbContextOptions) : base(dbContextOptions)
        {
            Console.WriteLine("MilkManDbContext constructor called");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<MeasurementUnit> MeasurementUnits { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<ReturnRequest> ReturnRequests { get; set; }
        public DbSet<Refund> Refunds { get; set; }


        //for testing purpose
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Your fallback connection string here");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("OnModelCreating called");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(x => new { x.LoginProvider, x.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());

/*            modelBuilder.Entity<Customer>()
                 .HasOne(c => c.ShoppingCart)
                 .WithOne(sc => sc.Customer)
                 .HasForeignKey<ShoppingCart>(sc => sc.CustomerId)
                 .OnDelete(DeleteBehavior.Cascade);*/

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.ShoppingCart)
                .WithOne(sc => sc.Customer)
                .HasForeignKey<ShoppingCart>(sc => sc.CustomerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new MeasurementUnitConfiguration());


        }
    }
}

