using Microsoft.AspNetCore.Identity;

namespace Nano_Health.Models
{
    public class User : IdentityUser
    {
        public string FName { get; set; }
        public string LName { get; set; }
        
    }
}
