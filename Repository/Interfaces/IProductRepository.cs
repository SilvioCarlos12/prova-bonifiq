using ProvaPub.Models;

namespace ProvaPub.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsPagination(int page,CancellationToken cancellationToken);
        Task<int> TotalProduct(CancellationToken cancellationToken);
    }
}
