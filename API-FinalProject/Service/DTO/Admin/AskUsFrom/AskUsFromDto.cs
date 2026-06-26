
namespace Service.DTO.Admin.AskUsFrom
{
    public class AskUsFromDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool IsApproved { get; set; }
    }
}
