using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hudek.Eshop.Web.Models.Entity
{
    [Table(nameof(ProductItem))]
    public class ProductItem
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [StringLength(64)]
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string Description { get; set; }


    }
}
