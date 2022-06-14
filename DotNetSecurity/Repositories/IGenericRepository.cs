using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace DotNetSecurity.Repositories
{
    public interface IGenericRepository<T> where T : class
    {

        Task CreateAsync(T entity);

        Task BulkInsert(List<T> t);

        Task UpdateAsync(T entity);

        Task BulkUpdate(List<T> t);

        Task DeleteAsync(string id);
        Task DeleteRangeAsync(ICollection<T> entities);
        

        IQueryable<T> GetAll();

        Task<ICollection<T>> GetAllAsync();

        Task<T> GetByIdAsync(string id);

        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);

        Task<ICollection<T>> GetByAsync(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> RunSqlQueryAsync(string sqlQuery);
        IQueryable<T> RunSqlQueryAsQueryable(string sqlQuery);
       
    }
}
