using Microsoft.AspNetCore.Identity;

namespace FrontEnd.Data
{
    public class User : IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}
