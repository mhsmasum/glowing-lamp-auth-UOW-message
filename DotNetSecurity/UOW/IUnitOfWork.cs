using DotNetSecurity.Repositories;

namespace DotNetSecurity.UOW
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        Task CompleteAsync();
    }
}
