using ProvaPub.Enums;
using ProvaPub.Strategy.Interfaces;

namespace ProvaPub.ChainOfResponsibility
{
    public interface ICreatePayment
    {
        IPaymentStrategy GetPaymentStrategy(Payment typePayment);
    }
}
