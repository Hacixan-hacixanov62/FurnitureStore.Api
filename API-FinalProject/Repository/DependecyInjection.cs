using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Repository.Repositories.Interface;
using Repository.Repositories.Interfaces;

namespace Repository
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddRepositoryLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ISliderRepository, SliderRepository>();
            services.AddScoped<ILandingBannerRepository, LandingBannerRepository>();
            services.AddScoped<IAskUsFromRepository, AskUsFromRepository>();
            services.AddScoped<ISubscribeImgRepository, SubscribeImgRepository>();
            services.AddScoped<IInstagramRepository, InstagramRepository>();
            services.AddScoped<IAboutBannerImgRepository, AboutBannerImgRepository>();
            services.AddScoped<IBlogCategoryRepository, BlogCategoryRepository>();
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<IWishlistRepository, WishlistRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IProductColorRepository, ProductColorRepository>();
            services.AddScoped<IProductTagRepository, ProductTagRepository>();
            services.AddScoped<IProductRepository, ProductRepository >();
            services.AddScoped<IBasketRepository, BasketRepository >();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IAboutPromoRepository, AboutPromoRepository>();
            services.AddScoped<IHelpSectionRepository, HelpSectionRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IBlogReviewRepository, BlogReviewRepository>();
            services.AddScoped<ILoginHistoryRepository, LoginHistoryRepository>();
            services.AddScoped<IPromoCodeRepository, PromoCodeRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
