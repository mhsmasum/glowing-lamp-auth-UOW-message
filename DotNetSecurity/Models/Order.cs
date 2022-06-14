using System.ComponentModel.DataAnnotations;

namespace DotNetSecurity.Models
{
    public class Order:BaseModel
    {
        
        
        public decimal OrderAmount { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual  List<OrderDetail> OrderDetails { get; set; }

    }
}
