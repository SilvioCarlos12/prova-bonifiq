using ProvaPub.Dtos;
using ProvaPub.Enums;
using ProvaPub.Models;

namespace ProvaPub.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> PayOrder(OrderInsertDto orderInsertDto,CancellationToken cancellationToken);
        Task<Order> InsertOrder(Order order, CancellationToken cancellationToken);
    }
}
