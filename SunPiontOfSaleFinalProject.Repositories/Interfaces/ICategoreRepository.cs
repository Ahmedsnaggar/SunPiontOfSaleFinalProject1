using SunPiontOfSaleFinalProject.Entiteis.Models;

namespace SunPiontOfSaleFinalProject.Repositories.Interfaces
{
    public interface ICategoreRepository
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        Category AddCategory(Category category);
        Category UpDateCategory(int id, Category category);
        void DeleteCategory(int id);
    }
}
