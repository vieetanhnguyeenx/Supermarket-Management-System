using BusinessObject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs
{
    public class SalesTransactionDTOResponse
    {
        public int TransactionID { get; set; }
        [Required]
        public DateTime? TransactionDate { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public decimal CashReceived { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public decimal Change { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public decimal TotalPrice { get; set; }
        [ForeignKey("Employee")]
        public string EmployeeID { get; set; }
        public int? CustomerID { get; set; } = null!;
        public virtual Employee? Employee { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual ICollection<TransactionDetail>? TransactionDetails { get; set; }
    }

    public class SalesTransactionDTOPOST
    {
        [Required]
        public DateTime? TransactionDate { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public decimal CashReceived { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public decimal Change { get; set; }
        [Required]
        [Range(1, Int32.MaxValue)]
        public decimal TotalPrice { get; set; }
        [Required]
        public bool Discontinued { get; set; }
        [ForeignKey("Employee")]
        public string EmployeeID { get; set; }
        public int? CustomerID { get; set; } = null!;
        public virtual ICollection<TransactionsDetailDTOPOST>? TransactionDetails { get; set; }
    }

    public class TransactionsDetailDTOPOST
    {
        public int ProductID { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Discount { get; set; }
    }
}
