using OrderManagement.Models;
using OrderManagement.Repository;

namespace OrderManagement.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Order> Orders { get; }
        IRepository<OrderDetail> OrderDetails { get; }
        Task<int> Save();
    }
}