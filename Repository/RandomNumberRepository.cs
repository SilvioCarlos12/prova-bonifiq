using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;

namespace ProvaPub.Repository
{
    public class RandomNumberRepository : IRandomNumberRepository
    {
        private readonly TestDbContext _context;

        public RandomNumberRepository(TestDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistNumber(int number, CancellationToken cancellationToken)
        {
             return await _context.Numbers.AnyAsync(x => x.Number == number,cancellationToken);
        }

        public async Task Insert(RandomNumber randomNumber, CancellationToken cancellationToken)
        {
            _context.Numbers.Add(randomNumber);
           await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
