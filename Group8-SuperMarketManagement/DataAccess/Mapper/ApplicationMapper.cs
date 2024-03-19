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

            //Map ST
            CreateMap<SalesTransaction, SalesTransactionDTOResponse>()
           .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee))
           .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
           .ForMember(dest => dest.TransactionDetails, opt => opt.MapFrom(src => src.TransactionDetails));
            CreateMap<SalesTransactionDTOPOST, SalesTransaction>();
            CreateMap<TransactionsDetailDTOPOST, TransactionDetail>();

            //Mapp Supplier
            CreateMap<Supplier, SupplierDTORespone>();
            CreateMap<SupplierDTOCreate, Supplier>();
            CreateMap<SupplierDTOPUT, Supplier>();  
        }
    }
}
