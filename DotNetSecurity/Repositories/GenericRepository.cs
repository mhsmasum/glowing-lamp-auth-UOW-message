using DotNetSecurity.Data;
using DotNetSecurity.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DotNetSecurity.Repositories
{
    public abstract class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> _dbSet;
        protected readonly ILogger _logger;
        public GenericRepository(ApplicationDbContext context , ILogger logger )
        {
            _context = context;
            _logger = logger;
            _dbSet = context.Set<T>();
        }


        public virtual async Task CreateAsync(T t)
        {
            try
            {
                await _dbSet.AddAsync(t);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public virtual async Task BulkInsert(List<T> t)
        {
            await _dbSet.AddRangeAsync(t);
        }
        public virtual async Task BulkUpdate(List<T> t)
        {
            await Task.Delay(500);
            _dbSet.UpdateRange(t);
        }
        public virtual async Task UpdateAsync(T t)
        {
            await Task.FromResult(_dbSet.Update(t));
        }

        public virtual async Task DeleteAsync(string id)
        {
            var entity = await _dbSet.FindAsync(id);
                

            // await Task.FromResult(_dbContext.Set<T>().Remove(entity));  
            await Task.Run(() => _dbSet.Remove(entity));
        }
        public virtual async Task DeleteRangeAsync(ICollection<T> entities)
        {
            //await Task.Delay(500);
            await Task.Run(() => _dbSet.RemoveRange(entities));
        }
    

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
            // return _dbContext.Set<T>();
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {

            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public abstract  Task<T> GetByIdAsync(string id) ;
       

        public virtual IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbSet.Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> GetByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
        public virtual async Task<ICollection<T>> RunSqlQueryAsync(string sqlQuery)
        {
            return await _dbSet.FromSqlRaw(sqlQuery).ToListAsync();
        }
        public virtual IQueryable<T> RunSqlQueryAsQueryable(string sqlQuery)
        {
            return _dbSet.FromSqlRaw(sqlQuery).AsNoTracking();
        }

       
    }
}
