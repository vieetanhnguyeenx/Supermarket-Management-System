using BusinessObject;

namespace DataAccess.DAO
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            var ListCategories = new List<Category>();
            try
            {
                using (var context = new MyDBContext())
                {
                    ListCategories = context.Categories.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return ListCategories;
        }
    }
}
