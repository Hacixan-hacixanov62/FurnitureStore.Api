

using Service.Services.Interfaces;
using System.Net.Mail;
using System.Net;

namespace Service.Services
{
    public class SendEmail : ISendEmail
    {
        public async Task SendAsync(string from, string displayName, string to, string messageBody, string subject)
        {
            MailMessage mailMessage = new();
            mailMessage.From = new MailAddress(from, displayName);
            mailMessage.To.Add(new MailAddress(to));
            mailMessage.Subject = subject;
            mailMessage.Body = messageBody;
            mailMessage.IsBodyHtml = true;

            using SmtpClient smtpClient = new();
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("aitajjf2@gmail.com", "jvug zabb diid mxsb");

            await smtpClient.SendMailAsync(mailMessage); 
        }

    }
}
