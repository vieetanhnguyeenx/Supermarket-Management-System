﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }
        [Required, StringLength(40)]
        public string LastName { get; set; } = null!;
        [Required, StringLength(40)]
        public string FirstName { get; set; } = null!;
        [Required, StringLength(40)]
        public string Address { get; set; } = null!;
        [Required, StringLength(11)]
        public string Phone { get; set; } = null!;
        [Required]
        [Range(1, Int32.MaxValue)]
        public int Point {  get; set; }
        [Required]
        public string Email {  get; set; }  
        public virtual ICollection<SalesTransaction> SalesTransactions { get; set; }
    }
}
