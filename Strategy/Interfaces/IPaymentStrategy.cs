using ProvaPub.Enums;

namespace ProvaPub.Strategy.Interfaces
{
    public interface IPaymentStrategy
    {
        string GetNameMethodPayment();
        bool TypePaymentValid(Payment typePayment);
    }
}
