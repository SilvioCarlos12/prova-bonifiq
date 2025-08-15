using ProvaPub.Dtos;
using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IProductService
    {
        Task<Pagination<Product>> ListProducts(int page,CancellationToken cancellationToken);
    }
}
