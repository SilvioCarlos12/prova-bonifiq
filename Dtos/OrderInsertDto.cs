using ProvaPub.Enums;

namespace ProvaPub.Dtos
{
    public record OrderInsertDto(Payment PaymentMethod, decimal PaymentValue, int CustomerId)
    {
    }
}
