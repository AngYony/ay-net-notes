using Microsoft.AspNetCore.Identity;

namespace IdentitySample.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NewColumn { get; set; }
    }
}