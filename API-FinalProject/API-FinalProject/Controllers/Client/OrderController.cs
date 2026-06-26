using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.UI.Order;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Client
{
    public class OrderController : BaseController
    {

        private readonly IEmailService _emailService;

        public OrderController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendConfirmationEmail([FromBody] OrderConfirmationEmailDto model)
        {
            if (model == null || model.Products == null || !model.Products.Any())
            {
                return BadRequest("Məhsul siyahısı boşdur və ya məlumat düzgün göndərilməyib.");
            }

            var sb = new StringBuilder();

            sb.AppendLine("<div style='font-family:Arial,sans-serif; max-width:600px; margin:0 auto; padding:20px; border:1px solid #ddd; border-radius:10px;'>");

            sb.AppendLine($"<h2 style='color:#333;'>Hi {model.FullName},</h2>");
            sb.AppendLine("<p style='font-size:16px;'>The products you ordered are as follows:</p>");

            sb.AppendLine("<table style='width:100%; border-collapse:collapse; margin-top:10px;'>");
            sb.AppendLine("<thead>");
            sb.AppendLine("<tr style='background-color:#f4f4f4;'>");
            sb.AppendLine("<th style='text-align:left; padding:8px; border-bottom:1px solid #ddd;'>Product</th>");
            sb.AppendLine("<th style='text-align:center; padding:8px; border-bottom:1px solid #ddd;'>Quantity</th>");
            sb.AppendLine("<th style='text-align:right; padding:8px; border-bottom:1px solid #ddd;'>Price</th>");
            sb.AppendLine("</tr>");
            sb.AppendLine("</thead>");
            sb.AppendLine("<tbody>");

            foreach (var item in model.Products)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td style='padding:8px; border-bottom:1px solid #eee;'>{item.Name}</td>");
                sb.AppendLine($"<td style='padding:8px; text-align:center; border-bottom:1px solid #eee;'>{item.Count}</td>");
                sb.AppendLine($"<td style='padding:8px; text-align:right; border-bottom:1px solid #eee;'>{item.Price:C}</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</tbody>");
            sb.AppendLine("</table>");

            if (!string.IsNullOrEmpty(model.PromoCode))
            {
                sb.AppendLine($"<p style='margin-top:15px; font-size:15px;'>Promo code: <strong>{model.PromoCode}</strong> ({model.DiscountPercent}% discount applied.)</p>");
            }

            sb.AppendLine($"<p style='margin-top:10px; font-size:17px;'><strong>Total: {model.Total:C}</strong></p>");

            sb.AppendLine("<hr style='margin:20px 0;'>");
            sb.AppendLine("<p style='font-size:16px;'>🛋️ As the JoiFurn family, we thank you!</p>");
            sb.AppendLine("<p style='font-size:14px; color:#888;'>If you have any questions, don't hesitate to contact us.</p>");
            sb.AppendLine("</div>");

            _emailService.Send(
                to: model.ToEmail,
                subject: "JoiFurn - Order Confirmation",
                html: sb.ToString()
            );
            return Ok("Email send");
        }
    }
}
