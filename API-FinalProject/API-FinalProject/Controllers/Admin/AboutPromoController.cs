using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.AboutPromo;
using Service.DTO.Admin.Sliders;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class AboutPromoController : BaseController
    {
        private readonly IAboutPromoService _aboutPromoService;

        public AboutPromoController(IAboutPromoService aboutPromo)
        {
           _aboutPromoService = aboutPromo;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AboutPromoCreateDto request)
        {
            await _aboutPromoService.CreateAsync(request);

            return CreatedAtAction(nameof(Create), "Created success");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _aboutPromoService.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] AboutPromoEditDto request)
        {
            await _aboutPromoService.EditAsync(request, id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _aboutPromoService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _aboutPromoService.GetAllAsync());
        }
    }
}
