using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class StoreDBContext(DbContextOptions<StoreDBContext>options):DbContext(options)
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<DelvieryMethod> Brands { get; set; }
        public DbSet<ProductType> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
 

    }
}
