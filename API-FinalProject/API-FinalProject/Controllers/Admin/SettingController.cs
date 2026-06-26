using API_FinalProject.Controllers.Admin;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Settings;
using Service.Services;
using Service.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace FinalProject.Controllers.Admin
{
    public class SettingController : BaseController
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] SettingCreateDto request)
        //{
        //    await _settingService.CreateAsync(request);
        //    return CreatedAtAction(nameof(Create), new { response = "Data successfully created" });
        //}

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _settingService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _settingService.GetByIdAsync(id));
        }

        //[HttpDelete]
        //public async Task<IActionResult> Delete([FromQuery][Required] int id)
        //{
        //    await _settingService.DeleteAsync(id);
        //    return Ok();
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] SettingEditDto request)
        {
            await _settingService.EditAsync(id, request);
            return Ok();
        }
    }
}
