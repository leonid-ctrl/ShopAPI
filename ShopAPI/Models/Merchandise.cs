using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopDbAccess.Models
{
    public class Merchandise
    {
        public int ID { get; set; }
        [StringLength(25)]
        [Index(IsUnique = true)]
        public string Article { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}