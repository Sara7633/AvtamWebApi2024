using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AvtamWebApi2024Context categoryContext;

        public CategoryRepository(AvtamWebApi2024Context categoryContext)
        {
            this.categoryContext = categoryContext;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await categoryContext.Categories.ToListAsync();
        }
    }
}