using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.LandingBanner;
using Service.DTO.Admin.Sliders;
using Service.Services;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class LandingBannerController : BaseController
    {
        private readonly ILandingBannerService _landingBannerService;

        public LandingBannerController(ILandingBannerService landingBannerService)
        {
           _landingBannerService = landingBannerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _landingBannerService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] LandingBannerCreateDto request)
        {
            await _landingBannerService.CreateAsync(request);

            return CreatedAtAction(nameof(Create), "Created success");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _landingBannerService.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] LandingBannerEditDto request)
        {
            await _landingBannerService.EditAsync(request, id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _landingBannerService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginateDatas([FromQuery] int page, [FromQuery] int take)
        {
            return Ok(await _landingBannerService.GetPaginateAsync(page, take));

        }
    }
}
