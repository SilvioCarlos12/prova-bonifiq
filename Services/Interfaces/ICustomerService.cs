using ProvaPub.Dtos;
using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Pagination<Customer>> ListCustomers(int page, CancellationToken cancellationToken);
        Task<bool> CanPurchase(int customerId, decimal purchaseValue,CancellationToken cancellationToken);
    }
}
