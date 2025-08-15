using ProvaPub.ChainOfResponsibility;
using ProvaPub.Enums;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
	public class OrderService: IOrderService
    {
        TestDbContext _ctx;
        private readonly ICreatePayment _createPayment;

        public OrderService(TestDbContext ctx, ICreatePayment createPayment)
        {
            _ctx = ctx;
            _createPayment = createPayment;
        }

        public async Task<Order> PayOrder(Payment paymentMethod, decimal paymentValue, int customerId)
		{
            var typePayment = _createPayment.GetPaymentStrategy(paymentMethod);


            return await InsertOrder(new Order(paymentValue,customerId));


		}

		public async Task<Order> InsertOrder(Order order)
        {
            var orderInsert = (await _ctx.Orders.AddAsync(order)).Entity;
            await _ctx.SaveChangesAsync();
            orderInsert.OrderDate = orderInsert.OrderDate.AddHours(-3);
            return orderInsert;
        }
	}
}
