using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.DTO.Account;
using Service.Helpers;
using Service.Helpers.Account;
using Service.Helpers.Enums;
using Service.Services.Interfaces;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISendEmail _sendEmail;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly IDistributedCache _distributedCache;

        private readonly SymmetricSecurityKey _securityKey;
        private readonly IConfiguration _configuration;
        public AccountService(UserManager<AppUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            SignInManager<AppUser> signInManager,
                            IHttpContextAccessor httpContextAccessor,
                            IEmailService emailService,
                            IMapper mapper,
                            IOptions<JwtSettings> jwtSettings,
                            IConfiguration configuration,
                            ISendEmail sendEmail,
                            IDistributedCache distributedCache
                            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
            _sendEmail = sendEmail;
            _distributedCache = distributedCache;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]));
            _configuration = configuration;
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task CreateRoleAsync()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }
        public async Task<LoginResponse> LoginAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.EmailOrUserName)
                       ?? await _userManager.FindByNameAsync(model.EmailOrUserName);

            if (user is null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Error = "User not found. Please register first.",
                    Token = null
                };
            }

            if (user.LockoutEnd.HasValue && user.LockoutEnd <= DateTimeOffset.UtcNow)
            {
                user.IsBlocked = false;
                user.AccessFailedCount = 0;
                await _userManager.UpdateAsync(user);
            }

            if (user.IsBlocked || await _userManager.IsLockedOutAsync(user))
            {
                return new LoginResponse
                {
                    Success = false,
                    Error = "Your account is temporarily locked. Please try again later.",
                    Token = null
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                await _userManager.AccessFailedAsync(user);

                return new LoginResponse
                {
                    Success = false,
                    Error = "Incorrect password.",
                    Token = null
                };
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return new LoginResponse
                {
                    Success = false,
                    Error = "Email not confirmed.",
                    Token = null
                };
            }

            await _userManager.ResetAccessFailedCountAsync(user);

            var userRoles = await _userManager.GetRolesAsync(user);
            string token = GenerateJwtToken(user, userRoles.ToList());

            return new LoginResponse
            {
                Success = true,
                Error = null,
                Token = token,
                UserName = user.UserName,
                UserId = user.Id.ToString(),
                Roles = userRoles.ToList()
            };
        }
        public async Task<IResult> RegisterAsync(RegisterDto model)
        {
            // Check if user already exists by email or username
            var existingUserByEmail = await _userManager.FindByEmailAsync(model.Email);
            if (existingUserByEmail != null)
            {
                return Results.BadRequest(new RegisterResponse
                {
                    Success = false,
                    Message = new List<string> { "This email is already registered." }
                });
            }

            var existingUserByUserName = await _userManager.FindByNameAsync(model.UserName);
            if (existingUserByUserName != null)
            {
                return Results.BadRequest(new RegisterResponse
                {
                    Success = false,
                    Message = new List<string> { "This username is already taken." }
                });
            }

            // Map and create user
            var user = _mapper.Map<AppUser>(model);
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return Results.BadRequest(new RegisterResponse
                {
                    Success = false,
                    Message = result.Errors.Select(e => e.Description)
                });
            }

            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

            // Email confirmation
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var request = _httpContextAccessor.HttpContext.Request;
            string url = $"https://localhost:7004/api/Account/VerifyEmail?verifyEmail={HttpUtility.UrlEncode(user.Email)}&token={HttpUtility.UrlEncode(token)}";

            var template = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "confirm", "mailconfirm.html"));
            template = template.Replace("{{link}}", url);

            _emailService.Send(user.Email, "Email confirmation", template);

            return Results.Ok(new RegisterResponse
            {
                Success = true,
                Message = new List<string> { "Registration successful. Please check your email for confirmation." }
            });
        }
        public async Task<string> VerifyEmail(string verifyEmail, string token)
        {
            var appUser = await _userManager.FindByEmailAsync(verifyEmail);
            if (appUser == null) return "User does not exist.";

            var result = await _userManager.ConfirmEmailAsync(appUser, token);
            if (!result.Succeeded) return string.Join(", ", result.Errors.Select(error => error.Description));

            await _userManager.UpdateSecurityStampAsync(appUser);
            var roles = await _userManager.GetRolesAsync(appUser);

            return CreateToken(appUser, roles);
        }
   
        private string GenerateJwtToken(AppUser user, List<string> roles)
        {
            var claims = new List<Claim>
    {
        new(ClaimTypes.NameIdentifier, user.Id),
        new(ClaimTypes.Name, user.UserName),              // 👈 düzəliş
        new(ClaimTypes.Email, user.Email),
        new(JwtRegisteredClaimNames.Sub, user.Email),
        new(JwtRegisteredClaimNames.Email, user.Email),
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            roles.ForEach(role =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpireDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateToken(AppUser user, IList<string> roles)
        {
            List<Claim> claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("FullName", user.FullName),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.Email) // ⬅️ BURANI ƏLAVƏ ETDİN!
    };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            SigningCredentials credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"]
            };

            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
            var token = securityTokenHandler.CreateToken(tokenDescriptor);
            return securityTokenHandler.WriteToken(token);
        }
        public async Task<ResponseObject> ForgetPassword(string email, string requestScheme, string requestHost)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null)
            {
                return new ResponseObject
                {
                    ResponseMessage = "User does not exist.",
                    StatusCode = (int)StatusCodes.Status400BadRequest
                };
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
            string link = $"https://localhost:7169/Account/ResetPassword?email={HttpUtility.UrlEncode(appUser.Email)}&token={HttpUtility.UrlEncode(token)}";
            var template = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "confirm", "resetpassword.html"));
            template = template.Replace("{{confirmlink}}", link);

            // Email göndəririk
            _sendEmail.SendAsync("aitajjf2@gmail.com", "JoiFurn Furniture", appUser.Email, template, "Reset Password");

            IList<string> roles = await _userManager.GetRolesAsync(appUser);

            return new ResponseObject
            {
                ResponseMessage = token,
                StatusCode = (int)StatusCodes.Status200OK
            };
        }
        public async Task<string> ResetPassword(ResetPasswordDto model)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser == null) return "User not found";

            var passwordHasher = new PasswordHasher<AppUser>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(appUser, appUser.PasswordHash, model.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                return "New password cannot be the same as the old password.";
            }

            var isSucceeded = await _userManager.VerifyUserTokenAsync(appUser, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", model.Token);
            if (!isSucceeded) return "Token is not valid.";

            IdentityResult result = await _userManager.ResetPasswordAsync(appUser, model.Token, model.Password);
            if (!result.Succeeded) return string.Join(", ", result.Errors.Select(error => error.Description));

            await _userManager.UpdateSecurityStampAsync(appUser);
            await _distributedCache.RemoveAsync(appUser.Email);

            return "Password successfully reset";
        }
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            foreach (var userDto in userDtos)
            {
                var roles = await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(userDto.UserName));
                userDto.Roles = roles.ToList();
            }

            return userDtos;
        }
        public async Task<string> AddRoleAsync(string username, string roleName)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return "User does not exist.";

            if (!await _roleManager.RoleExistsAsync(roleName)) return "Role does not exist.";

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Any(r => r.Equals(roleName, StringComparison.OrdinalIgnoreCase)))
            {
                return $"User already has the role '{roleName}'.";
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                return $"Failed to add role: {string.Join(", ", result.Errors.Select(error => error.Description))}";
            }

            return $"Role '{roleName}' added to user '{username}' successfully.";
        }
        public async Task<string> RemoveRoleAsync(string username, string roleName)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return "User does not exist.";
            }

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                return "Role does not exist.";
            }

            if (roleName.Equals(Roles.SuperAdmin.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                return "The 'SuperAdmin' role cannot be removed.";
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Count == 1 && userRoles[0].Equals("Member", StringComparison.OrdinalIgnoreCase))
            {
                return "The 'Member' role cannot be removed as it is the user's only role.";
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!result.Succeeded)
            {
                return $"Failed to remove role: {string.Join(", ", result.Errors.Select(error => error.Description))}";
            }

            return $"Role '{roleName}' removed from user '{username}' successfully.";
        }
        public async Task<List<string>> GetAllRolesAsync()
        {
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return await Task.FromResult(roles);
        }
        public async Task<List<string>> GetUserRolesAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return new List<string>();

            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
        public async Task<string> SendMessageToAdminAsync(string email, string subject, string messageBody)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.IsInRoleAsync(user, "Admin"))
                return "Admin user not found.";

            await _sendEmail.SendAsync("aitajjf2@gmail.com", "JoiFurn System", email, messageBody, subject);
            return "Message sent to admin successfully.";
        }
        public async Task<List<string>> GetAdminsEmailsAsync()
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            return admins.Select(u => u.Email).ToList();
        }
        public async Task<string> BlockUserAsync(string username, TimeSpan blockDuration)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return "User not found.";

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("SuperAdmin"))
                return "SuperAdmin users cannot be blocked.";

            user.IsBlocked = true;
            user.LockoutEnabled = true;
            user.LockoutEnd = DateTimeOffset.UtcNow.Add(blockDuration);

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return $"User {username} successfully blocked.";

            return $"Failed to block user: {string.Join(", ", result.Errors.Select(e => e.Description))}";
        }
        public async Task<string> UnblockUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return "User not found.";

            user.IsBlocked = false;
            user.LockoutEnabled = false;
            user.LockoutEnd = null;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return $"User {user.UserName} successfully unblocked.";

            return $"Failed to unblock user: {string.Join(", ", result.Errors.Select(e => e.Description))}";
        }
        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
        public async Task<List<UserDto>> GetAllBlockedUsersAsync()
        {
            var blockedUsers = await _userManager.Users
                .Where(u => u.IsBlocked && u.LockoutEnd.HasValue && u.LockoutEnd > DateTimeOffset.UtcNow)
                .ToListAsync();

            var result = new List<UserDto>();

            foreach (var user in blockedUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FullName = user.FullName,
                    Roles = roles.ToList()
                });
            }

            return result;
        }
        public async Task<string> UpdateEmailAsync(string userId, string newEmail)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return "User not found";

            if (string.IsNullOrWhiteSpace(newEmail)) return "Email cannot be empty";

            if (user.Email == newEmail)
                return "New email cannot be the same as the current one.";

            var emailExists = await _userManager.FindByEmailAsync(newEmail);
            if (emailExists != null)
                return "This email is already taken.";

            user.Email = newEmail;
            user.NormalizedEmail = newEmail.ToUpper();

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return string.Join(", ", result.Errors.Select(e => e.Description));

            return "Email successfully updated.";
        }
        public async Task<string> UpdateUsernameAsync(string userId, string newUsername)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return "User not found";

            if (string.IsNullOrWhiteSpace(newUsername)) return "Username cannot be empty";

            if (user.UserName == newUsername)
                return "New username cannot be the same as the current one.";

            var usernameExists = await _userManager.FindByNameAsync(newUsername);
            if (usernameExists != null)
                return "This username is already taken.";

            user.UserName = newUsername;
            user.NormalizedUserName = newUsername.ToUpper();

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return string.Join(", ", result.Errors.Select(e => e.Description));

            return "Username successfully updated.";
        }
    }
}
