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
    public class SegmentsController : BaseApiController
    {
        private readonly ISegmentService _segmentService;
        private readonly IMapper _mapper;
        public SegmentsController(ISegmentService segmentService, IMapper mapper)
        {
            _mapper = mapper;
            _segmentService = segmentService;

        }

        [HttpGet]
        public async Task<ActionResult<Pagination<SegmentToReturnDto>>> GetAllSegments(
        [FromQuery] QueryParameters queryParameters)
        {
            var list = await _segmentService.GetSegmentsAsync(queryParameters);

            var data = _mapper.Map<IEnumerable<SegmentToReturnDto>>(list);

            return Ok(new Pagination<SegmentToReturnDto>
            (queryParameters.Page, queryParameters.PageCount, list.Count(), data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SegmentToReturnDto>> GetSegmentById(int id)
        {
            var segment = await _segmentService.GetSegmentByIdAsync(id);

            if (segment == null) return NotFound();

            return _mapper.Map<SegmentToReturnDto>(segment);
        }

        [HttpPost]
        public async Task<ActionResult<SegmentToCreateDto>> CreateSegment([FromBody] SegmentToCreateDto segmentDTO)
        {
            var segment = _mapper.Map<Segment>(segmentDTO);

            await _segmentService.CreateSegment(segment);

            return _mapper.Map<SegmentToCreateDto>(segment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SegmentToEditDto>> UpdateSegment(int id, [FromBody] SegmentToEditDto segmentToEditDto)
        {
            var segment = _mapper.Map<Segment>(segmentToEditDto);

            if (id != segment.Id) return BadRequest();

            await _segmentService.UpdateSegment(segment);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSegment(int id)
        {
            var segment = await _segmentService.GetSegmentByIdAsync(id);

            if (segment == null) return NotFound();

            await _segmentService.DeleteSegment(segment);

            return NoContent();
        }

    }
}







