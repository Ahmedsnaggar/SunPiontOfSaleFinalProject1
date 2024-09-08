using SunPiontOfSaleFinalProject.Entiteis.Models;

namespace SunPiontOfSaleFinalProject.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> AddItem(T item);
        Task<T> UpdateItem(T item);
        Task DeleteItem(int id);
    }
}
