using AutoMapper;
using BusinessObject;
using DataAccess.DTOs;

namespace DataAccess.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            //Map Category
            CreateMap<Category, CategoryDTOResponse>();
            CreateMap<CategoryDTOCreateRequest, Category>();
            CreateMap<CategoryDTOPUT,Category>();

            //Map Product
            CreateMap<Product, ProductDTOResponse>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier));
            CreateMap<ProductDTOPUT, Product>();
            CreateMap<ProductDTOPOST, Product>();

            //Mapp Supplier
            CreateMap<Supplier, SupplierDTORespone>();
            CreateMap<SupplierDTOCreate, Supplier>();
            CreateMap<SupplierDTOPUT, Supplier>();  
        }
    }
}
