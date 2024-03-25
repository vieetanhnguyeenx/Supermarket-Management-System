using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject
{
    public class MyDBContext : IdentityDbContext<Employee>
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }
        public MyDBContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<SalesTransaction> SalesTransactions { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<TransactionDetail> TransactionDetails { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TransactionDetail>()
                .HasKey(r => new { r.ProductID, r.TransactionID });
            modelBuilder.Entity<Category>().HasData(
               new Category { CategoryID = 1, CategoryName = "Beverages", Description = "", Discontinued = false },
               new Category { CategoryID = 2, CategoryName = "Condiments", Description = "", Discontinued = false },
               new Category { CategoryID = 3, CategoryName = "Confections", Description = "", Discontinued = false },
               new Category { CategoryID = 4, CategoryName = "Dairy Products", Description = "", Discontinued = false },
               new Category { CategoryID = 5, CategoryName = "Grains/Cereals", Description = "", Discontinued = false },
               new Category { CategoryID = 6, CategoryName = "Meat/Poultry", Description = "", Discontinued = false },
               new Category { CategoryID = 7, CategoryName = "Produce", Description = "", Discontinued = false },
               new Category { CategoryID = 8, CategoryName = "Seafood", Description = "", Discontinued = false }
               );
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { SupplierID = 1, CompanyName = "ABC Company", Address = "123 Main St", Phone = "1234567890", Discontinued = false },
                new Supplier { SupplierID = 2, CompanyName = "XYZ Corporation", Address = "456 Elm St", Phone = "4567890123", Discontinued = false },
                new Supplier { SupplierID = 3, CompanyName = "LMN Enterprises", Address = "789 Oak St", Phone = "7890123456", Discontinued = false },
                new Supplier { SupplierID = 4, CompanyName = "PQR Inc.", Address = "321 Maple St", Phone = "3216540987", Discontinued = false },
                new Supplier { SupplierID = 5, CompanyName = "EFG Ltd.", Address = "654 Pine St", Phone = "6549873210", Discontinued = false },
                new Supplier { SupplierID = 6, CompanyName = "HIJ Co.", Address = "987 Cedar St", Phone = "9873216540", Discontinued = false },
                new Supplier { SupplierID = 7, CompanyName = "RST Industries", Address = "234 Birch St", Phone = "2345678901", Discontinued = false },
                new Supplier { SupplierID = 8, CompanyName = "UVW Group", Address = "567 Walnut St", Phone = "5678901234", Discontinued = false },
                new Supplier { SupplierID = 9, CompanyName = "MNO Limited", Address = "890 Ash St", Phone = "8901234567", Discontinued = false },
                new Supplier { SupplierID = 10, CompanyName = "QRS Enterprises", Address = "432 Spruce St", Phone = "4327651098", Discontinued = false }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductID = 1, ProductName = "Product 1", SupplierID = 1, CategoryID = 1, Description = "Description of Product 1", Discontinued = false, TotalQuantity = 100, Price = 10 },
                new Product { ProductID = 2, ProductName = "Product 2", SupplierID = 2, CategoryID = 2, Description = "Description of Product 2", Discontinued = false, TotalQuantity = 150, Price = 20 },
                new Product { ProductID = 3, ProductName = "Product 3", SupplierID = 3, CategoryID = 1, Description = "Description of Product 3", Discontinued = false, TotalQuantity = 200, Price = 15 },
                new Product { ProductID = 4, ProductName = "Product 4", SupplierID = 4, CategoryID = 3, Description = "Description of Product 4", Discontinued = false, TotalQuantity = 120, Price = 25 },
                new Product { ProductID = 5, ProductName = "Product 5", SupplierID = 5, CategoryID = 2, Description = "Description of Product 5", Discontinued = false, TotalQuantity = 180, Price = 18 },
                new Product { ProductID = 6, ProductName = "Product 6", SupplierID = 6, CategoryID = 3, Description = "Description of Product 6", Discontinued = false, TotalQuantity = 90, Price = 30 },
                new Product { ProductID = 7, ProductName = "Product 7", SupplierID = 7, CategoryID = 1, Description = "Description of Product 7", Discontinued = false, TotalQuantity = 250, Price = 22 },
                new Product { ProductID = 8, ProductName = "Product 8", SupplierID = 8, CategoryID = 2, Description = "Description of Product 8", Discontinued = false, TotalQuantity = 300, Price = 12 },
                new Product { ProductID = 9, ProductName = "Product 9", SupplierID = 9, CategoryID = 3, Description = "Description of Product 9", Discontinued = false, TotalQuantity = 150, Price = 28 },
                new Product { ProductID = 10, ProductName = "Product 10", SupplierID = 10, CategoryID = 1, Description = "Description of Product 10", Discontinued = false, TotalQuantity = 170, Price = 17 }
                );
            modelBuilder.Entity<Customer>().HasData(
                new Customer {CustomerID=1, LastName = "Doe", FirstName = "John", Address = "123 Main St", Phone = "1234567890", Point = 100,Email="" },
                new Customer {CustomerID=2, LastName = "Smith", FirstName = "Jane", Address = "456 Elm St", Phone = "4567890123", Point = 150, Email = "" },
                new Customer {CustomerID =3, LastName = "Johnson", FirstName = "Michael", Address = "789 Oak St", Phone = "7890123456", Point = 200, Email = "" },
                new Customer {CustomerID =4,LastName = "Williams", FirstName = "Emily", Address = "321 Maple St", Phone = "3216540987", Point = 120, Email = "" },
                new Customer {CustomerID =5, LastName = "Brown", FirstName = "Chris", Address = "654 Pine St", Phone = "6549873210", Point = 180, Email = "" },
                new Customer { CustomerID =6, LastName = "Jones", FirstName = "Jessica", Address = "987 Cedar St", Phone = "9873216540", Point = 90, Email = "" },
                new Customer {CustomerID = 7, LastName = "Davis", FirstName = "David", Address = "234 Birch St", Phone = "2345678901", Point = 250, Email = "" },
                new Customer {CustomerID=8, LastName = "Miller", FirstName = "Sarah", Address = "567 Walnut St", Phone = "5678901234", Point = 300, Email = "" },
                new Customer {CustomerID=9, LastName = "Wilson", FirstName = "Ryan", Address = "890 Ash St", Phone = "8901234567", Point = 150, Email = "" },
                new Customer {CustomerID =10, LastName = "Moore", FirstName = "Laura", Address = "432 Spruce St", Phone = "4327651098", Point = 170, Email = "" }
                );

        }
    }
}
