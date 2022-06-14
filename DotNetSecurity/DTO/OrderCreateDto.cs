using DotNetSecurity.Models;

namespace DotNetSecurity.DTO
{
    public class OrderCreateDto
    {
       
        public int OrderAmount   { get; set; }
  
        public List<OrderDetailDto> Details { get; set; }
    }
}
