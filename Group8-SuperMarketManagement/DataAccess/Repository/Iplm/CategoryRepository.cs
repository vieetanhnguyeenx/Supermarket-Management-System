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

        public void DeleteCategory(int id)
        {
            CategoryDAO.DeleteCategory(id); 
        }

        public List<CategoryDTOResponse> GetCategories()
        {
            return mapper.Map<List<Category>, List<CategoryDTOResponse>>(CategoryDAO.GetCategories());
        }

        public CategoryDTOResponse GetCategoryById(int id)
        {
            return mapper.Map<Category, CategoryDTOResponse>(CategoryDAO.GetCategoryById(id));
        }

        public void SaveCategory(CategoryDTOCreateRequest createRequest)
        {
            CategoryDAO.SaveCategory(mapper.Map <CategoryDTOCreateRequest,Category>(createRequest));
        }

        public void UpdateCategory(CategoryDTOPUT category)
        {
            CategoryDAO.UpdateCategory(mapper.Map<CategoryDTOPUT, Category>(category));
        }
    }
}
