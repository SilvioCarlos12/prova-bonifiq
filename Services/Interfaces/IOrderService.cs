using ProvaPub.Enums;
using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> PayOrder(Payment paymentMethod, decimal paymentValue, int customerId);
        Task<Order> InsertOrder(Order order);
    }
}
