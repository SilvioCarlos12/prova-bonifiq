using ProvaPub.Dtos;
using ProvaPub.Models;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
	public class ProductService: IProductService
    {

		private readonly IProductRepository _produtoRepository;

        public ProductService(IProductRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<Pagination<Product>> ListProducts(int page, CancellationToken cancellationToken)
		{

			var countProducts = await _produtoRepository.TotalProduct(cancellationToken);

			var produtos = await _produtoRepository.GetProductsPagination(page, cancellationToken);

		    return new Pagination<Product>(produtos,countProducts, page);
		}

	}
}
