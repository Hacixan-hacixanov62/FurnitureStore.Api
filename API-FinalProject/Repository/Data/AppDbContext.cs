using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntity).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
              .Property(o => o.Status)
              .HasConversion<string>();
        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<LandingBanner> LandingBanners { get; set; }
        public DbSet<AskUsFrom> AskUsFroms { get; set; }
        public DbSet<SubscribeImg> SubscribeImgs { get; set; }
        public DbSet<Instagram> Instagrams { get; set; }      
        public DbSet<AboutBannerImg> AboutBannerImgs { get; set; } 
        public DbSet<BlogCategory> BlogCategories { get; set; } 
        public DbSet<BlogPost> BlogPosts { get; set; } 
        public DbSet<BlogPostImg> BlogPostImgs { get; set; } 
        public DbSet<Category> Categories { get; set; }       
        public DbSet<Brand> Brands { get; set; }       
        public DbSet<Tag> Tags { get; set; }       
        public DbSet<Color> Colors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; } 
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<WishlistProduct> WishlistProducts { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<AboutPromo> AboutPromos { get; set; }
        public DbSet<HelpSection> HelpSections { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BlogReview> BlogReviews { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
