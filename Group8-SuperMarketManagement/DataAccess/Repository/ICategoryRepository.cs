using DataAccess.DTOs;

namespace DataAccess.Repository
{
    public interface ICategoryRepository
    {
        List<CategoryDTOResponse> GetCategories();
        CategoryDTOResponse GetCategoryById(int id);
        void SaveCategory(CategoryDTOCreateRequest createRequest);
        void UpdateCategory(CategoryDTOPUT category);
        void DeleteCategory(int id);
    }
}
