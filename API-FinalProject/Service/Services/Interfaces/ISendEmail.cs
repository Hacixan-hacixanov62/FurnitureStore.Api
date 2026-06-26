
namespace Service.Services.Interfaces
{
    public interface ISendEmail
    {
        Task SendAsync(string from, string displayName, string to, string messageBody, string subject);
    }
}
