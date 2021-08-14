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
            var modalities = await _modalityService.GetModalitiesWithSearching(queryParameters);
            var list = await _modalityService.GetModalitiesWithPaging(queryParameters);

            var data = _mapper.Map<IEnumerable<ModalityDto>>(list);

            return Ok(new Pagination<ModalityDto>
            (queryParameters.Page, queryParameters.PageCount, modalities.Count(), data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModalityDto>> GetModalityById(int id)
        {
            var modality = await _modalityService.GetModalityByIdAsync(id);

            if (modality == null) return NotFound();

            return _mapper.Map<ModalityDto>(modality);
        }

        [HttpPost]
        public async Task<ActionResult<ModalityDto>> CreateModality([FromBody] ModalityDto modalityDTO)
        {
            var modality = _mapper.Map<Modality>(modalityDTO);

            await _modalityService.CreateModality(modality);

            return _mapper.Map<ModalityDto>(modality);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ModalityDto>> UpdateModality(int id, [FromBody] ModalityDto modalityDto)
        {
            var modality = _mapper.Map<Modality>(modalityDto);

            if (id != modality.Id) return BadRequest();

            await _modalityService.UpdateModality(modality);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteModality(int id)
        {
            var modality = await _modalityService.GetModalityByIdAsync(id);

            if (modality == null) return NotFound();

            await _modalityService.DeleteModality(modality);

            return NoContent();
        }

    }
}










