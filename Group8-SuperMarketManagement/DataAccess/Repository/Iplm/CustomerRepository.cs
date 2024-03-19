using AutoMapper;
using BusinessObject;
using DataAccess.DAO;
using DataAccess.DTOs;
using DataAccess.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Iplm
{
    public class CustomerRepository : ICustomerRepository
    {
        private IMapper mapper;

        public CustomerRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationMapper());
            });
            mapper = config.CreateMapper();
        }
        public CustomerDTORespone GetCustomerById(int id)
        {
            return mapper.Map<Customer,CustomerDTORespone>(CustomerDAO.GetCustomerById(id));
        }

        public List<CustomerDTORespone> GetCustomers()
        {
            return mapper.Map<List<Customer>, List<CustomerDTORespone>>(CustomerDAO.GetCustomers());
        }

        public void SaveCustomer(CustomerDTOCreate customer)
        {

            CustomerDAO.SaveCustomer(mapper.Map<CustomerDTOCreate, Customer>(customer));
        }

        public void UpdateCustomer(CustomerDTOPUT customer)
        {
            CustomerDAO.UpdateCustomer(mapper.Map<CustomerDTOPUT, Customer>(customer));
        }
    }
}
