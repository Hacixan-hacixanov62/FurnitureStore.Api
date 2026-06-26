using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.LoginHistory;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class LoginHistoryController : BaseController
    {
        private readonly ILoginHistoryService _service;

        public LoginHistoryController(ILoginHistoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LoginHistoryCreateDto dto)
        {
            await _service.CreateAsync(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUser([FromQuery]string userId)
        {
            var result = await _service.GetByUserIdAsync(userId);
            return Ok(result);
        }
    }
}
