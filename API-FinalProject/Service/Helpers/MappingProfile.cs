using AutoMapper;
using Domain.Entities;
using Service.DTO.Account;
using Service.DTO.Admin.AboutBannerImg;
using Service.DTO.Admin.AboutPromo;
using Service.DTO.Admin.AskUsFrom;
using Service.DTO.Admin.BlogCategory;
using Service.DTO.Admin.BlogPost;
using Service.DTO.Admin.Brand;
using Service.DTO.Admin.Category;
using Service.DTO.Admin.Color;
using Service.DTO.Admin.HelpSection;
using Service.DTO.Admin.Instagram;
using Service.DTO.Admin.LandingBanner;
using Service.DTO.Admin.LoginHistory;
using Service.DTO.Admin.Sliders;
using Service.DTO.Admin.SubscribeImg;
using Service.DTO.Admin.Tag;
using Service.DTO.Admin.Task;
using Service.DTO.UI.BlogReview;
using Service.DTO.UI.Review;
using Service.DTO.UI.Subscription;
using Service.DTOs.Admin.Products;
using Service.DTOs.Admin.Settings;
using Service.DTOs.UI.Wishlists;

namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, AppUser>();
            CreateMap<AppUser, UserDto>();


            CreateMap<Slider, SliderDto>();            
            CreateMap<SliderCreateDto, Slider>();
            CreateMap<SliderEditDto, Slider>().ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<LandingBanner, LandingBannerDto>();
            CreateMap<LandingBannerCreateDto, LandingBanner>();
            CreateMap<LandingBannerEditDto, LandingBanner>().ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<AskUsFrom, AskUsFromDto>();
            CreateMap<AskUsFromCreateDto, AskUsFrom>();

            CreateMap<SubscribeImg, SubscribeImgDto>();
            CreateMap<SubscribeImgCreateDto, SubscribeImg>();
            CreateMap<SubscribeImgEditDto, SubscribeImg>().ForMember(dest => dest.Img, opt => opt.Ignore());

            CreateMap<Instagram, InstagramDto>();
            CreateMap<InstagramCreateDto, Instagram>();
            CreateMap<InstagramEditDto, Instagram>().ForMember(dest => dest.Img, opt => opt.Ignore());

            CreateMap<AboutBannerImg, AboutBannerImgDto>();
            CreateMap<AboutBannerImgCreateDto, AboutBannerImg>();
            CreateMap<AboutBannerImgEditDto, AboutBannerImg>().ForMember(dest => dest.Img, opt => opt.Ignore());

            CreateMap<BlogCategory, BlogCategoryDto>();
            CreateMap<BlogCategoryCreateDto, BlogCategory>();
            CreateMap<BlogCategoryEditDto, BlogCategory>();

            CreateMap<BlogPost, BlogPostDto>().ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                                              .ForMember(d => d.BlogCategory, opt => opt.MapFrom(s => s.BlogCategory.Name)); 
            CreateMap<BlogPostImg, BlogPostImgDto>(); 
            CreateMap<BlogPostCreateDto, BlogPost>().ForMember(dest => dest.Images, opt => opt.Ignore());
            CreateMap<BlogPostEditDto, BlogPost>().ForMember(dest => dest.Images, opt => opt.Ignore());


            CreateMap<Category,CategoryDto>();
            CreateMap<CategoryCreateDto,Category>();
            CreateMap<CategoryEditDto,Category>().ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<Brand,BrandDto>();
            CreateMap<BrandCreateDto,Brand>();
            CreateMap<BrandEditDto,Brand>().ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<Tag,TagDto>();
            CreateMap<TagCreateDto,Tag>();
            CreateMap<TagEditDto,Tag>();
                     
            CreateMap<Color,ColorDto>();
            CreateMap<ColorCreateDto,Color>();
            CreateMap<ColorEditDto,Color>();

            CreateMap<Wishlist, WishlistDto>();
            CreateMap<WishlistDto, Wishlist>();
            CreateMap<WishlistProduct, WishlistProductDto>();
            CreateMap<WishlistProductDto, WishlistProduct>();

            CreateMap<Setting, SettingDto>();
            CreateMap<SettingCreateDto, Setting>();
            CreateMap<SettingEditDto, Setting>();

            CreateMap<Product, ProductDto>()
              .ForMember(d => d.Brand, opt => opt.MapFrom(s => s.Brand.Name))
              .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category.Name))
              .ForMember(d => d.CategoryId, opt => opt.MapFrom(s => s.Category.Id))
              .ForMember(d => d.Tags, opt => opt.MapFrom(s => s.ProductTags.Select(m => m.Tag.Name).ToList())) 
              .ForMember(d => d.Colors, opt => opt.MapFrom(s => s.ProductColors.Select(m => m.Color.Name).ToList())) 
              .ForMember(d => d.MainImage, opt => opt.MapFrom(s => s.ProductImages.FirstOrDefault(i => i.IsMain).Img))
              .ForMember(d => d.Images, opt => opt.MapFrom(s => s.ProductImages.Select(pi => pi.Img).ToList()));
            CreateMap<ProductCreateDto, Product>()
              .ForMember(dest => dest.ProductTags, opt => opt.Ignore());


            CreateMap<Product, ProductWithImagesDto>()
             .ForMember(d => d.Brand, opt => opt.MapFrom(s => s.Brand.Name))
             .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category.Name))
             .ForMember(d => d.CategoryId, opt => opt.MapFrom(s => s.Category.Id))
             .ForMember(d => d.Tags, opt => opt.MapFrom(s => s.ProductTags.Select(pt => pt.Tag.Name)))
             .ForMember(d => d.Colors, opt => opt.MapFrom(s => s.ProductColors.Select(pc => pc.Color.Name)))
             .ForMember(d => d.ProductImages, opt => opt.MapFrom(s => s.ProductImages));
            CreateMap<ProductImage, ProductImageDto>();

            CreateMap<Product, ProductDetailDto>()
              .ForMember(d => d.Brand, opt => opt.MapFrom(s => s.Brand.Name))
              .ForMember(d => d.Tags, opt => opt.MapFrom(s => s.ProductTags.Select(m => new TagDto
               {
                   Id = m.Tag.Id,
                   Name = m.Tag.Name,
               }).ToList()))
              .ForMember(d => d.Colors, opt => opt.MapFrom(s => s.ProductColors.Select(m => new ColorDto
              {
                  Id = m.Color.Id,
                  Name = m.Color.Name,
              }).ToList()));
            CreateMap<ProductEditDto, Product>();

            CreateMap<SubscriptionCreateDto, Subscription>();
            CreateMap<Subscription, SubscriptionDto>();

            CreateMap<AboutPromo, AboutPromoDto>();
            CreateMap<AboutPromoCreateDto, AboutPromo>();
            CreateMap<AboutPromoEditDto, AboutPromo>().ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<HelpSectionCreateDto, HelpSection>();
            CreateMap<HelpSectionEditDto, HelpSection>();
            CreateMap<HelpSection, HelpSectionDto>();

            CreateMap<Review, ReviewDto>()
             .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.FullName))
             .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId))
             .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src=>src.CreatedDate));
            CreateMap<ReviewCreateDto, Review>();      
            CreateMap<ReviewEditDto, Review>()
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment)); 
            
            
            CreateMap<BlogReview, BlogReviewDto>()
             .ForMember(dest => dest.BlogPostName, opt => opt.MapFrom(src => src.BlogPost.Title))
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.FullName))
             .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUserId))
             .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedDate));
            CreateMap<BlogReviewCreateDto, BlogReview>().ForMember(dest => dest.BlogPostId, opt => opt.MapFrom(src => src.PostId));      
            CreateMap<BlogReviewEditDto, BlogReview>()
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));

            CreateMap<LoginHistoryCreateDto, LoginHistory>();
            CreateMap<LoginHistory, LoginHistoryListDto>();

            CreateMap<CreateTaskDto, TaskItem>();
            CreateMap<TaskItem, TaskResponseDto>();
        }
    }
}
