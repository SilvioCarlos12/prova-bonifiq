using ProvaPub.Enums;
using ProvaPub.Strategy.Interfaces;

namespace ProvaPub.Strategy
{
    public class CreditCardPayment : IPaymentStrategy
    {
        public string GetNameMethodPayment()
        {
            return "CreditCard";
        }

        public bool TypePaymentValid(Payment typePayment)
        {
            return Payment.CreditCard == typePayment; 
        }
    }
}
