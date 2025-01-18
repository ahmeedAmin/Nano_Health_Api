using System.ComponentModel.DataAnnotations;

namespace Nano_Health.Dtos
{
    public class RegisterDto
    {
     
        public string FName { get; set; }     
        public string LName { get; set; }
    
        public string Email { get; set; }
        public string Password { get; set; }

      
    }
}
