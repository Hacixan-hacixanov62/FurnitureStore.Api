using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.PromoCode;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class PromoCodeController : BaseController
    {
        private readonly IPromoCodeService _promoCodeService;

        public PromoCodeController(IPromoCodeService promoCodeService)
        {
            _promoCodeService = promoCodeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]PromoCodeCreateDto dto)
        {
            var existingPromo = await _promoCodeService.GetByCodeAsync(dto.Code);
            if (existingPromo != null)
            {
                return BadRequest(new { message = $"Promo code '{dto.Code}' already exist." });
            }

            await _promoCodeService.CreateAsync(dto);
            return Ok(new { message = "Promo code create." });
        }


        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode([FromRoute]string code)
        {
            var promo = await _promoCodeService.GetByCodeAsync(code);
            if (promo == null) return NotFound();
            return Ok(promo);
        }

        [HttpGet("check/{code}")]
        public async Task<IActionResult> CheckPromoCode(string code)
        {
            var result = await _promoCodeService.CheckAndApplyAsync(code);
            return Ok(result);
        }

        [HttpPost("use/{code}")]
        public async Task<IActionResult> UsePromoCode(string code)
        {
            var success = await _promoCodeService.IncrementUsageCountAsync(code);
            if (!success)
                return BadRequest(new { message = "Promokod istifadə oluna bilmədi." });

            return Ok(new { message = "Promokod uğurla istifadə edildi." });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var promos = await _promoCodeService.GetAllAsync();
            return Ok(promos);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int id)
        {
            var success = await _promoCodeService.DeleteAsync(id);

            if (!success)
                return NotFound(new { message = "Promokod tapılmadı və ya silinmədi." });

            return Ok(new { message = "Promokod uğurla silindi." });
        }
    }
}
