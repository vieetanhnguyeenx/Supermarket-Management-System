using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Supplier
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierID { get; set; }
        [Required, StringLength(40)]
        public string CompanyName { get; set; } = null!;
        [Required, StringLength(100)]
        public string Address { get; set; } = null!;
        [Required, StringLength(11)]
        public string Phone { get; set; } = null!;
        [Required]
        public bool Discontinued { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
