using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AvtamWebApi2024Context productContext;

        public ProductRepository(AvtamWebApi2024Context productContext)
        {
            this.productContext = productContext;
        }

        public async Task<List<Product>> Get(string? description, int? minPrice, int? maxPrice, string? name, int?[] categoryIds, int position, int skip)
        {
            var query = productContext.Products
                .Include(p => p.Category)
                .Where(product =>
                    (name == null || product.Name.Contains(name)) &&
                    (description == null || product.Description.Contains(description)) &&
                    (minPrice == null || product.Price >= minPrice) &&
                    (maxPrice == null || product.Price <= maxPrice) &&
                    (!categoryIds.Any() || categoryIds.Contains((int)product.CategoryId)))
                .OrderBy(product => product.Price);

            List<Product> products = await query.Skip(skip).Take(position).ToListAsync();
            return products;
        }

        public async Task<List<Product>> Get()
        {
            return await productContext.Products.ToListAsync();
        }
    }
}