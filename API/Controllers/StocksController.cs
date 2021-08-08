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
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StocksController : BaseApiController
    {
        private readonly IStockService _stockService;
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;

        public StocksController(
        IStockService stockService, 
        ITransactionService transactionService,
        IMapper mapper,
        IHttpClientFactory clientFactory)
        {
            _stockService = stockService;
            _transactionService = transactionService;
            _mapper = mapper;
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<StockToReturnDto>>> GetAllStocks(
        [FromQuery] QueryParameters queryParameters)
        {
            var stocks = await _stockService.GetStocksWithSearching(queryParameters);
            var list = await _stockService.GetStocksWithPaging(queryParameters);

            var data = _mapper.Map<IEnumerable<StockToReturnDto>>(list);

            return Ok(new Pagination<StockToReturnDto>
            (queryParameters.Page, queryParameters.PageCount, stocks.Count(), data));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StockToReturnDto>> GetStockById(int id)
        {
            var email = User.RetrieveEmailFromPrincipal();

            var stock = await _stockService.GetStockByIdAsync(id);

            if (stock == null) return NotFound(new ServerResponse(404));

            var stockToReturn = _mapper.Map<StockToReturnDto>(stock);
            stockToReturn.TotalQuantity = await _transactionService.TotalQuantity(email, id);

            return Ok(stockToReturn);
        }

        [HttpGet("first/{id}")]
        public async Task<ActionResult<StockDto>> GetStockByIdForEditing(int id)
        {
            var stock = await _stockService.GetStockByIdAsync(id);

            if (stock == null) return NotFound();

            return _mapper.Map<StockDto>(stock);
        }

        [HttpPost]
        public async Task<ActionResult<StockDto>> CreateStock([FromBody] StockDto stockDTO)
        {
            var stock = _mapper.Map<Stock>(stockDTO);

            await _stockService.CreateStock(stock);

            return _mapper.Map<StockDto>(stock);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StockDto>> UpdateStock(int id, [FromBody] StockDto stockDto)
        {
            var stock = _mapper.Map<Stock>(stockDto);

            if (id != stock.Id) return BadRequest();

            await _stockService.UpdateStock(stock);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStock(int id)
        {
            var stock = await _stockService.GetStockByIdAsync(id);

            if (stock == null) return NotFound();

            await _stockService.DeleteStock(stock);

            return NoContent();
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var list = await _stockService.GetCategories();

            return Ok(list);
        }

        [HttpGet("modalities")]
        public async Task<ActionResult<IEnumerable<Modality>>> GetModalities()
        {
            var list = await _stockService.GetModalities();

            return Ok(list);
        }

        [HttpGet("segments")]
        public async Task<ActionResult<IEnumerable<Segment>>> GetSegments()
        {
            var list = await _stockService.GetSegments();

            return Ok(list);
        }

        [HttpGet("typesofstock")]
        public async Task<ActionResult<IEnumerable<Modality>>> GetTypesOfStock()
        {
            var list = await _stockService.GetTypesOfStock();

            return Ok(list);
        }

        [HttpPut("refresh")]
        public async Task<ActionResult<IEnumerable<Stock>>> RefreshPrices()
        {
            StockDataModelDto stockData = new StockDataModelDto();

            var request = new HttpRequestMessage(HttpMethod.Get,
           "https://rest.zse.hr/web/Bvt9fe2peQ7pwpyYqODM/price-list/XZAG/2021-07-27/json"); 

            var client = _clientFactory.CreateClient();

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                stockData = await response.Content.ReadFromJsonAsync<StockDataModelDto>();

                    foreach (var subitem in stockData.securities)
                    {
                        await _stockService
                        .RefreshPrices(subitem.symbol, Convert.ToDecimal(subitem.close_price));
                    }             
            }           
            return Ok(stockData);
        }  
 
    }
}




