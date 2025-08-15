using ProvaPub.Models;

namespace ProvaPub.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomer(int id, CancellationToken cancellationToken);
        Task<int> GetCustomerPurchaseCount(int id, CancellationToken cancellationToken);
        Task<int> CanPurchaseThisMonth(int id, DateTime baseDate, CancellationToken cancellationToken);
        Task<int> CustomerTotal(CancellationToken cancellationToken);
        Task<List<Customer>> GetCustomersPagination(int page, CancellationToken cancellationToken);
    }
}
