using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<Pagination<TypeOfStockDto>>> GetAllTypesOfStock(
        [FromQuery] QueryParameters queryParameters)
        {
            var typesOfStock = await _typeOfStockService.GetTypesOfStockWithSearching(queryParameters);
            var list = await _typeOfStockService.GetTypesOfStockWithPaging(queryParameters);

            var data = _mapper.Map<IEnumerable<TypeOfStockDto>>(list);

            return Ok(new Pagination<TypeOfStockDto>
            (queryParameters.Page, queryParameters.PageCount, typesOfStock.Count(), data)); 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TypeOfStockDto>> GetTypeOfStockById(int id)
        {
            var typeOfStock = await _typeOfStockService.GetTypeOfStockByIdAsync(id);

            if (typeOfStock == null) return NotFound();

            return _mapper.Map<TypeOfStockDto>(typeOfStock);
        }

        [HttpPost]
        public async Task<ActionResult<TypeOfStockDto>> CreateTypeOfStock([FromBody] TypeOfStockDto typeOfStockDTO)
        {
            var typeOfStock = _mapper.Map<TypeOfStock>(typeOfStockDTO);

            await _typeOfStockService.CreateTypeOfStock(typeOfStock);

            return _mapper.Map<TypeOfStockDto>(typeOfStock);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TypeOfStockDto>> UpdateTypeOfStock(int id, [FromBody] TypeOfStockDto typeOfStockDto)
        {
            var typeOfStock = _mapper.Map<TypeOfStock>(typeOfStockDto);

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






