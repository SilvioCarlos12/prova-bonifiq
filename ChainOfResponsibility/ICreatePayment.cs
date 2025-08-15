using ProvaPub.Enums;
using ProvaPub.Strategy.Interfaces;

namespace ProvaPub.ChainOfResponsibility
{
    public interface ICreatePayment
    {
        IPaymentStrategy GetMethodPayment(Payment typePayment);
    }
}
