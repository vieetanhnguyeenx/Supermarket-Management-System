using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject
{
    public class SalesTransaction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }
        [Required]
        public DateTime? TransactionDate { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal CashReceived { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Change { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalPrice { get; set; }
        [Required]
        public bool Discontinued { get; set; }
        [ForeignKey("Employee")]
        public string EmployeeID { get; set; } = "";
        public int? CustomerID { get; set; } = null!;

        public virtual Employee? Employee { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<TransactionDetail>? TransactionDetails { get; set; }
    }
}
