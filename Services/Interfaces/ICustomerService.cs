using ProvaPub.Dtos;
using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface ICustomerService
    {
        Pagination<Customer> ListCustomers(int page);
        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
    }
}
