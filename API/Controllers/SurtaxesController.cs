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
            var count = await _surtaxService.GetCountForSurtaxes();

            var list = await _surtaxService.GetSurtaxesWithSearchingAndPaging(queryParameters);

            var data = _mapper.Map<IEnumerable<SurtaxDto>>(list);

            return Ok(new Pagination<SurtaxDto>
            (queryParameters.Page, queryParameters.PageCount, count, data));
        }
        
        [AllowAnonymous]
        [HttpGet("getsurtaxes")]
        public async Task<ActionResult<IEnumerable<SurtaxDto>>> GetSurtaxesForTypeahead()
        {
            var list = await _surtaxService.ListAllSurtaxes();

            var surtaxes = _mapper.Map<IEnumerable<SurtaxDto>>(list);

            return Ok(surtaxes);
        }

        [HttpGet("{id}")]       
        public async Task<ActionResult<SurtaxDto>> GetSurtaxById(int id)
        {
            var surtax = await _surtaxService.GetSurtaxByIdAsync(id);

            if (surtax == null) return NotFound(new ServerResponse(404));

            return _mapper.Map<SurtaxDto>(surtax);
        }

        [HttpPost]
        public async Task<ActionResult<SurtaxDto>> CreateSurtax([FromBody] SurtaxDto surtaxDTO)
        {
            var surtax = _mapper.Map<Surtax>(surtaxDTO);

            await _surtaxService.CreateSurtax(surtax);

            return CreatedAtAction("GetSurtaxById", new {id = surtax.Id }, 
                _mapper.Map<SurtaxDto>(surtax));    
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSurtax(int id, [FromBody] SurtaxDto surtaxDto)
        {
            var surtax = _mapper.Map<Surtax>(surtaxDto);

            if (id != surtax.Id) return BadRequest(new ServerResponse(400));

            await _surtaxService.UpdateSurtax(surtax);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSurtax(int id)
        {
            var surtax = await _surtaxService.GetSurtaxByIdAsync(id);

            if (surtax == null) return NotFound(new ServerResponse(404));

            await _surtaxService.DeleteSurtax(surtax);

            return NoContent();
        }
    }
}