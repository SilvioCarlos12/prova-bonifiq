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
            _customerRepository = Substitute.For<ICustomerRepository>();
            _customerService = new CustomerService(_customerRepository);
        }

        [Theory(DisplayName = "CustomerId invalid")]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task CustomerId_Invalid(int customerIdInvalid)
        {
            //Arrange
            var purchaseValue = 10M;
            //Act
            var act = async () => await _customerService.CanPurchase(customerIdInvalid, purchaseValue, CancellationToken.None);
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

        [Fact(DisplayName = "CustomerId Not Exist")]
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
        public async Task CanPurchaseThisMonth_ShouldReturnFalse_WhenSecondPurchase()
        {
            //Arrange
            var purchaseValue = 10M;
            var customerId = 10;
            var orderInMonths = 10;
            _customerRepository.GetCustomer(customerId, CancellationToken.None).Returns(new Customer()
            {
                Id = customerId
            });

            _customerRepository.CanPurchaseThisMonth(customerId,Arg.Any<DateTime>(), CancellationToken.None).Returns(orderInMonths);
            //Act
            var result = await _customerService.CanPurchase(customerId, purchaseValue, CancellationToken.None);
            //Assert
            result.Should().BeFalse();
        }

        [Fact(DisplayName = " A customer that never bought before can make a first purchase of maximum 100,00")]
        public async Task IsFirstPurchaseWithinLimit_ShouldReturnFalse_WhenNewCustomer_AndAmountOver100()
        {
            //Arrange
            var purchaseValue = 110M;
            var customerId = 10;
            var dataBase = DateTime.UtcNow;
            var orderInMonths = 0;
            _customerRepository.GetCustomer(customerId, CancellationToken.None).Returns(new Customer()
            {
                Id = customerId
            });

            _customerRepository.CanPurchaseThisMonth(customerId, dataBase, CancellationToken.None).Returns(orderInMonths);

            _customerRepository.GetCustomerPurchaseCount(customerId, CancellationToken.None).Returns(orderInMonths);
            //Act
            var result = await _customerService.CanPurchase(customerId, purchaseValue, CancellationToken.None);
            //Assert
            result.Should().BeFalse();
        }

        [Theory(DisplayName = "A customer can purchases only during business hours and working days")]
        [InlineData(5)]
        [InlineData(23)]
        public async Task CanPurchaseNow_ShouldReturnFalse_WhenOutsideBusinessHours(int hoursOutside)
        {
            //Arrange
            var purchaseValue = 120M;
            var customerId = 10;
            var dataBase = DateTime.UtcNow;
            var orderInMonths = 10;
            var dateOutsideBusinessHours = new DateOnly(2025, 8, 15);
            var hours = new TimeOnly(hoursOutside,30);
            var dataTime = dateOutsideBusinessHours.ToDateTime(hours);
            _customerRepository.GetCustomer(customerId, CancellationToken.None).Returns(new Customer()
            {
                Id = customerId
            });

            _customerRepository.CanPurchaseThisMonth(customerId, dataBase, CancellationToken.None).Returns(orderInMonths);

            _customerRepository.GetCustomerPurchaseCount(customerId, CancellationToken.None).Returns(orderInMonths);

            var customerService = new CustomerService(_customerRepository, dataTime);

            //Act
            var result = await customerService.CanPurchase(customerId, purchaseValue, CancellationToken.None);
            //Assert
            result.Should().BeFalse();
        }

        [Theory(DisplayName = "A customer can purchases only during business hours and working days")]
        [InlineData(16)]
        [InlineData(17)]
        public async Task CanPurchaseNow_ShouldReturnFalse_WhenOutsideBusinessDays(int dayOutSide)
        {
            //Arrange
            var purchaseValue = 120M;
            var customerId = 10;
            var dataBase = DateTime.UtcNow;
            var orderInMonths = 10;
            var dateOutsideBusinessHours = new DateOnly(2025, 8, dayOutSide);
            var hours = new TimeOnly(9);
            var dataTime = dateOutsideBusinessHours.ToDateTime(hours);
            _customerRepository.GetCustomer(customerId, CancellationToken.None).Returns(new Customer()
            {
                Id = customerId
            });

            _customerRepository.CanPurchaseThisMonth(customerId, dataBase, CancellationToken.None).Returns(orderInMonths);

            _customerRepository.GetCustomerPurchaseCount(customerId, CancellationToken.None).Returns(orderInMonths);

            var customerService = new CustomerService(_customerRepository, dataTime);

            //Act
            var result = await customerService.CanPurchase(customerId, purchaseValue, CancellationToken.None);
            //Assert
            result.Should().BeFalse();
        }


        [Fact(DisplayName = "the customer can make the purchase")]
        public async Task CanPurchaseNow_ShouldReturnTrue()
        {
            //Arrange
            var purchaseValue = 120M;
            var customerId = 10;
            var dataBase = DateTime.UtcNow;
            var orderInMonths = 10;
            var dateOutsideBusinessHours = new DateOnly(2025, 8, 15);
            var hours = new TimeOnly(9,30);
            var dataTime = dateOutsideBusinessHours.ToDateTime(hours);
            _customerRepository.GetCustomer(customerId, CancellationToken.None).Returns(new Customer()
            {
                Id = customerId
            });

            _customerRepository.CanPurchaseThisMonth(customerId, dataBase, CancellationToken.None).Returns(orderInMonths);

            _customerRepository.GetCustomerPurchaseCount(customerId, CancellationToken.None).Returns(orderInMonths);

            var customerService = new CustomerService(_customerRepository, dataTime);

            //Act
            var result = await customerService.CanPurchase(customerId, purchaseValue, CancellationToken.None);
            //Assert
            result.Should().BeTrue();
        }

    }
}