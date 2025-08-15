using ProvaPub.Models;

namespace ProvaPub.Repository.Interfaces
{
    public interface IRandomNumberRepository
    {
        Task<bool> ExistNumber(int number, CancellationToken cancellationToken);
        Task Insert(RandomNumber randomNumber, CancellationToken cancellationToken);
    }
}
