using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.Sliders;
using Service.Services;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class SliderController : BaseController
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SliderCreateDto request)
        {
            await _sliderService.CreateAsync(request);

            return CreatedAtAction(nameof(Create),"Created success");
        }
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _sliderService.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] SliderEditDto request)
        {
            await _sliderService.EditAsync(request,id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _sliderService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _sliderService.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetPaginateDatas([FromQuery] int page, [FromQuery] int take)
        {
            return Ok(await _sliderService.GetPaginateAsync(page, take));
        }
    }
}

