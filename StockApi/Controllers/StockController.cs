using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApi.Models;
using StockApi.Services;

namespace StockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StockController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public StockController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet("GetAllStocks")]
        public async Task<IActionResult> GetAllStockAsync() 
        {
           var result= await unitOfWork.StockRepository.GetAllAsync();
            string json = JsonConvert.SerializeObject(result, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(new JsonResult(json));
        }
        [HttpGet("{symbol}/history")]
        public async Task<IActionResult> GetHistoricalStock(string symbol)
        {
            var HistoricalStock = await unitOfWork.StockRepository.GetAsync(symbol);
            if(HistoricalStock == null) 
            {
                return Ok($"There is Not Stock by this Symbol{symbol}");
            }
            return Ok(HistoricalStock);
        }
    }
}
