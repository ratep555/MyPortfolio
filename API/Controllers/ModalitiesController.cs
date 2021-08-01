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
        public async Task<ActionResult<Pagination<ModalityToReturnDto>>> GetAllModalities(
        [FromQuery] QueryParameters queryParameters)
        {
            var list = await _modalityService.GetModalitiesAsync(queryParameters);

            var data = _mapper.Map<IEnumerable<ModalityToReturnDto>>(list);

            return Ok(new Pagination<ModalityToReturnDto>
            (queryParameters.Page, queryParameters.PageCount, list.Count(), data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModalityToReturnDto>> GetModalityById(int id)
        {
            var modality = await _modalityService.GetModalityByIdAsync(id);

            if (modality == null) return NotFound();

            return _mapper.Map<ModalityToReturnDto>(modality);
        }

        [HttpPost]
        public async Task<ActionResult<ModalityToCreateDto>> CreateModality([FromBody] ModalityToCreateDto modalityDTO)
        {
            var modality = _mapper.Map<Modality>(modalityDTO);

            await _modalityService.CreateModality(modality);

            return _mapper.Map<ModalityToCreateDto>(modality);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ModalityToEditDto>> UpdateModality(int id, [FromBody] ModalityToEditDto modalityToEditDto)
        {
            var modality = _mapper.Map<Modality>(modalityToEditDto);

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










