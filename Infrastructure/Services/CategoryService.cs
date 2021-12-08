using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Paging;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly PortfolioContext _context;
        public CategoryService(PortfolioContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesWithSearchingAndPaging(QueryParameters queryParameters)
        {
            IQueryable<Category> category = _context.Categories.AsQueryable()
                                            .OrderBy(x => x.CategoryName);
            
            if (queryParameters.HasQuery())
            {
                category = category
                .Where(t => t.CategoryName.Contains(queryParameters.Query));
            }

            category = category.Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                           .Take(queryParameters.PageCount);
            
            return await category.ToListAsync();        
        }

        public async Task<int> GetCountForCategories()
        {
            return await _context.Categories.CountAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories         
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();                    
        }

        public async Task UpdateCategory(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;        
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

    }
}