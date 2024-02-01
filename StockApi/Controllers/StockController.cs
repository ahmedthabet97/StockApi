using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApi.Models;
using StockApi.Services;

namespace StockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public StockController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStockAsync() 
        {
         var result= await unitOfWork.StockRepository.GetAllAsync();
            
            return Ok(result);
        }
        
    }
}
