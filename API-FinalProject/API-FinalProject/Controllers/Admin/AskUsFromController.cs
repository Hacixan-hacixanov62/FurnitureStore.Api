using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.AskUsFrom;
using Service.DTO.Admin.Sliders;
using Service.Services;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class AskUsFromController : BaseController
    {
        private readonly IAskUsFromService _askUsFromService;
        public AskUsFromController(IAskUsFromService askUsFromService)
        {
            _askUsFromService = askUsFromService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _askUsFromService.GetAllAsync());
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int id)
        { 
            await _askUsFromService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> ApproveMessage([FromQuery] int id)
        {
            var message = await _askUsFromService.GetByIdAsync(id);
            if (message == null) return NotFound($"Message with ID {id} not found.");

            await _askUsFromService.ApproveMessageAsync(id);
            return Ok("Message approved successfully!");
        }
    }
}
