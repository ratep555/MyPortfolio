using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Paging;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
        public async Task<ActionResult<Pagination<CategoryToReturnDto>>> GetAllCategories(
        [FromQuery] QueryParameters queryParameters)
        {
            var list = await _categoryService.GetCategoriesAsync(queryParameters);

            var data = _mapper.Map<IEnumerable<CategoryToReturnDto>>(list);

            return Ok(new Pagination<CategoryToReturnDto>
            (queryParameters.Page, queryParameters.PageCount, list.Count(), data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryToReturnDto>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null) return NotFound();

            return _mapper.Map<CategoryToReturnDto>(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryToCreateDto>> CreateCategory([FromBody] CategoryToCreateDto categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);

            await _categoryService.CreateCategory(category);

            return _mapper.Map<CategoryToCreateDto>(category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryToEditDto>> UpdateCategory(int id, [FromBody] CategoryToEditDto categoryToEditDto)
        {
            var category = _mapper.Map<Category>(categoryToEditDto);

            if (id != category.Id) return BadRequest();

            await _categoryService.UpdateCategory(category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null) return NotFound();

            await _categoryService.DeleteCategory(category);

            return NoContent();
        }

    }
}