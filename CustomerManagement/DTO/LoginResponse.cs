namespace CustomerManagement.DTO
{
    public class LoginResponse
    {
        public string? Username { get; set; }
        public string? AccessToken { get; set; }
        public int? ExpireIN { get; set; }
    }
}
