namespace Service.DTO.Admin.PromoCode
{
    public class PromoCodeCreateDto
    {
        public string Code { get; set; }
        public int DiscountPercent { get; set; }
        public int UsageLimit { get; set; }
    }
}
