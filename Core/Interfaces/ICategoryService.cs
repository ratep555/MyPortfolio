using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface
     ICategoryService
    {
        Task<IQueryable<Category>> GetCategoriesAsync(QueryParameters queryParameters);
        Task<Category> GetCategoryByIdAsync(int id);
        Task CreateCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(Category category);

    }
}