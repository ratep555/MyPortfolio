using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface
     ICategoryService
    {
        Task<IQueryable<Category>> GetCategoriesWithSearching(QueryParameters queryParameters);
        Task<IQueryable<Category>> GetCategoriesWithPaging(QueryParameters queryParameters);
        Task<Category> GetCategoryByIdAsync(int id);
        Task CreateCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(Category category);

    }
}