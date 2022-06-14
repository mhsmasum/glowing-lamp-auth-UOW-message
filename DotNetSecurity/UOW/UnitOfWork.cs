using DotNetSecurity.Data;
using DotNetSecurity.Repositories;

namespace DotNetSecurity.UOW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public UnitOfWork(ApplicationDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            Products = new ProductRepository(_context, _logger);
            Orders = new OrderRepository(_context, _logger);
        }

        public IProductRepository Products { get;private set; }

        public IOrderRepository Orders { get; private set; }


       
        public async Task  CompleteAsync()
        {
            try
            {
                var data = await _context.SaveChangesAsync();
            }
            catch (Exception aa)
            {
                var messge = aa.ToString();
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
