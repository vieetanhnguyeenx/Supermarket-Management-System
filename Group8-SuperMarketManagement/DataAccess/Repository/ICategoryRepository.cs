using DataAccess.DTOs;

namespace DataAccess.Repository
{
    public interface ICategoryRepository
    {
        List<CategoryDTOResponse> GetCategories();
    }
}
