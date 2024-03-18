using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class SalesTransaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }
        [Required]
        public DateTime? TransactionDate { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int CashReceived { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int Change { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public int TotalPrice { get; set; }
        [Required]
        public bool Discontinued { get; set; }
        [ForeignKey("Employee")]
        public string EmployeeID { get; set; }
        public int? CustomerID { get; set; } = null!;

        public virtual Employee? Employee { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<TransactionDetail>? TransactionDetails { get; set; }
    }
}
