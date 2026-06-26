namespace Service.Helpers.Account
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}
