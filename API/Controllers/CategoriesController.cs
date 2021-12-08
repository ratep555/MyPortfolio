using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ErrorHandling;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<CategoryDto>>> GetAllCategories(
            [FromQuery] QueryParameters queryParameters)
        {
            var count = await _categoryService.GetCountForCategories();
            var list = await _categoryService.GetCategoriesWithSearchingAndPaging(queryParameters);

            var data = _mapper.Map<IEnumerable<CategoryDto>>(list);

            return Ok(new Pagination<CategoryDto>
            (queryParameters.Page, queryParameters.PageCount, count, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null) return NotFound(new ServerResponse(404));

            return _mapper.Map<CategoryDto>(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CategoryDto categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);

            await _categoryService.CreateCategory(category);

            return CreatedAtAction("GetCategoryById", new {id = category.Id }, 
                _mapper.Map<CategoryDto>(category));        
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            if (id != category.Id) return BadRequest(new ServerResponse(400));

            await _categoryService.UpdateCategory(category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null) return NotFound(new ServerResponse(404));

            await _categoryService.DeleteCategory(category);

            return NoContent();
        }

    }
}