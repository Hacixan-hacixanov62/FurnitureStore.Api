using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Repository.Repositories.Interface;
using Repository.Repositories.Interfaces;
using Service.DTO.Admin.Products;
using Service.DTOs.Admin.Products;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IColorRepository _colorRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IProductTagRepository _productTagRepository;
        private readonly IProductColorRepository _productColorRepository;
        private readonly ISendEmail _sendMail;
        private readonly IFileService _fileService;
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;


        public ProductService(IProductRepository productRepository,
                              ICategoryRepository categoryRepository,
                              IBrandRepository brandRepository,
                              IColorRepository colorRepository,
                              ITagRepository tagRepository,
                              IProductTagRepository productTagRepository,
                              IProductColorRepository productColorRepository,                             
                              IFileService fileService,
                              IMapper mapper,
                              IWebHostEnvironment env,
                              ISubscriptionService subscriptionService, ISendEmail sendEmail)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
            _colorRepository = colorRepository;
            _tagRepository = tagRepository;
            _productTagRepository = productTagRepository;
            _productColorRepository = productColorRepository;
            _subscriptionService = subscriptionService;
            _fileService = fileService;
            _mapper = mapper;
            _sendMail = sendEmail;
            _env = env;
        }

        public async Task CreateAsync(ProductCreateDto model)
        {
            if (model is null) throw new ArgumentNullException();

            var exist = await _productRepository.GetAllWithExpressionAsync(x => x.Name.ToLower() == model.Name.ToLower());
            if (exist.ToList().Count > 0) throw new ArgumentException("This product already exists");

            List<ProductImage> images = new();
            foreach (var item in model.Images)
            {
                string fileUrl = await _fileService.UploadFileAsync(item, "productimages");
                images.Add(new ProductImage { Img = fileUrl });
            }

            images.FirstOrDefault().IsMain = true;
            model.ProductImages = images;

            var data = _mapper.Map<Product>(model);

            if (await _brandRepository.GetByIdAsync(model.BrandId) is null) throw new KeyNotFoundException("Brand not found");
            if (await _categoryRepository.GetByIdAsync(model.CategoryId) is null) throw new KeyNotFoundException("Category not found");

            // Tag-ları pivot üçün hazırla (ProductTags navigation ilə bağlanır)
            if (model.TagIds != null && model.TagIds.Any(x => x > 0))
            {
                data.ProductTags = new List<ProductTag>();
                foreach (int id in model.TagIds.Distinct())
                {
                    if (await _tagRepository.GetByIdAsync(id) == null)
                        throw new KeyNotFoundException($"Tag with ID {id} not found.");

                    data.ProductTags.Add(new ProductTag { TagId = id });
                }
            }

            await _productRepository.CreateAsync(data);

            if (model.ColorIds != null && model.ColorIds.Any(x => x > 0))
            {
                foreach (var id in model.ColorIds.Distinct())
                {
                    if (await _colorRepository.GetByIdAsync(id) == null)
                        throw new KeyNotFoundException($"Color with ID {id} not found.");

                    await _productColorRepository.CreateAsync(new ProductColor
                    {
                        ColorId = id,
                        ProductId = data.Id 
                    });
                }
            }




            var templateHtml = await File.ReadAllTextAsync(
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "confirm", "mailsubscribes.html"));

            string productUrl = $"https://localhost:7169/ProductDetail/Detail/{data.Id}";
     
            var subscribers = await _subscriptionService.GetAllSubscriptionsAsync();

            foreach (var subscriber in subscribers)
            {
                var messageBody = templateHtml
                    .Replace("{{ProductName}}", data.Name)
                    .Replace("{{ProductLink}}", productUrl);

                var subject = $"🆕 New Product Added: {data.Name}";

                try
                {
                    await _sendMail.SendAsync(
                        from: "aitajjf2@gmail.com",
                        displayName: "JoinFurn",
                        to: subscriber.Email,
                        messageBody: messageBody,
                        subject: subject
                    );
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Mail göndərmə xətası: " + ex.Message);
                }
            }


        }
        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdWithIncludesAsync(id);
            if (product == null) throw new KeyNotFoundException($"Product with ID {id} not found.");
            await _productRepository.DeleteAsync(product);
        }
        public async Task<ProductDetailDto> DetailAsync(int id)
        {
            ArgumentNullException.ThrowIfNull(id);

            var product = await _productRepository.GetByIdWithIncludesAsync(id) ?? throw new KeyNotFoundException($"Data with ID{id} not found");

            return _mapper.Map<ProductDetailDto>(product);
        }
        public async Task EditAsync(int id, ProductEditDto model)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));

            var product = await _productRepository.GetByIdWithIncludesAsync(id)
                ?? throw new KeyNotFoundException("Product not found");

            if (model.UploadImages != null && model.UploadImages.Any())
            {
                foreach (var file in model.UploadImages)
                {
                    string fileUrl = await _fileService.UploadFileAsync(file, "productimages");
                    var newImage = new ProductImage
                    {
                        Img = fileUrl,
                        IsMain = false,
                        ProductId = product.Id
                    };
                    product.ProductImages.Add(newImage);
                }
            }

            if (model.MainImageId.HasValue)
            {
                foreach (var image in product.ProductImages)
                {
                    image.IsMain = image.Id == model.MainImageId.Value;
                }
            }

            foreach (var tagId in model.TagIds)
            {
                if (!product.ProductTags.Any(pt => pt.TagId == tagId))
                {
                    product.ProductTags.Add(new ProductTag { TagId = tagId });
                }
            }

            var tagsToRemove = product.ProductTags
                .Where(pt => !model.TagIds.Contains(pt.TagId))
                .ToList();
            foreach (var tag in tagsToRemove)
            {
                product.ProductTags.Remove(tag);
            }

            foreach (var colorId in model.ColorIds)
            {
                if (!product.ProductColors.Any(pc => pc.ColorId == colorId))
                {
                    product.ProductColors.Add(new ProductColor { ColorId = colorId });
                }
            }

            var colorsToRemove = product.ProductColors
                .Where(pc => !model.ColorIds.Contains(pc.ColorId))
                .ToList();
            foreach (var color in colorsToRemove)
            {
                product.ProductColors.Remove(color);
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Stock = model.Stock;
            product.BrandId = model.BrandId;
            product.CategoryId = model.CategoryId;

            await _productRepository.EditAsync(product);
        }
        public async Task<IEnumerable<ProductDto>> FilterAsync(string categoryName, string colorName, string tagName, string brandName)
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.FilterAsync(categoryName, colorName, tagName, brandName));

        }
        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<ProductDto>>(_productRepository.GetAllWithIncludes());
        }
        public async Task<ProductDto> GetByIdAsync(int id)
        {
            ArgumentNullException.ThrowIfNull(id);
            var product = await _productRepository.GetByIdWithIncludesAsync(id)
                          ?? throw new KeyNotFoundException($"Data with ID {id} not found");
            return _mapper.Map<ProductDto>(product);
        }
        public async Task<IEnumerable<ProductDto>> SearchByCategoryAndName(string categoryOrProductName)
        {
            var products = _productRepository.GetAllWithExpression(x =>
                x.Name.ToLower().Trim().Contains(categoryOrProductName.ToLower().Trim()) 
             || x.Category.Name.ToLower().Trim().Contains(categoryOrProductName.ToLower().Trim()));
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<PaginationResponse<ProductDto>> GetPaginateAsync(int page, int take)
        {
            var products = _productRepository.GetAllWithExpression(null);
            int totalItemCount = products.Count();

            var paginated = products
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();

            var mappedDatas = _mapper.Map<IEnumerable<ProductDto>>(paginated);

            int totalPage = (int)Math.Ceiling((decimal)totalItemCount / take);

            return new PaginationResponse<ProductDto>(mappedDatas, totalPage, page, totalItemCount);
        }
        public async Task<bool> DeleteImageAsync(int productId, int productImageId)
        {
            var product = await _productRepository.GetByIdWithIncludesAsync(productId);
            if (product == null) return false;

            var image = product.ProductImages.FirstOrDefault(i => i.Id == productImageId);
            if (image == null) return false;

            string imageName = Path.GetFileName(image.Img);
            string filePath = Path.Combine(AppContext.BaseDirectory, "wwwroot", "Uploads", "productimages", imageName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            if (image.IsMain && product.ProductImages.Any(x => x.Id != image.Id))
            {
                var newMain = product.ProductImages.FirstOrDefault(x => x.Id != image.Id);
                if (newMain != null)
                {
                    newMain.IsMain = true;
                }
            }

            product.ProductImages.Remove(image); // navigation property-dən silirik
            await _productRepository.EditAsync(product); // bu həm update, həm save edir

            return true;
        }
        public async Task<IEnumerable<ProductDto>> GetAllTakenAsync(int take, int? skip = null)
        {
            var products = await _productRepository.GetAllTakenAsync(take, skip);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        public async Task<int> GetProductsCountAsync()
        {
            return await _productRepository.GetProductsCount();
        }
        public async Task<IEnumerable<ProductDto>> GetSortedProductsAsync(string sortType)
        {
            var products = await _productRepository.SortedProductsAsync(sortType);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
        public async Task<IEnumerable<ProductDto>> GetComparisonProductsAsync(int categoryId, int selectedProductId, int count = 3)
        {
           var compareProducts = _productRepository.GetComparisonProducts(categoryId, selectedProductId, count);
            return  _mapper.Map<IEnumerable<ProductDto>>(compareProducts);
        }
        public async Task<ProductWithImagesDto> GetByIdWithImagesAsync(int id)
        {
            var product = await _productRepository.GetByIdImagesWithIncludesAsync(id)
                ?? throw new KeyNotFoundException($"Product with ID {id} not found");

            return _mapper.Map<ProductWithImagesDto>(product);
        }
        public async Task<List<ProductDto>> FilterByPriceAsync(ProductFilterDto filterDto)
        {
            var products = await _productRepository.FilterByPriceAsync(filterDto.MinPrice, filterDto.MaxPrice);
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>> SearchByName(string name)
        {
            var products = _productRepository.GetAllWithExpression(x => x.Name.ToLower().Trim().Contains(name.ToLower().Trim()));
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<decimal> GetMaxPriceAsync()
        {
            return await _productRepository.GetMaxPriceAsync();
        }
    }
}
