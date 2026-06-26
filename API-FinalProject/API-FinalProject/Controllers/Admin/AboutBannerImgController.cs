using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.AboutBannerImg;
using Service.DTO.Admin.Instagram;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class AboutBannerImgController : BaseController
    {
        private readonly IAboutBannerImgService _aboutBannerImgService;

        public AboutBannerImgController(IAboutBannerImgService aboutBannerImgService)
        {
           _aboutBannerImgService = aboutBannerImgService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _aboutBannerImgService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AboutBannerImgCreateDto request)
        {
            await _aboutBannerImgService.CreateAsync(request);

            return CreatedAtAction(nameof(Create), "Created success");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _aboutBannerImgService.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromForm] AboutBannerImgEditDto request, [FromRoute] int id)
        {
            await _aboutBannerImgService.EditAsync(request, id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _aboutBannerImgService.DeleteAsync(id);
            return Ok();
        }
    }
}
