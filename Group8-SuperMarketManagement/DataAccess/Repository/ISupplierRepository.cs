using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ISupplierRepository
    {
        List<SupplierDTORespone> GetSuppliers();
        SupplierDTORespone GetSupplierById(int id);
        void SaveSupplier(SupplierDTOCreate supplier);
        void UpdateSupplier(SupplierDTOPUT supplier);
        void DeleteSupplier(int id);    
    }
}
