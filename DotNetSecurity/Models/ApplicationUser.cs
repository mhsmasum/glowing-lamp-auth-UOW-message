using Microsoft.AspNetCore.Identity;

namespace DotNetSecurity.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Msisdn { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
