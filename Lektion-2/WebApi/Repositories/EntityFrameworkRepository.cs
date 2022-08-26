using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Helpers;

namespace WebApi.Repositories
{
    public abstract class EntityFrameworkRepository<T> where T : class
    {
        private readonly DataContext _context;

        protected EntityFrameworkRepository(DataContext context)
        {
            _context = context;
        }

        //CRUD - Create Read(Get) Update Delete

        protected virtual async Task<T> CreateRecordAsync(T record)
        {
            try
            {
                if (record != null)
                {
                    _context.Add(record);
                    await _context.SaveChangesAsync();
                    return record;
                }
            }
            catch { }
            return null!;
        }

        protected virtual async Task<T> ReadRecordAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                // return await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id) ?? null!;
                return await _context.Set<T>().FirstOrDefaultAsync(predicate) ?? null!;
            }
            catch { }
            return null!;
        }

        protected virtual async Task<IEnumerable<T>> ReadRecordsAsync(int take = 0)
        {
            try
            {
                if (take != 0)
                    return await _context.Set<T>().Take(take).ToListAsync() ?? null!;
                
                return await _context.Set<T>().ToListAsync() ?? null!;
            }
            catch { }
            return null!;
        }

        protected virtual async Task<T> UpdateRecordAsync(Expression<Func<T, bool>> predicate, T record)
        {
            try
            {
                var _record = await _context.Set<T>().FirstOrDefaultAsync(predicate);

                if (_record != null)
                {
                    _context.Entry(record).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return _record;
                }
            }
            catch { }
            return null!;
        }

        protected virtual async Task<bool> DeleteRecordAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var _record = await _context.Set<T>().FirstOrDefaultAsync(predicate);

                if (_record != null)
                {
                    _context.Remove(_record);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch { }
            return false;
        }
    }
}
