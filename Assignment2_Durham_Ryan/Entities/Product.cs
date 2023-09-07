using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2_Durham_Ryan.Entities
{
    [Table("Products")]
    public class Product
    {
        public int ProductID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Cost { get; set; }
    }
}
