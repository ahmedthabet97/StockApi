using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApi.Enums;
using StockApi.Models;
using StockApi.Services;
using StockApi.ViewModels;
using System.Runtime.CompilerServices;

namespace StockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrderAsync() 
        {
            var orders = await unitOfWork.OrderRepository.GetAllAsync("Stocks");
         
            return Ok(orders);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("There is an Error");
            }
         
            var order = new Order
            {
                Quantity = model.Quantity,
                Stocks =new List<Stock>(),// { await unitOfWork.StockRepository.GetAsync(model.StockSymbol) },
                Type = model.OrderType,

            };
            foreach(var stockSymbol in model.StockSymbol)
            {
                var stock = await unitOfWork.StockRepository.GetAsync(stockSymbol);
                if(stock != null)
                {
                    order.Stocks.Add(stock);
                }
            }
            await unitOfWork.OrderRepository.CreateAsync(order);
           await unitOfWork.SaveChangesAsync();
            return Ok(order);
        }
             
    }
}
