using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CustomerDAO
    {
        public static List<Customer> GetCustomers()
        {
            var lst = new List<Customer>(); try
            {
                using (var context = new MyDBContext())
                {
                    lst = context.Customers.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return lst;
        }
        public static Customer GetCustomerById(int id)
        {
            var cust = new Customer();
            try
            {
                using (var context = new MyDBContext())
                {
                    cust = context.Customers.FirstOrDefault(x => x.CustomerID == id);
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);    
            }
            return cust;
        }
        public static void SaveCustomer(Customer customer)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Customers.Add(customer);
                    context.SaveChanges();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateCustomer(Customer customer)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Entry<Customer>(customer).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();  
                }
            }catch(Exception ex)
            {
                throw new Exception (ex.Message);
            }
        }
    }
}
