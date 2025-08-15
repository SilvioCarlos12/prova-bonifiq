using ProvaPub.Dtos;
using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IProductService
    {
        Pagination<Product> ListProducts(int page);
    }
}
