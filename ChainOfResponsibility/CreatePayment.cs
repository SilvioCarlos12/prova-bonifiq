using ProvaPub.Enums;
using ProvaPub.Strategy;
using ProvaPub.Strategy.Interfaces;

namespace ProvaPub.ChainOfResponsibility
{
    public class CreatePayment: ICreatePayment
    {
        private List<IPaymentStrategy> _paymentStrategies;

       public CreatePayment()
        {
            _paymentStrategies = new List<IPaymentStrategy>();
            AddPayments();
        }

        public IPaymentStrategy GetPaymentStrategy(Payment typePayment)
        {
            return _paymentStrategies.Single(x => x.TypePaymentValid(typePayment));
        }

        private void AddPayments()
        {
            _paymentStrategies.Add(new PaypalPayment());
            _paymentStrategies.Add(new CreditCardPayment());
            _paymentStrategies.Add(new PixPayment());
        }
    }
}
