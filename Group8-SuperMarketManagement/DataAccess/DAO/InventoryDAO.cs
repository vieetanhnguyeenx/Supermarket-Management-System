using BusinessObject;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class InventoryDAO
    {
        public static List<Inventory> GetInventories()
        {
            var list = new List<Inventory>(); try
            {
                using (var context = new MyDBContext())
                {
                    list = context.Inventories.Include(x => x.Employee).Include(x => x.Product).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public static Inventory GetInventoryById(int id)
        {
            var inv = new Inventory();
            try
            {
                using (var context = new MyDBContext())
                {
                    inv = context.Inventories.FirstOrDefault(x => x.InventoryID == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return inv;
        }
        public static void SaveInventory(Inventory inventory)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    inventory.Discontinued = false;
                    context.Inventories.Add(inventory);
                    var pro = context.Products.FirstOrDefault(x => x.ProductID == inventory.ProductID);
                    pro.TotalQuantity += inventory.Quantity;
                    context.Entry<Product>(pro).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void PutInventory(Inventory inventory)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Entry<Inventory>(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteInventory(int id)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var inv = context.Inventories.FirstOrDefault(x => x.InventoryID == id);
                    inv.Discontinued = true;
                    context.Entry<Inventory>(inv).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
