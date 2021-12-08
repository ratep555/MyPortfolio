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
        public async Task<ActionResult<Pagination<SegmentDto>>> GetAllSegments(
            [FromQuery] QueryParameters queryParameters)
        {
            var count = await _segmentService.GetCountForSegments();

            var list = await _segmentService.GetSegmentsWithSearchingAndPaging(queryParameters);

            var data = _mapper.Map<IEnumerable<SegmentDto>>(list);

            return Ok(new Pagination<SegmentDto>
                (queryParameters.Page, queryParameters.PageCount, count, data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SegmentDto>> GetSegmentById(int id)
        {
            var segment = await _segmentService.GetSegmentByIdAsync(id);

            if (segment == null) return NotFound(new ServerResponse(404));

            return _mapper.Map<SegmentDto>(segment);
        }

        [HttpPost]
        public async Task<ActionResult<SegmentDto>> CreateSegment([FromBody] SegmentDto segmentDTO)
        {
            var segment = _mapper.Map<Segment>(segmentDTO);

            await _segmentService.CreateSegment(segment);

            return CreatedAtAction("GetSegmentById", new {id = segment.Id }, 
                _mapper.Map<SegmentDto>(segment));        
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSegment(int id, [FromBody] SegmentDto segmentDto)
        {
            var segment = _mapper.Map<Segment>(segmentDto);

            if (id != segment.Id) return BadRequest(new ServerResponse(400));

            await _segmentService.UpdateSegment(segment);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSegment(int id)
        {
            var segment = await _segmentService.GetSegmentByIdAsync(id);

            if (segment == null) return NotFound(new ServerResponse(404));

            await _segmentService.DeleteSegment(segment);

            return NoContent();
        }
    }
}







