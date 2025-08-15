using ProvaPub.ChainOfResponsibility;
using ProvaPub.Dtos;
using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
	public class OrderService: IOrderService
    {
        private readonly ICreatePayment _createPayment;
        private readonly IOrderRepository _orderRepository;

        public OrderService(ICreatePayment createPayment, IOrderRepository orderRepository)
        {
            _createPayment = createPayment;
            _orderRepository = orderRepository;
        }

        public async Task<Order> PayOrder(OrderInsertDto orderInsertDto, CancellationToken cancellationToken)
		{
            var typePayment = _createPayment.GetMethodPayment(orderInsertDto.PaymentMethod);

            return await InsertOrder(new Order(orderInsertDto.PaymentValue, orderInsertDto.CustomerId,typePayment.GetNameMethodPayment()),cancellationToken);
		}

		public async Task<Order> InsertOrder(Order order, CancellationToken cancellationToken)
        {
            await _orderRepository.Insert(order, cancellationToken);
            order.OrderDate = order.OrderDate.AddHours(-3);
            return order;
        }
	}
}
