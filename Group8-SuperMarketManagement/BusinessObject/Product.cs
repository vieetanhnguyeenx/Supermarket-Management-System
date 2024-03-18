using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        [Required, StringLength(40)]
        public string ProductName { get; set; } = null!;
        [ForeignKey("Supplier")]
        public int SupplierID { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        [Required, StringLength(100)]
        public string? Description { get; set; }
        [Required]
        public bool Discontinued { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int TotalQuantity { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public decimal Price { get; set; }

        public virtual Category? Category { get; set; } = null!;
        public virtual Supplier? Supplier { get; set; } = null!;
        public virtual ICollection<TransactionDetail>? TransactionDetails { get; set; }
        public virtual ICollection<Inventory>? Inventories { get; set; }
    }
}
