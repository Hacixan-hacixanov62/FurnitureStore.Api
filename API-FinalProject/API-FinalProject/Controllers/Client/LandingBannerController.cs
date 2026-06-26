using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Client
{
    public class LandingBannerController : BaseController
    {
        private readonly ILandingBannerService _landingBannerService;

        public LandingBannerController(ILandingBannerService landingBannerService)
        {
            _landingBannerService = landingBannerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _landingBannerService.GetAllAsync());
        }

    }
}
