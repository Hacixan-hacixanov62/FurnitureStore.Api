using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using Service.Services.Interfaces;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<ILandingBannerService, LandingBannerService>();
            services.AddScoped<IAskUsFromService, AskUsFromService>();
            services.AddScoped<ISubscribeImgService, SubscribeImgService>();
            services.AddScoped<IInstagramService, InstagramService>();
            services.AddScoped<IAboutBannerImgService, AboutBannerImgService>();                   
            services.AddScoped<IBlogCategoryService, BlogCategoryService>();                   
            services.AddScoped<IBlogPostService, BlogPostService>();                   
            services.AddScoped<ICategoryService, CategoryService>();                   
            services.AddScoped<IBrandService, BrandService>();                   
            services.AddScoped<ITagService, TagService>();                   
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISendEmail, SendEmail>();
            services.AddScoped<IWishlistService, WishlistService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IAboutPromoService, AboutPromoService>();
            services.AddScoped<IHelpSectionService, HelpSectionService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IBlogReviewService, BlogReviewService>();
            services.AddScoped<ILoginHistoryService, LoginHistoryService>();
            services.AddScoped<IPromoCodeService, PromoCodeService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddDistributedMemoryCache();
            return services;
        }
    }
}