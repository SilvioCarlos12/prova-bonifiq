using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class RandomService
	{
        TestDbContext _ctx;
		public RandomService()
        {
            var contextOptions = new DbContextOptionsBuilder<TestDbContext>()
    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Teste;Trusted_Connection=True;")
    .Options;

            _ctx = new TestDbContext(contextOptions);
        }
        public async Task<int> GetRandom()
		{
            var number =  new Random().Next(1000);
            var numberExist = _ctx.Numbers.Where(x => x.Number == number).Any();
            if (numberExist)
                return number;
            _ctx.Numbers.Add(new RandomNumber() { Number = number });
            _ctx.SaveChanges();
			return number;
		}

	}
}
