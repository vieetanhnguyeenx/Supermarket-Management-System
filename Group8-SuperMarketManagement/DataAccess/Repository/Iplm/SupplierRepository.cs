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
    internal class SupplierRepository : ISupplierRepository
    {
        private IMapper mapper;

        public SupplierRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationMapper());
            });
            mapper = config.CreateMapper();
        }
        public void DeleteSupplier(int id)
        {
            SupplierDAO.DeleteSupplier(id);
        }

        public SupplierDTORespone GetSupplierById(int id)
        {
            return mapper.Map<Supplier, SupplierDTORespone>(SupplierDAO.GetSupplierById(id));
        }

        public List<SupplierDTORespone> GetSuppliers()
        {
            return mapper.Map<List<Supplier>, List<SupplierDTORespone>>(SupplierDAO.GetSuppliers());
        }

        public void SaveSupplier(SupplierDTOCreate supplier)
        {
            SupplierDAO.SaveSupplier(mapper.Map<SupplierDTOCreate, Supplier>(supplier));
        }

        public void UpdateSupplier(SupplierDTOPUT supplier)
        {
            SupplierDAO.UpdateSupplier(mapper.Map<SupplierDTOPUT, Supplier>(supplier));
        }
    }
}
