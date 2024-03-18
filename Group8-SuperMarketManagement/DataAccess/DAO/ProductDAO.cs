using BusinessObject;
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
            int price = 0;
            try
            {
                price = int.Parse(keyword);
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
                    var list = context.Products.Where(x => x.Discontinued == false).ToList();
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
                    ListProducts = context.Products.Where(x => x.Discontinued == false).ToList();
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
                    product = context.Products.SingleOrDefault(x => x.ProductID == productID && x.Discontinued == false);
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
    }
}
