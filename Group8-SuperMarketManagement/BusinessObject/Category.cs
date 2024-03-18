using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }
        [Required, StringLength(40)]
        public string CategoryName { get; set; } = null!;
        [Required, StringLength(100)]
        public string? Description { get; set; }
        [Required]
        public bool Discontinued { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
