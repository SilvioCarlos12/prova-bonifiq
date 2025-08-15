using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;

namespace ProvaPub.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TestDbContext _testDbContext;

        public OrderRepository(TestDbContext testDbContext)
        {
            _testDbContext = testDbContext;
        }

        public async Task Insert(Order order, CancellationToken cancellationToken)
        {
           _testDbContext.Orders.Add(order);
           await _testDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
