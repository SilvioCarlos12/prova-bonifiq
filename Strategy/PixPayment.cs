using ProvaPub.Enums;
using ProvaPub.Strategy.Interfaces;

namespace ProvaPub.Strategy
{
    public class PixPayment : IPaymentStrategy
    {
        public string GetNameMethodPayment()
        {
            return "Pix";
        }

        public bool TypePaymentValid(Payment typePayment)
        {
            return Payment.Pix == typePayment;
        }
    }
}
