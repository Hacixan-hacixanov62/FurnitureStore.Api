using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.HelpSection;
using Service.DTO.Admin.Instagram;
using Service.Services;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class HelpSectionController : BaseController
    {
        private readonly IHelpSectionService _helpSectionService;
        public HelpSectionController(IHelpSectionService helpSectionService)
        {
            _helpSectionService = helpSectionService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HelpSectionCreateDto request)
        {
            await _helpSectionService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), "Created success");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _helpSectionService.GetByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] HelpSectionEditDto request)
        {
            await _helpSectionService.EditAsync(request, id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _helpSectionService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
          
          return Ok(await _helpSectionService.GetAllAsync());
        }
    }
}
