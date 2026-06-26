using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Repository.Repositories.Interface;
using Service.DTO.UI.Wishlists;
using Service.DTOs.UI.Wishlists;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IProductService _productService;

        public WishlistService(IWishlistRepository wishlistRepository, IProductService productService)
        {
            _wishlistRepository = wishlistRepository;
            _productService = productService;
        }

        public async Task<WishlistItemDto> GetByUserIdAsync(string userId)
        {
            var wishlist = await _wishlistRepository.GetByUserIdAsync(userId);
            if (wishlist == null) return null;

            var productDtos = wishlist.WishlistProducts.Select(wp => new WishlistProductDto
            {
                ProductId = wp.ProductId,
                ProductName = wp.Product.Name,
                ProductPrice = wp.Product.Price,
                Stock = wp.Product.Stock,
                ProductImage = wp.Product.ProductImages.FirstOrDefault()?.Img
            }).ToList();

            return new WishlistItemDto
            {
                AppUserId = wishlist.AppUserId,
                Products = productDtos,
                ProductCount = productDtos.Count()
            };
        }


        public async Task<WishlistResult> AddWishlistAsync(WishlistDto wishlistDto)
        {
            Wishlist existWishlist = await _wishlistRepository.GetByUserIdAsync(wishlistDto.AppUserId);

            if (existWishlist == null)
            {
                var newWishlist = new Wishlist
                {
                    AppUserId = wishlistDto.AppUserId,
                    WishlistProducts = new List<WishlistProduct>
            {
                new WishlistProduct { ProductId = wishlistDto.ProductId }
            }
                };

                await _wishlistRepository.AddAsync(newWishlist);
                await _wishlistRepository.SaveChangesAsync();

                return new WishlistResult
                {
                    Success = true,
                    Message = "Product added wishlist."
                };
            }
            else
            {
                bool alreadyExists = existWishlist.WishlistProducts
                    .Any(wp => wp.ProductId == wishlistDto.ProductId);

                if (alreadyExists)
                {
                    return new WishlistResult
                    {
                        Success = false,
                        Message = "This product is already exist in wishlist."
                    };
                }

                existWishlist.WishlistProducts.Add(new WishlistProduct
                {
                    ProductId = wishlistDto.ProductId
                });

                await _wishlistRepository.UpdateAsync(existWishlist);
                await _wishlistRepository.SaveChangesAsync();

                return new WishlistResult
                {
                    Success = true,
                    Message = "Added succesfully !."
                };
            }
        }



        public async Task DeleteProductFromWishList(int productId, string userId)
        {
            await _wishlistRepository.RemoveProductFromWishlistAsync(userId, productId);
            await _wishlistRepository.SaveChangesAsync();
        }


        public async Task DeleteWishlistAsync(int id)
        {
            ArgumentNullException.ThrowIfNull(nameof(id));
            var wishList = await _wishlistRepository.GetByIdAsync((int)id) ?? throw new KeyNotFoundException("Data not found.");
            await _wishlistRepository.DeleteAsync(id);
        }
    }
}
