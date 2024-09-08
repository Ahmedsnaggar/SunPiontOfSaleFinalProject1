using ContextFile;
using Microsoft.EntityFrameworkCore;
using SunPiontOfSaleFinalProject.Repositories.Interfaces;

namespace SunPiontOfSaleFinalProject.Repositories.Emplimintations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private MyDbContext _db;
        private DbSet<T> _dbSet;
        public BaseRepository(MyDbContext db, DbSet<T> dbSet)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public async Task<T> AddItem(T item)
        {
            await _dbSet.AddAsync(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task DeleteItem(int id)
        {
            var entity = await GetById(id);
            _dbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return _dbSet;
        }

        public async Task<T> GetById(int id)
        {
            return await  _dbSet.FindAsync(id);
        }

        public async Task<T> UpdateItem(T item)
        {
            var oldEntity = _dbSet.Attach(item);
            oldEntity.State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return item;
        }
    }
}
