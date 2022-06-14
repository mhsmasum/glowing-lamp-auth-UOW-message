using DotNetSecurity.Data;
using DotNetSecurity.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetSecurity.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        
      
        public ProductRepository(ApplicationDbContext context, ILogger logger): base (context, logger)
        {
            
        }
        public override async Task<Product> GetByIdAsync(string id)
        {
           return await _dbSet.Where(aa => aa.Id == Convert.ToInt64(id)).FirstOrDefaultAsync();
        }
    }
}
