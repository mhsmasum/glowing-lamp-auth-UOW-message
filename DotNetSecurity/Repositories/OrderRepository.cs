using DotNetSecurity.Data;
using DotNetSecurity.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetSecurity.Repositories
{
    public class OrderRepository:GenericRepository<Order>,IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {

        }

        public override async Task<Order> GetByIdAsync(string id)
        {
            return await _dbSet.Where(aa => aa.Id == Convert.ToInt64(id)).FirstOrDefaultAsync();
        }

        public override async Task CreateAsync(Order t)
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
    }
}
