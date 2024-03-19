using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class TransactionDetail
    {
        [ForeignKey("SalesTransaction")]
        public int TransactionID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        [Required]
        public int UnitPrice { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public int Discount { get; set; }

        public virtual Product? Product { get; set; } = null!;
        public virtual SalesTransaction? Transaction { get; set; } = null!;
    }
}
