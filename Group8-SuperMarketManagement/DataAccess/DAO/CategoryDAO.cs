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
        public static Category GetCategoryById(int id)
        {
            var Category = new Category();
            try
            {
                using (var context = new MyDBContext())
                {
                    Category = context.Categories.FirstOrDefault(x => x.CategoryID == id);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return Category;
        }
        public static void SaveCategory(Category category)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var ca = new Category()
                    {
                        CategoryName = category.CategoryName,
                        Description = category.Description,
                        Discontinued = false
                    };
                    context.Categories.Add(ca);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static void UpdateCategory(Category category)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var cate = context.Categories.FirstOrDefault(x => x.CategoryID == category.CategoryID);
                    cate.CategoryName = category.CategoryName;
                    cate.Description = category.Description;
                    context.Entry<Category>(cate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void DeleteCategory(int id)
        {
            try
            {
                using (var context = new MyDBContext())
                {
                    var cate = context.Categories.FirstOrDefault(x => x.CategoryID == id);
                    cate.Discontinued = true;
                    context.Entry<Category>(cate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
