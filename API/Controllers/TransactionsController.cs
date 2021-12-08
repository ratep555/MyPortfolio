using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using API.ErrorHandling;
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
    public class TransactionsController : BaseApiController
    {
        private readonly ITransactionService _transactionService;
        private readonly IAnnualReviewService _annualReviewService;
        private readonly IMapper _mapper;
        public TransactionsController(
        ITransactionService transactionService, 
        IAnnualReviewService annualReviewService,
        IMapper mapper)
        {
            _transactionService = transactionService;
            _annualReviewService = annualReviewService;
            _mapper = mapper;
        }

        [HttpPost("buy/{id}")]
        public async Task<ActionResult<TransactionDto>> BuyStock(int id, TransactionDto transactionDto)
        {     
            var email = User.RetrieveEmailFromPrincipal();

            await _annualReviewService.ActionsRegardingProfitOrLossCardUponPurchase(email);

            var transaction = await _transactionService.BuyStockAsync(transactionDto, id, email);                                                                                                

            var transactionToReturn = _mapper.Map<TransactionDto>(transaction);

            return Ok(transactionToReturn);
        }

        [HttpPost("sell/{id}")]
        public async Task<ActionResult<TransactionDto>> SellStock(int id, TransactionDto transactionDto)
        {     
            var email = User.RetrieveEmailFromPrincipal();

            if (await _transactionService.TotalQuantity(email, id) < transactionDto.Quantity)
            {
                return BadRequest("You are selling more than you have!");
            }

            var transaction = await _transactionService.SellStockAsync(transactionDto, id, email);

            await _annualReviewService.TwoYearException(email, transaction);

            await _transactionService.UpdateResolved(id, email);    

            await _annualReviewService.ActionsRegardingProfitOrLossCardUponSelling(email);

            var transactionToReturn = _mapper.Map<TransactionDto>(transaction);

            return Ok(transactionToReturn);
        }

        [HttpGet("portfolio")]
        public async Task<ActionResult<ClientPortfolioWithProfitOrLossDto>> GetProfitAndTraffic(
            [FromQuery]QueryParameters queryParameters)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var list = await _transactionService.ClientPortfolioWithProfitOrLoss(queryParameters, email);

            return Ok(list);
        }

        [HttpGet("listoftransactions")]
        public async Task<ActionResult<TransactionsForUserWithProfitAndTrafficDto>> GetTransactionsWithProfitAndTraffic(
            [FromQuery]QueryParameters queryParameters)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var list = await _transactionService.ShowTransactionsWithProfitAndTraffic(queryParameters, email);

            return Ok(list);
        }

        [HttpGet("charts")]
        public async Task<ActionResult> ShowGraphForClientPortfolio()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var list = await _transactionService.GetChartForClientPortfolio(email);

            if (list.Count() > 0) return Ok(new { list });

            return BadRequest(new ServerResponse(400));        
        }

        [HttpGet("countcharts")]
        public async Task<ActionResult<int>> ShowCountForClientPortfolio()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            int count = await _transactionService.GetCountForChart(email);

            return Ok(count);        
        }
    }
}







