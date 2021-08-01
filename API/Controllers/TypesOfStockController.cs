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
    public class TypesOfStockController : BaseApiController
    {
        private readonly ITypeOfStockService _typeOfStockService;
        private readonly IMapper _mapper;
        public TypesOfStockController(ITypeOfStockService typeOfStockService, IMapper mapper)
        {
            _typeOfStockService = typeOfStockService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<TypeOfStockToReturnDto>>> GetAllTypesOfStock(
        [FromQuery] QueryParameters queryParameters)
        {
            var list = await _typeOfStockService.GetTypesOfStockAsync(queryParameters);

            var data = _mapper.Map<IEnumerable<TypeOfStockToReturnDto>>(list);

            return Ok(new Pagination<TypeOfStockToReturnDto>
            (queryParameters.Page, queryParameters.PageCount, list.Count(), data)); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TypeOfStockToReturnDto>> GetTypeOfStockById(int id)
        {
            var typeOfStock = await _typeOfStockService.GetTypeOfStockByIdAsync(id);

            if (typeOfStock == null) return NotFound();

            return _mapper.Map<TypeOfStockToReturnDto>(typeOfStock);
        }

        [HttpPost]
        public async Task<ActionResult<TypeOfStockToCreateDto>> CreateTypeOfStock([FromBody] TypeOfStockToCreateDto typeOfStockDTO)
        {
            var typeOfStock = _mapper.Map<TypeOfStock>(typeOfStockDTO);

            await _typeOfStockService.CreateTypeOfStock(typeOfStock);

            return _mapper.Map<TypeOfStockToCreateDto>(typeOfStock);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TypeOfStockToEditDto>> UpdateTypeOfStock(int id, [FromBody] TypeOfStockToEditDto typeOfStockToEditDto)
        {
            var typeOfStock = _mapper.Map<TypeOfStock>(typeOfStockToEditDto);

            if (id != typeOfStock.Id) return BadRequest();

            await _typeOfStockService.UpdateTypeOfStock(typeOfStock);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTypeOfStock(int id)
        {
            var typeOfStock = await _typeOfStockService.GetTypeOfStockByIdAsync(id);

            if (typeOfStock == null) return NotFound();

            await _typeOfStockService.DeleteTypeOfStock(typeOfStock);

            return NoContent();
        }
    }
}






