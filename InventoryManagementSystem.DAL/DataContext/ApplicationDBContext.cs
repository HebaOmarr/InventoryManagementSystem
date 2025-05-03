using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystem.Entities.Model;
using Microsoft.Extensions.Hosting;

namespace InventoryManagementSystem.DAL.DataContext
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {

        DbSet<Product> Products { get; set; }
        DbSet<Warehouse> Warehouses { get; set; }
        DbSet<WarehouseProducts> WarehouseProducts { get; set; }
     
        DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        DbSet<Category> Categories { get; set; }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                 new ApplicationRole { Id = 2, Name = "User", NormalizedName = "USER" });


            modelBuilder.Entity<WarehouseProducts>()
                .HasKey(w => new { w.ProductId, w.WarehouseId });

            modelBuilder.Entity<Product>()
    .Property(p => p.Price)
    .HasColumnType("decimal(10, 2)");


            modelBuilder.Entity<InventoryTransaction>()
         .HasOne(t => t.FromWarehouse)
         .WithMany()
         .HasForeignKey(t => t.FromWarehouseId)
         .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InventoryTransaction>()
                .HasOne(t => t.ToWarehouse)
                .WithMany()
                .HasForeignKey(t => t.ToWarehouseId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}

