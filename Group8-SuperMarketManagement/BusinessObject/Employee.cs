using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    public class Employee : IdentityUser
    {
        public string? LastName { get; set; } = null!;
        public string? FirstName { get; set; } = null!;
        public DateTime? DoB { get; set; }
        public DateTime HireDate { get; set; }
        public string? Address { get; set; } = null!;
        public string? Phone { get; set; } = null!;
        public bool Discontinued { get; set; }
        public virtual ICollection<Inventory>? Inventories { get; set; }
        public virtual ICollection<SalesTransaction>? SalesTransactions { get; set; }
    }
}
