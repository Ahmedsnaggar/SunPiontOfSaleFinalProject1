using SunPiontOfSaleFinalProject.Entiteis.Models;

namespace SunPiontOfSaleFinalProject.Repositories.Interfaces
{
    public interface ICategoreRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task<Category> AddCategory(Category category);
       Task<Category> UpDateCategory(int id, Category category);
        Task DeleteCategory(int id);
    }
}
