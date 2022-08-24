using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Helpers;

namespace WebApi.Repositories
{
    public abstract class GenericRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        protected GenericRepository(DataContext context)
        {
            _context = context;
        }

        protected virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            if (entity != null)
            {
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }

            return null!;
        }

        protected virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        protected virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {         
            return await _context.Set<TEntity>().FindAsync(predicate) ?? null!;
        }
    }
}
