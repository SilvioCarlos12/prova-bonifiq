using Microsoft.EntityFrameworkCore;
using ProvaPub.Dtos;
using ProvaPub.Extensions;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services
{
	public class ProductService: IProductService
    {
		TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public Pagination<Product>  ListProducts(int page)
		{
			   
		        var countProducts = _ctx.Products.Count();

			    var produtos = _ctx.Products
				.PaginationQuery(page)
				.AsNoTracking()
				.OrderBy(x=> x.Id)
				.ToList();

				return new Pagination<Product>(produtos,countProducts, page);
		}

	}
}
