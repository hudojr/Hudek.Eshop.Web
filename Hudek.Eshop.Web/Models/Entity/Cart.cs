using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Hudek.Eshop.Web.Models.Identity;

namespace Hudek.Eshop.Web.Models.Entity
{
    [Table(nameof(Cart))]
    public class Cart
    {

        [Key]
        [Required]
        public int ID { get; set; }

        [ForeignKey(nameof(User))]
        [Required]
        public int CartNumber { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        public User User { get; set; }

        public IList<CartItem> CartItems { get; set; }

    }
}
