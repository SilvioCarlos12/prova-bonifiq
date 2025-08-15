using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
	public class RandomService:IRandomNumberService
	{
        private readonly IRandomNumberRepository _randomNumberRepository;
        public RandomService(IRandomNumberRepository randomNumberRepository)
        {
            _randomNumberRepository = randomNumberRepository;
        }

        public async Task<int> GetRandom(CancellationToken cancellationToken)
		{
            var number =  new Random().Next(100);

            var numberExist = await _randomNumberRepository.ExistNumber(number, cancellationToken);

            if (numberExist)
                return number;

            await _randomNumberRepository.Insert(new RandomNumber() { Number = number }, cancellationToken);

			return number;
		}

	}
}
