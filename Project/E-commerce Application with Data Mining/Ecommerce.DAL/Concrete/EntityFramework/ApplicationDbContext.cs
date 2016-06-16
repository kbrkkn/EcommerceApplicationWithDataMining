using Ecommerce.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Ecommerce.DAL.Concrete.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {  public ApplicationDbContext()
                : base("EcommerceContext", throwIfV1Schema: false)
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<TypeOfCustomer> TypeOfCustomer { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<County> CityCounty { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Boutique> Boutique { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<SubCategory> SubCategory { get; set; }
        public DbSet<Image> Image { get; set; }

        public DbSet<Stock> Stock { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
  
            modelBuilder.Entity<Size>()
                 .HasMany(c => c.Products).WithMany(i => i.Sizes)
                 .Map(t => t.MapLeftKey("Product_Product_Id")
                     .MapRightKey("Size_Size_Id")
                     .ToTable("SizeProducts")
                     );


            modelBuilder.Entity<Color>()
               .HasMany(c => c.Products).WithMany(i => i.Colors)
               .Map(t => t.MapLeftKey("Product_Product_Id")
                   .MapRightKey("Color_Color_Id")
                   .ToTable("ColorProducts")
                   );
            base.OnModelCreating(modelBuilder);
        }
   
      
   
    }

}