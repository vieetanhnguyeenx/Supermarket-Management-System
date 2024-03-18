using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Inventory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InventoryID { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public decimal PurchasePrice { get; set; }
        [Required]
        public DateTime EntryDate { get; set; }
        [ForeignKey("Employee")]
        public string EmployeeID { get; set; } = null!;

        public virtual Product? Product { get; set; } = null!;
        public virtual Employee? Employee { get; set; } = null!;
    }
}
