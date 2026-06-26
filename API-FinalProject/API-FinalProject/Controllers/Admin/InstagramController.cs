using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.Instagram;
using Service.DTO.Admin.SubscribeImg;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class InstagramController : BaseController
    {
        private readonly IInstagramService _instagramService;

        public InstagramController(IInstagramService instagramService)
        {
          _instagramService = instagramService; 
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _instagramService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InstagramCreateDto request)
        {
            await _instagramService.CreateAsync(request);

            return CreatedAtAction(nameof(Create), "Created success");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _instagramService.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] InstagramEditDto request)
        {
            await _instagramService.EditAsync(request, id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _instagramService.DeleteAsync(id);
            return Ok();
        }
    }
}
