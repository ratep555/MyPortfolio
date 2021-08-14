using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
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
    public class AnnualReviewsController : BaseApiController
    {
        private readonly IAnnualReviewService _annualReviewService;
        private readonly IMapper _mapper;
        public AnnualReviewsController(IAnnualReviewService annualReviewService, IMapper mapper)
        {
            _annualReviewService = annualReviewService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<AnnualTaxLiabilityDto>> GetAnnual()
        {
            var email = User.RetrieveEmailFromPrincipal();

            var annual = await _annualReviewService.ShowAnnual(email);

            return Ok(annual);
        }

        [HttpGet("charts")]
        public async Task<ActionResult> ShowChartWithProfAndLoss()
        {
            var email = User.RetrieveEmailFromPrincipal();

            var list = await _annualReviewService.ShowListOfAnnualProfitAndLoss(email);

            if (list.Count() > 0) return Ok(new { list });

            return BadRequest();
        }

        [HttpGet("list")]
        public async Task<ActionResult<Pagination<AnnualProfitOrLossDto>>> GetAllAnnualProfitAndLoss(
        [FromQuery] QueryParameters queryParameters)
        {
            var annuals = await _annualReviewService.GetAnnualProfitOrLossWithSearching(queryParameters);
            var list = await _annualReviewService.GetAnnualProfitOrLossWithPaging(queryParameters);

            var data = _mapper.Map<IEnumerable<AnnualProfitOrLossDto>>(list);

            return Ok(new Pagination<AnnualProfitOrLossDto>
            (queryParameters.Page, queryParameters.PageCount, annuals.Count(), data));
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<AnnualTaxLiabilityDto>> GetTaxLiability(int id)
        {
            var email = User.RetrieveEmailFromPrincipal();

            var taxliability = await _annualReviewService.ShowTaxLiability(email, id);

            return Ok(taxliability);
        }
    }
}