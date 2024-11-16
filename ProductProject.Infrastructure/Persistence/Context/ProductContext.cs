using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductProject.Domain.Entities;

namespace ProductProject.Infrastructure.Persistence.Context
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options) : base (options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e => {
                e.ToTable("product");

                e.HasKey(e => e.Id);

                e.Property(e => e.Id).HasColumnName("id");
                e.Property(e => e.Name).HasColumnName("name");
                e.Property(e => e.Price).HasColumnName("price");
                e.Property(e => e.Description).HasColumnName("description");
            });
        }
    }
}