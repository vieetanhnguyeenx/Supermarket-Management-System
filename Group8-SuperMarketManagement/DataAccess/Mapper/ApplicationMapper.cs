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
        }
    }
}
