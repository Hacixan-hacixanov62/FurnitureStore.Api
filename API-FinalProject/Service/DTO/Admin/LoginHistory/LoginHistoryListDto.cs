namespace Service.DTO.Admin.LoginHistory
{
    public class LoginHistoryListDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string IPAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
