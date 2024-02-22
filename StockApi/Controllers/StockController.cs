using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<StockHub> _hubContext;
        public StockController(IUnitOfWork unitOfWork, IHubContext<StockHub> hubContext)
        {
            this.unitOfWork = unitOfWork;
            _hubContext = hubContext;
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
