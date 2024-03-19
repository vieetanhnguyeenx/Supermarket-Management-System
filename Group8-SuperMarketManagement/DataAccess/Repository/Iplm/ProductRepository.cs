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
    public class ProductRepository : IProductRepository
    {
        private IMapper mapper;
        public ProductRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationMapper());
            });
            mapper = config.CreateMapper();
        }

        public void DisableProduct(int productID)
        {
            ProductDAO.DisableProduct(productID);
        }

        public ProductDTOResponse GetDisabledProduct(int productID)
        {
            return mapper.Map<Product, ProductDTOResponse>(ProductDAO.GetDisabledProductByID(productID));
        }

        public List<ProductDTOResponse> GetDisabledProducts()
        {
            return mapper.Map<List<Product>, List<ProductDTOResponse>>(ProductDAO.GetDisabledProducts());
        }

        public ProductDTOResponse GetProduct(int productID)
        {
            return mapper.Map<Product, ProductDTOResponse>(ProductDAO.GetProductByID(productID));
        }

        public List<ProductDTOResponse> GetProducts()
        {
            return mapper.Map<List<Product>, List<ProductDTOResponse>>(ProductDAO.GetProducts());
        }

        public void SaveProduct(ProductDTOPOST productDTOPOST)
        {
            var product = mapper.Map<Product>(productDTOPOST);
            ProductDAO.SaveProduct(product);

        }

        public List<ProductDTOResponse> SearchDisableProducts(string keyword)
        {
            return mapper.Map<List<Product>, List<ProductDTOResponse>>(ProductDAO.SearchDisableProducts(keyword));
        }

        public List<ProductDTOResponse> SearchProducts(string keyword)
        {
            return mapper.Map<List<Product>, List<ProductDTOResponse>>(ProductDAO.SearchProducts(keyword));
        }

        public void UndisableProduct(int productID)
        {
            ProductDAO.UndisableProduct(productID);
        }

        public void UpdateProduct(ProductDTOPUT productDTOPUT)
        {
            var product = mapper.Map<Product>(productDTOPUT);
            ProductDAO.UpdateProduct(product);  
        }
    }
}
