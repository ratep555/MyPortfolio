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
    public class SurtaxesController : BaseApiController
    {
        private readonly ISurtaxService _surtaxService;
        private readonly IMapper _mapper;
        public SurtaxesController(ISurtaxService surtaxService, IMapper mapper)
        {
            _mapper = mapper;
            _surtaxService = surtaxService;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<SurtaxDto>>> GetAllSurtaxes(
        [FromQuery] QueryParameters queryParameters)
        {
            var surtaxes = await _surtaxService.GetSurtaxesWithSearching(queryParameters);
            var list = await _surtaxService.GetSurtaxesWithPaging(queryParameters);

            var data = _mapper.Map<IEnumerable<SurtaxDto>>(list);

            return Ok(new Pagination<SurtaxDto>
            (queryParameters.Page, queryParameters.PageCount, surtaxes.Count(), data));
        }

        [HttpGet("{id}")]       
        public async Task<ActionResult<SurtaxDto>> GetSurtaxById(int id)
        {
            var surtax = await _surtaxService.GetSurtaxByIdAsync(id);

            if (surtax == null) return NotFound();

            return _mapper.Map<SurtaxDto>(surtax);
        }

        [HttpPost]
        public async Task<ActionResult<SurtaxDto>> CreateSegment([FromBody] SurtaxDto surtaxDTO)
        {
            var surtax = _mapper.Map<Surtax>(surtaxDTO);

            await _surtaxService.CreateSurtax(surtax);

            return _mapper.Map<SurtaxDto>(surtax);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SurtaxDto>> UpdateSurtax(int id, [FromBody] SurtaxDto surtaxDto)
        {
            var surtax = _mapper.Map<Surtax>(surtaxDto);

            if (id != surtax.Id) return BadRequest();

            await _surtaxService.UpdateSurtax(surtax);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSurtax(int id)
        {
            var surtax = await _surtaxService.GetSurtaxByIdAsync(id);

            if (surtax == null) return NotFound();

            await _surtaxService.DeleteSurtax(surtax);

            return NoContent();
        }
    }
}