using DearlerPlatform.Domain;
using DearlerPlatform.Domain.Userinfo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DearlerPlatform.Core.Core
{
    public class DearlerPlarformDbContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerPwd> CustomerPwds { get; set; }

        public DbSet<CustomerInvoice> CustomerInvoices { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
        public DbSet<ProductSale> ProductSales { get; set; }
        public DbSet<ProductSaleAreaDiff> ProductSaleAreasDiffs { get; set; }

        public DearlerPlarformDbContext()
        {

        }

        public DearlerPlarformDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                //为表添加描述
                entity.HasComment("用户表");
                //设置属性
                entity.Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("用户名");

            });
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=192.168.52.131;database=Wy;User ID=sa;Password=abcd-1234");
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
