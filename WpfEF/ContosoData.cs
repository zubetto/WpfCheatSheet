namespace WpfEF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ContosoData : DbContext
    {
        public ContosoData()
            : base("name=ContosoData")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductsDetailedView> ProductsDetailedViews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Partner>()
                .Property(e => e.AnnualRevenue)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.ProductCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.StandardCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.ListPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ProductsDetailedView>()
                .Property(e => e.Cost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ProductsDetailedView>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ProductsDetailedView>()
                .Property(e => e.Profit)
                .HasPrecision(19, 4);
        }
    }
}
