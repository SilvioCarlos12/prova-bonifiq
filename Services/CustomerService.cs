using ProvaPub.Dtos;
using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
    public class CustomerService: ICustomerService
    {

        private readonly ICustomerRepository _customerRepository;

        public CustomerService( ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Pagination<Customer>> ListCustomers(int page,CancellationToken cancellationToken)
        {
            var countCustomer = await _customerRepository.CustomerTotal(cancellationToken);
            var customers = await _customerRepository.GetCustomersPagination(page, cancellationToken);
            return new Pagination<Customer>(customers ,countCustomer, page);
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue, CancellationToken cancellationToken)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            //Business Rule: Non registered Customers cannot purchase
            var customer = await _customerRepository.GetCustomer(customerId,cancellationToken);
            if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

            //Business Rule: A customer can purchase only a single time per month
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = await _customerRepository.CanPurchaseThisMonth(customerId,baseDate,cancellationToken);
            if (ordersInThisMonth > 0)
                return false;

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            var haveBoughtBefore = await _customerRepository.GetCustomerPurchaseCount(customerId,cancellationToken);
            if (haveBoughtBefore == 0 && purchaseValue > 100)
                return false;

            //Business Rule: A customer can purchases only during business hours and working days
            if (DateTime.UtcNow.Hour < 8 || DateTime.UtcNow.Hour > 18 || DateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday || DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday)
                return false;


            return true;
        }

    }
}
