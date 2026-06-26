using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Repository.Repositories.Interface;
using Service.DTO.Admin.PromoCode;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class PromoCodeService : IPromoCodeService
    {
        private readonly IPromoCodeRepository _promoRepo;
        private readonly ISendEmail _sendMail;

        public PromoCodeService(IPromoCodeRepository promoRepo, ISendEmail sendMail)
        {
            _promoRepo = promoRepo;
            _sendMail = sendMail;
        }

        public async Task CreateAsync(PromoCodeCreateDto dto)
        {
            var existingPromo = await _promoRepo.GetByCodeAsync(dto.Code);
            if (existingPromo != null)
            {
                throw new InvalidOperationException($"Promokod '{dto.Code}' artıq mövcuddur.");
            }

            var promo = new PromoCode
            {
                Code = dto.Code,
                DiscountPercent = dto.DiscountPercent,
                UsageLimit = dto.UsageLimit,
                IsActive = true
            };

            await _promoRepo.AddAsync(promo);

            var members = await _promoRepo.GetAllMembersAsync();

            string from = "aitajjf2@gmail.com";
            string displayName = "JoiFurn";
            string subject = "🎉 New Promo Code at JoiFurn!";
            string body = $"Hey friend! " +
                $"JoiFurn has a sweet deal just for you! Use promo code <b>{dto.Code}</b> and grab a cool <b>{dto.DiscountPercent}%</b> discount! Start shopping, grab your favorite furniture for less, and make your home awesome! Don’t miss out — this deal is just for you! 🚀✨";

            foreach (var user in members)
            {
                await _sendMail.SendAsync(from, displayName, user.Email, body, subject);
            }
        }


        public async Task<PromoCodeResultDto?> GetByCodeAsync(string code)
        {
            var promo = await _promoRepo.GetByCodeAsync(code);
            if (promo == null) return null;

            return new PromoCodeResultDto
            {
                Code = promo.Code,
                DiscountPercent = promo.DiscountPercent,
                IsActive = promo.IsActive,
                UsageLimit = promo.UsageLimit,
                UsageCount = promo.UsageCount
            };
        }

        public async Task<PromoCodeCheckResultDto> CheckAndApplyAsync(string code)
        {
            var promo = await _promoRepo.GetByCodeAsync(code);

            if (promo == null || !promo.IsActive)
            {
                return new PromoCodeCheckResultDto
                {
                    IsValid = false,
                    DiscountPercent = 0,
                    Message = "Promokod tapılmadı və ya deaktivdir."
                };
            }

            if (promo.UsageLimit > 0 && promo.UsageCount >= promo.UsageLimit)
            {
                return new PromoCodeCheckResultDto
                {
                    IsValid = false,
                    DiscountPercent = 0,
                    Message = "Bu promokod artıq istifadə limiti qədər istifadə olunub."
                };
            }

            return new PromoCodeCheckResultDto
            {
                IsValid = true,
                DiscountPercent = promo.DiscountPercent,
                Message = $"{promo.DiscountPercent}% endirim tətbiq olundu!"
            };
        }

        public async Task<bool> IncrementUsageCountAsync(string code)
        {
            var promo = await _promoRepo.GetByCodeAsync(code);
            if (promo == null || !promo.IsActive)
                return false;

            if (promo.UsageLimit > 0 && promo.UsageCount >= promo.UsageLimit)
                return false;

            promo.UsageCount++;
            await _promoRepo.UpdateAsync(promo);
            return true;
        }
        public async Task<List<PromoCodeDto>> GetAllAsync()
        {
            var promos = await _promoRepo.GetAllAsync();

            return promos.Select(x => new PromoCodeDto
            {
                Id = x.Id,
                Code = x.Code,
                DiscountPercent = x.DiscountPercent,
                UsageLimit = x.UsageLimit,
                IsActive = x.IsActive
            }).ToList();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var promo = await _promoRepo.GetByIdAsync(id);
            if (promo == null)
                return false;

            await _promoRepo.DeleteAsync(promo);
            return true;
        }
    }
}
