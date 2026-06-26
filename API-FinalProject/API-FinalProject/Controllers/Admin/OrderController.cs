using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO.Admin.Order;
using Service.Services.Interfaces;

namespace API_FinalProject.Controllers.Admin
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;


        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto dto)
        {
            await _orderService.CreateOrderFromBasketAsync(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUser([FromRoute]string userId)
        {
            var result = await _orderService.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeOrderStatusDto dto)
        {
            try
            {
                await _orderService.ChangeStatusAsync(dto.OrderId, dto.NewStatus);
                return Ok(new { Message = "Order status changed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

    }
}
