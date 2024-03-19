using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        public static List<Product> SearchProducts(string keyword)
        {
            decimal price = 0;
            try
            {
                price = decimal.Parse(keyword);
            }
            catch (Exception e)
            {
                price = 0;
            }
            var ListProducts = new List<Product>();
            try
            {
                using (var context = new MyDBContext())
                {
                    var list = context.Products.Include(x=>x.Supplier).Include(x=>x.Category).Where(x => x.Discontinued == false).ToList();
                    ListProducts = list.Where(x => x.ProductName.Contains(keyword) || x.Price == price).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ListProducts;
        }

        public static List<Product> SearchDisableProducts(string keyword)
        {
            decimal price = 0;
            try
            {
                price = decimal.Parse(keyword);
            }
            catch (Exception e)
            {
                price = 0;
            }
            var ListProducts = new List<Product>();
            try
            {
                using (var context = new MyDBContext())
                {
                    var list = context.Products.Include(x => x.Supplier).Include(x => x.Category).Where(x => x.Discontinued == true).ToList();
                    ListProducts = list.Where(x => x.ProductName.Contains(keyword) || x.Price == price).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ListProducts;
        }

        public static List<Product> GetProducts()
        {
            var ListProducts = new List<Product>();
            try
            {
                using (var context = new MyDBContext())
                {
                    ListProducts = context.Products.Include(x => x.Supplier).Include(x => x.Category).Where(x => x.Discontinued == false).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ListProducts;
        }

        public static List<Product> GetDisabledProducts()
        {
            var ListProducts = new List<Product>();
            try
            {
                using (var context = new MyDBContext())
                {
                    ListProducts = context.Products.Include(x => x.Supplier).Include(x => x.Category).Where(x => x.Discontinued == true).ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ListProducts;
        }

        public static Product GetProductByID(int productID)
        {
            var product = new Product();
            try
            {
                using (var context = new MyDBContext())
                {
                    product = context.Products.Include(x => x.Supplier).Include(x => x.Category).SingleOrDefault(x => x.ProductID == productID && x.Discontinued == false);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return product;
        }

        public static Product GetDisabledProductByID(int productID)
        {
            var product = new Product();
            try
            {
                using (var context = new MyDBContext())
                {
                    product = context.Products.Include(x => x.Supplier).Include(x => x.Category).SingleOrDefault(x => x.ProductID == productID && x.Discontinued == true);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return product;
        }

        public static void SaveProduct(Product product)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateProduct(Product product)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DisableProduct(int productID)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var productToDelete = context.Products.SingleOrDefault(x => x.ProductID == productID);
                    productToDelete.Discontinued = true;
                    context.Entry(productToDelete).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UndisableProduct(int productID)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var productToDelete = context.Products.SingleOrDefault(x => x.ProductID == productID);
                    productToDelete.Discontinued = false;
                    context.Entry(productToDelete).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
