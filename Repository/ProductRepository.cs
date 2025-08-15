using Microsoft.EntityFrameworkCore;
using ProvaPub.Extensions;
using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;

namespace ProvaPub.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly TestDbContext _context;

        public ProductRepository(TestDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsPagination(int page, CancellationToken cancellationToken)
        {
            return await _context.Products
                 .PaginationQuery(page)
                 .AsNoTracking()
                 .OrderBy(x => x.Id)
                 .ToListAsync(cancellationToken);
        }

        public async Task<int> TotalProduct(CancellationToken cancellationToken)
        {
            return await _context.Products.CountAsync(cancellationToken);
        }
    }
}
