namespace ProvaPub.Services.Interfaces
{
    public interface IRandomNumberService
    {
        Task<int> GetRandom(CancellationToken cancellationToken);
    }
}
