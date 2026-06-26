using API_FinalProject.Controllers.Client;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace FinalProject.Controllers.UI
{
	public class SettingController : BaseController
	{
		private readonly ISettingService _settingService;

		public SettingController(ISettingService settingService)
		{
			_settingService = settingService;
		}
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _settingService.GetAllAsync());
		}
	}
}
