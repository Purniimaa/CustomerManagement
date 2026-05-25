using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerManagement.DTO
{
    public class CustomerDTO
    {
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal Balance { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImagePath { get; set; }

    }

 


}
