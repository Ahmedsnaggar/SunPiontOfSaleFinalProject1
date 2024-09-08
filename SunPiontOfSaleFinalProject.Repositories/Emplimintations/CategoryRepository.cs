using ContextFile;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Category> AddCategory(Category category)
        {
            await _db.Categories.AddAsync(category);
           await  _db.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategory(int id)
        {
            var category = await GetCategoryById(id);
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
           return await _db.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task <Category> UpDateCategory(int id, Category category)
        {
            var oldCategory = await GetCategoryById(id);
            oldCategory.CategoryName = category.CategoryName;
            oldCategory.CategoryDescription = category.CategoryDescription;
           await  _db.SaveChangesAsync();
            return oldCategory;
        }
    }
}
