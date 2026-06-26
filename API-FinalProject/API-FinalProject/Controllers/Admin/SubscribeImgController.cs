using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.Sliders;
using Service.DTO.Admin.SubscribeImg;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class SubscribeImgController : BaseController
    {
        private readonly ISubscribeImgService _subscribeImgService;

        public SubscribeImgController(ISubscribeImgService subscribeImgService)
        {
            _subscribeImgService = subscribeImgService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {       
           return Ok(await _subscribeImgService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SubscribeImgCreateDto request)
        {
            await _subscribeImgService.CreateAsync(request);

            return CreatedAtAction(nameof(Create), "Created success");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _subscribeImgService.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] SubscribeImgEditDto request)
        {
            await _subscribeImgService.EditAsync(request, id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _subscribeImgService.DeleteAsync(id);
            return Ok();
        }
    }
}
