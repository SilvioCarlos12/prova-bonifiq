using ProvaPub.Enums;
using ProvaPub.Strategy.Interfaces;

namespace ProvaPub.Strategy
{
    public class PixPayment : IPaymentStrategy
    {
        public string GetTypePayment()
        {
            return "Pix";
        }

        public bool TypePaymentValid(Payment typePayment)
        {
            return Payment.Pix == typePayment;
        }
    }
}
