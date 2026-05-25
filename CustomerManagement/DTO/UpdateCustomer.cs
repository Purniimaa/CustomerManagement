using System.Text.Json.Serialization;

namespace CustomerManagement.DTO
{
    public class UpdateCustomer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public IFormFile? Image { get; set; }

        [JsonIgnore]
        public string ImagePath { get; set; } = string.Empty;


    }
}
