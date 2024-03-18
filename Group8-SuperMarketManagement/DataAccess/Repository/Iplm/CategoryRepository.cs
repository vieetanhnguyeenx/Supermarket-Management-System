using AutoMapper;
using BusinessObject;
using DataAccess.DAO;
using DataAccess.DTOs;
using DataAccess.Mapper;

namespace DataAccess.Repository.Iplm
{
    public class CategoryRepository : ICategoryRepository
    {
        private IMapper mapper;

        public CategoryRepository()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ApplicationMapper());
            });
            mapper = config.CreateMapper();
        }
        public List<CategoryDTOResponse> GetCategories()
        {
            return mapper.Map<List<Category>, List<CategoryDTOResponse>>(CategoryDAO.GetCategories());
        }
    }
}
