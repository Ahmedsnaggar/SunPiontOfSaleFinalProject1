using ContextFile;
using SunPiontOfSaleFinalProject.Entiteis.Models;
using SunPiontOfSaleFinalProject.Repositories.Interfaces;

namespace SunPiontOfSaleFinalProject.Repositories.Emplimintations
{
    public class CategoryRepository : ICategoreRepository
    {
        private MyDbContext _db;
        public CategoryRepository(MyDbContext db)
        {
            _db = db;
        }

        public Category AddCategory(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return category;
        }

        public void DeleteCategory(int id)
        {
            var category = GetCategoryById(id);
            _db.Categories.Remove(category);
            _db.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories()
        {
           return _db.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _db.Categories.Find(id);
        }

        public Category UpDateCategory(int id, Category category)
        {
            var oldCategory = GetCategoryById(id);
            oldCategory.CategoryName = category.CategoryName;
            oldCategory.CategoryDescription = category.CategoryDescription;
            _db.SaveChanges();
            return oldCategory;
        }
    }
}
