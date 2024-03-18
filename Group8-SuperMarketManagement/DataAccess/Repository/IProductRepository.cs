using DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        List<ProductDTOResponse> GetProducts();
        List<ProductDTOResponse> SearchProducts(string keyword);
        ProductDTOResponse GetProduct(int productID);
        void UpdateProduct(ProductDTOPUT productDTOPUT);
        void DisableProduct(int productID);
        void SaveProduct (ProductDTOPOST productDTOPOST);
    }
}
