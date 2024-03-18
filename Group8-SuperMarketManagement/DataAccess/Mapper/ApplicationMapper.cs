using AutoMapper;
using BusinessObject;
using DataAccess.DTOs;

namespace DataAccess.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Category, CategoryDTOResponse>();
            CreateMap<Product, ProductDTOResponse>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier));
            CreateMap<ProductDTOPUT, Product>();
            CreateMap<ProductDTOPOST, Product>();
        }
    }
}
