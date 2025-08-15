using Microsoft.EntityFrameworkCore;
using ProvaPub.Extensions;
using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;

namespace ProvaPub.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TestDbContext _testDbContext;

        public CustomerRepository(TestDbContext testDbContext)
        {
            _testDbContext = testDbContext;
        }

        public async Task<int> CanPurchaseThisMonth(int id, DateTime baseDate, CancellationToken cancellationToken)
        {
            return await _testDbContext.Customers.Include(x => x.Orders)
                 .AsNoTracking()
                 .CountAsync(x => x.Id == id && x.Orders.Any(x => x.OrderDate >= baseDate), cancellationToken);
        }

        public async Task<int> CustomerTotal(CancellationToken cancellationToken)
        {
            return await _testDbContext.Customers.CountAsync(cancellationToken);
        }

        public async Task<Customer?> GetCustomer(int id, CancellationToken cancellationToken)
        {
            return await _testDbContext.Customers.FirstAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<int> GetCustomerPurchaseCount(int id, CancellationToken cancellationToken)
        {
            return await _testDbContext.Customers.CountAsync(s => s.Id == id && s.Orders.Any(), cancellationToken);
        }

        public async Task<List<Customer>> GetCustomersPagination(int page, CancellationToken cancellationToken)
        {
            return await _testDbContext.Customers.PaginationQuery(page).AsNoTracking().ToListAsync(cancellationToken);
        }
    }
}
