using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DotNetSecurity.Models
{
    public class OrderDetail :BaseModel
    {

        public int OrderId { get; set; }

        [JsonIgnore]
        public virtual  Order Order { get; set; }
        public int ProductId { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }

        public int Quantity { get; set; }

    }
}
