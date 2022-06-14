using System.ComponentModel.DataAnnotations;

namespace DotNetSecurity.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
       
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
