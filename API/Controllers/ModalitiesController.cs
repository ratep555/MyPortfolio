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
    public class ModalitiesController : BaseApiController
    {
        private readonly IModalityService _modalityService;
        private readonly IMapper _mapper;
        public ModalitiesController(IModalityService modalityService, IMapper mapper)
        {
            _mapper = mapper;
            _modalityService = modalityService;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ModalityDto>>> GetAllModalities(
            [FromQuery] QueryParameters queryParameters)
        {
            var count = await _modalityService.GetCountForModalities();
            
            var list = await _modalityService.GetModalitiesWithSearchingAndPaging(queryParameters);

            var data = _mapper.Map<IEnumerable<ModalityDto>>(list);

            return Ok(new Pagination<ModalityDto>
                (queryParameters.Page, queryParameters.PageCount, count, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModalityDto>> GetModalityById(int id)
        {
            var modality = await _modalityService.GetModalityByIdAsync(id);

            if (modality == null) return NotFound(new ServerResponse(404));

            return _mapper.Map<ModalityDto>(modality);
        }

        [HttpPost]
        public async Task<ActionResult<ModalityDto>> CreateModality([FromBody] ModalityDto modalityDTO)
        {
            var modality = _mapper.Map<Modality>(modalityDTO);

            await _modalityService.CreateModality(modality);

            return CreatedAtAction("GetModalityById", new {id = modality.Id }, 
                _mapper.Map<ModalityDto>(modality));             
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateModality(int id, [FromBody] ModalityDto modalityDto)
        {
            var modality = _mapper.Map<Modality>(modalityDto);

            if (id != modality.Id) return BadRequest(new ServerResponse(400));

            await _modalityService.UpdateModality(modality);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteModality(int id)
        {
            var modality = await _modalityService.GetModalityByIdAsync(id);

            if (modality == null) return NotFound(new ServerResponse(404));

            await _modalityService.DeleteModality(modality);

            return NoContent();
        }
    }
}










