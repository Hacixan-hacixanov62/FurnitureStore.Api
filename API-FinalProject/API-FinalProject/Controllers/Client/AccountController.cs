using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Account;
using Service.Helpers;
using Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;

namespace API_FinalProject.Controllers.Client
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IAccountService accountService, UserManager<AppUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _accountService.RegisterAsync(request);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            var response = await _accountService.LoginAsync(request);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string verifyEmail, string token)
        {
            if (VerifyEmail == null || token == null) return BadRequest("Something went wrong");
            var response = await _accountService.VerifyEmail(verifyEmail, token);
            return Redirect("https://localhost:7169/Account/Login");
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword([FromBody] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("Email not found. Make sure you typed correctly");

            if (email == null) return BadRequest("Email not found. Make sure you typed correctly");
            var scheme = HttpContext.Request.Scheme;
            var host = HttpContext.Request.Host.Value;
            ResponseObject responseObj = await _accountService.ForgetPassword(email, scheme, host);
            if (responseObj.StatusCode == (int)StatusCodes.Status400BadRequest) return BadRequest(responseObj.ResponseMessage);
            else if (responseObj.StatusCode == (int)StatusCodes.Status404NotFound) return NotFound(responseObj.ResponseMessage);

            return Ok(responseObj);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string responseMessage = await _accountService.ResetPassword(request);
            if (responseMessage == "User not found" || responseMessage == "TokenIsNotValid") return BadRequest(responseMessage);
            return Ok(responseMessage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new { Message = "User not authenticated." });

            var result = await _accountService.UpdateEmailAsync(userId, model.NewEmail);

            if (result.Contains("successfully"))
                return Ok(new { Message = result });

            return BadRequest(new { Message = result });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUsername([FromBody] UpdateUsernameDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(new { Message = "User not authenticated." });

            var result = await _accountService.UpdateUsernameAsync(userId, model.NewUsername);

            if (result.Contains("successfully"))
                return Ok(new { Message = result });

            return BadRequest(new { Message = result });
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized("User not authenticated");

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized("User not found from claims");

            return Ok(new
            {
                Email = user.Email,
                Username = user.UserName
            });
        }

    }
}
