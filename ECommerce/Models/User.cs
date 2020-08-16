
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class User
    {
       
        public int User_id{ get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool isAdmin { get; set; }

        public string Token { get; set; }

        public string Name { get; set; }

    }
}
