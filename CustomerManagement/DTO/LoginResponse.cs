namespace CustomerManagement.DTO
{
    public class LoginResponse
    {
        public string Username { get; set; } = string.Empty;

        public string? AccessToken { get; set; }

        public int ExpiresIn { get; set; }

        public string RefreshToken { get; set; }


    }
}
