using InventoryX.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace InventoryX.Infrastructure
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {            
        }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<InventoryItemType> InventoryItemTypes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<InventoryItem>().HasOne(e => e.Type).WithMany(e => e.InventoryItems).HasForeignKey(e => e.TypeId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Sale>()
            .HasOne(s => s.Seller)
            .WithMany(p => p.Sales)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Sale>()
            .HasOne(s => s.InventoryItem)
            .WithMany(p => p.Sales)
            .HasForeignKey(s => s.InventoryItemId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Purchase>().HasOne(e => e.InventoryItem).WithMany(e => e.Purchases).HasForeignKey(s => s.InventoryItemId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Purchase>().HasOne(e => e.Purchaser).WithMany(e => e.Purchases).HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Restrict);
        }
}
}
