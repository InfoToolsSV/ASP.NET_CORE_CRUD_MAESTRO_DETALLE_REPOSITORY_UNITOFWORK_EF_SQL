using OrderManagement.Data;
using OrderManagement.Models;
using OrderManagement.Repository;

namespace OrderManagement.UnitOfWork
{
    public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
    {
        private readonly AppDbContext _context = appDbContext;
        private IRepository<Order> _orders;
        private IRepository<OrderDetail> _ordersDetail;

        public IRepository<Order> Orders => _orders ??= new Repository<Order>(_context);

        public IRepository<OrderDetail> OrderDetails => _ordersDetail ??= new Repository<OrderDetail>(_context);

        public void Dispose() => _context.Dispose();

        public async Task<int> Save() => await _context.SaveChangesAsync();
    }
}