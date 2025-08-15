using ProvaPub.Models;

namespace ProvaPub.Repository.Interfaces
{
    public interface IOrderRepository
    {
        Task Insert(Order order, CancellationToken cancellationToken);
    }
}
