namespace ProvaPub.Models
{
	public class Order
	{
		public int Id { get; set; }
		public decimal Value { get; set; }
		public int CustomerId { get; set; }
		public DateTime OrderDate { get; set; }
		public string MethodPayment { get; set; }	
		public Customer Customer { get; set; }

		public Order(decimal value, int customerId,string methodPayment)
		{
			OrderDate = DateTime.UtcNow;
			CustomerId = customerId;
			Value = value;
			MethodPayment = methodPayment;
        }


    }
}
