using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICustomerRepository
    {
        List<CustomerDTORespone> GetCustomers();
        CustomerDTORespone GetCustomerById(int id);
        void SaveCustomer(CustomerDTOCreate customer);
        void UpdateCustomer(CustomerDTOPUT customer);
    }
}
