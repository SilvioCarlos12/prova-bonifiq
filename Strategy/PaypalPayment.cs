using ProvaPub.Enums;
using ProvaPub.Strategy.Interfaces;

namespace ProvaPub.Strategy
{
    public class PaypalPayment : IPaymentStrategy
    {
        public string GetNameMethodPayment()
        {
            return "PayPal";
        }

        public bool TypePaymentValid(Payment typePayment)
        {
            return Payment.Paypal == typePayment;
        }
    }
}
