using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopDbAccess.Models
{
    public class Order
    {
        public int ID { get; set; }
        [Required]
        public virtual ICollection<Merchandise> OrdersMerchandise { get; set; }
        public double OrderTotal { get; set; }
    }
}