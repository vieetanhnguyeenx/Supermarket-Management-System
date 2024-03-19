using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class SupplierDAO
    {
        public static List<Supplier> GetSuppliers()
        {
            var ls = new List<Supplier>();
            try
            {
                using (var context = new MyDBContext())
                {
                    ls = context.Suppliers.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ls;
        }
        public static Supplier GetSupplierById(int id)
        {
            var sup = new Supplier(); try
            {
                using (var context = new MyDBContext())
                {
                    sup = context.Suppliers.FirstOrDefault(x => x.SupplierID == id);
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return sup;
        }
        public static void SaveSupplier(Supplier supplier)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    supplier.Discontinued = false;
                    context.Suppliers.Add(supplier);
                    context.SaveChanges();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateSupplier(Supplier supplier)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var sup = context.Suppliers.FirstOrDefault(x => x.SupplierID == supplier.SupplierID);
                    sup.CompanyName = supplier.CompanyName;
                    sup.Address = supplier.Address;
                    sup.Phone = sup.Phone;
                    context.Entry<Supplier>(sup).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteSupplier(int id)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var sup = context.Suppliers.FirstOrDefault(x => x.SupplierID == id);
                    sup.Discontinued = true;
                    context.Entry<Supplier>(sup).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
