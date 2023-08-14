using MarketPlace.DataLayer.Entities.Account;
using MarketPlace.DataLayer.Entities.Contacts;
using MarketPlace.DataLayer.Entities.ProductOrder;
using MarketPlace.DataLayer.Entities.Products;
using MarketPlace.DataLayer.Entities.Site;
using MarketPlace.DataLayer.Entities.Store;
using MarketPlace.DataLayer.Entities.Wallet;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.DataLayer.Context
{
    public class MarketPlaceDbContext : DbContext
    {
        public MarketPlaceDbContext(DbContextOptions<MarketPlaceDbContext> options) : base(options) 
        { 

        }

        #region dbsets

        public DbSet<User> Users { get; set; }

        public DbSet<SiteSetting> SiteSettings { get; set; }

        public DbSet<ContactUs> ContactUses { get; set; }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<SiteBanner> SiteBanners { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<TicketMessage> TicketMessages { get; set; }

        public DbSet<Seller> Sellers { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductSelectedCategory> ProductSelectedCategories { get; set; }

        public DbSet<ProductColor> ProductColors { get; set; }

        public DbSet<ProductGallery> ProductGalleries { get; set; }

        public DbSet<ProductFeature> ProductFeatures { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<SellerWallet> SellerWallets { get; set; }

        public DbSet<ProductDiscount> ProductDiscounts { get; set; }

        public DbSet<ProductDiscountUse> ProductDiscountUses { get; set; }

        #endregion

        #region on model creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //modelBuilder.Entity<TicketMessage>()
            //    .HasOne(m => m.Sender)
            //    .WithMany(s => s.TicketMessages)
            //    .HasForeignKey(m => m.SenderId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<TicketMessage>()
            //    .HasOne(m => m.Ticket)
            //    .WithMany(t => t.TicketMessages)
            //    .HasForeignKey(m => m.TicketId)
            //    .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
