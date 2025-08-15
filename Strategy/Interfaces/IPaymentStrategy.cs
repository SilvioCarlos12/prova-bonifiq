using ProvaPub.Enums;

namespace ProvaPub.Strategy.Interfaces
{
    public interface IPaymentStrategy
    {
        string GetTypePayment();
        bool TypePaymentValid(Payment typePayment);
    }
}
