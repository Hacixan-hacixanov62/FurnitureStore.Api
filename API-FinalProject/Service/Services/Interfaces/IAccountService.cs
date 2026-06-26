using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Service.DTO.Account;
using Service.Helpers;
using Service.Helpers.Account;

namespace Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IResult> RegisterAsync(RegisterDto model);
        Task<LoginResponse> LoginAsync(LoginDto model);
        Task CreateRoleAsync();
        Task<string> VerifyEmail(string VerifyEmail, string token);
        string CreateToken(AppUser user, IList<string> roles);
        Task<AppUser> GetUserByEmailAsync(string email);
        Task<ResponseObject> ForgetPassword(string email, string requestScheme, string requestHost);
        Task<string> ResetPassword(ResetPasswordDto model);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<string> AddRoleAsync(string username, string roleName);
        Task<string> RemoveRoleAsync(string username, string roleName);
        Task<List<string>> GetAllRolesAsync();
        Task<List<string>> GetUserRolesAsync(string username);
        Task<string> SendMessageToAdminAsync(string email, string subject, string messageBody);
        Task<List<string>> GetAdminsEmailsAsync();
        Task<string> BlockUserAsync(string username, TimeSpan blockDuration);
        Task<string> UnblockUserAsync(string username);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<List<UserDto>> GetAllBlockedUsersAsync();


        Task<string> UpdateUsernameAsync(string userId, string newUsername);
        Task<string> UpdateEmailAsync(string userId, string newEmail);
    }
}
