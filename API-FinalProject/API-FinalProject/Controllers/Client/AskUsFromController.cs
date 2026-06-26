using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.AskUsFrom;
using Service.DTO.UI.Subscription;
using Service.Services;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Client
{
    public class AskUsFromController : BaseController
    {
        private readonly IAskUsFromService _askUsFromService;
        public AskUsFromController(IAskUsFromService askUsFromService)
        {
            _askUsFromService = askUsFromService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AskUsFromCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(request);
            await _askUsFromService.CreateAsync(request);
            return Ok("Subscribed successfully!");
        }

       

        [HttpGet]
        public async Task<IActionResult> GetApprovedMessages()
        {
            var approvedMessages = await _askUsFromService.GetApprovedMessagesAsync();
            return Ok(approvedMessages);
        }

    }
}
