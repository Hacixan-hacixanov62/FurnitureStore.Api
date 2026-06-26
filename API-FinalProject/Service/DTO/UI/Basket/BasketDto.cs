namespace Service.DTOs.UI.Basket
{
    public class BasketDto
    {
		public string AppUserId { get; set; }
		public List<BasketProductDto> BasketProducts { get; set; }
		public int TotalProductCount { get; set; }
		public decimal TotalPrice { get; set; }

	}
}
