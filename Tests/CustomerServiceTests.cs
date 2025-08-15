using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {
        private readonly CustomerService _customerService;
        private readonly ICustomerRepository _customerRepository;
        public CustomerServiceTests()
        {
            _customerRepository =  Substitute.For<ICustomerRepository>();
            _customerService = new CustomerService(_customerRepository);
        }

        [Theory(DisplayName ="CustomerId invalid")]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task CustomerId_Invalid(int customerIdInvalid)
        {
            //Arrange
            var purchaseValue = 10M;
            //Act
            var act = async () =>  await _customerService.CanPurchase(customerIdInvalid, purchaseValue, CancellationToken.None);
            //Assert

           var mensage = await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
           mensage.WithMessage("Specified argument was out of the range of valid values. (Parameter 'customerId')");   
        }

        [Theory(DisplayName = "Pushase Value Invalid")]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task Pushase_Value_Invalid(int pushasedInvalid)
        {
            //Arrange
            var customerId = 10;
            //Act
            var act = async () => await _customerService.CanPurchase(customerId, pushasedInvalid, CancellationToken.None);
            //Assert

            var mensage = await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
            mensage.WithMessage("Specified argument was out of the range of valid values. (Parameter 'purchaseValue')");
        }

        [Fact(DisplayName ="CustomerId Not Exist")]
        public async Task CustomerId_Not_Exist()
        {
            //Arrange
            var purchaseValue = 10M;
            var customerId = 10;
            _customerRepository.GetCustomer(customerId, CancellationToken.None).ReturnsNull();
            //Act
            var act = async () => await _customerService.CanPurchase(customerId, purchaseValue, CancellationToken.None);
            //Assert

            var mensage = await act.Should().ThrowAsync<InvalidOperationException>();
            mensage.WithMessage($"Customer Id {customerId} does not exists");
        }

        [Fact(DisplayName = "A customer can purchase only a single time per month ")]
        public async Task CanPurchaseThisMonth_ShouldReturnTrue_WhenAlreadyPurchasedInCurrentMonth()
        {
            //Arrange
            var purchaseValue = 10M;
            var customerId = 10;
            var dataBase = DateTime.UtcNow;
            var orderInMonths = 10;
           _customerRepository.GetCustomer(customerId, CancellationToken.None).Returns(new Customer()
           {
               Id = customerId
           });

            _customerRepository.CanPurchaseThisMonth(customerId, dataBase, CancellationToken.None).Returns(orderInMonths);
            //Act
            var result = await _customerService.CanPurchase(customerId, purchaseValue, CancellationToken.None);
            //Assert
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "A customer that never bought before can make a first purchase of maximum 100,00")]
        public async Task CanPurchaseThisMonth_ShouldReturnTrue_WhenAlreadyPurchasedInCurrentMonth()
        {
            //Arrange
            var purchaseValue = 10M;
            var customerId = 10;
            var dataBase = DateTime.UtcNow;
            var orderInMonths = 10;
            _customerRepository.GetCustomer(customerId, CancellationToken.None).Returns(new Customer()
            {
                Id = customerId
            });

            _customerRepository.CanPurchaseThisMonth(customerId, dataBase, CancellationToken.None).Returns(orderInMonths);
            //Act
            var result = await _customerService.CanPurchase(customerId, purchaseValue, CancellationToken.None);
            //Assert
            result.Should().BeFalse();
        }

    }
}
//public async Task<bool> CanPurchase(int customerId, decimal purchaseValue, CancellationToken cancellationToken)
//{
//   

//    

//  



//    //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
//    var haveBoughtBefore = await _customerRepository.GetCustomerPurchaseCount(customerId, cancellationToken);
//    if (haveBoughtBefore == 0 && purchaseValue > 100)
//        return false;

//    //Business Rule: A customer can purchases only during business hours and working days
//    if (DateTime.UtcNow.Hour < 8 || DateTime.UtcNow.Hour > 18 || DateTime.UtcNow.DayOfWeek == DayOfWeek.Saturday || DateTime.UtcNow.DayOfWeek == DayOfWeek.Sunday)
//        return false;


//    return true;
//}