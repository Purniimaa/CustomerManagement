namespace CustomerManagement.DTO
{
    public class RefreshToken
    {

        public string Username { get; set; }
        public string? refreshtoken { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool ? IsRevoked { get; set; }   

    }
}
