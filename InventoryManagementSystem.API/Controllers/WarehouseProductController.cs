using InventoryManagementSystem.BLL.CQRS.Commands.WarehouseProductss;
using InventoryManagementSystem.BLL.CQRS.Commands.Warehouses;
using InventoryManagementSystem.BLL.CQRS.Queries.WarehouseProducts;
using InventoryManagementSystem.BLL.CQRS.Queries.Warehouses;
using InventoryManagementSystem.BLL.DTOs.Warehouse;
using InventoryManagementSystem.BLL.DTOs.WarehouseProduct;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public WarehouseProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddWarehouse([FromBody] CreateWarehouseProductDTO warehousProducteDTO)
        {
            if (warehousProducteDTO == null)
            {
                return BadRequest("Warehouseproduct data is null");
            }
           bool result= await mediator.Send(new AddWarehouseProductCommand { WarehouseProductDTO=warehousProducteDTO });

             return result?  Ok("Warehouse Product added successfully") :
                BadRequest("Failed to add Warehouse Product");
        }
        [HttpGet("GetAllWarehouseProduct")]
        public async Task<IActionResult> Getall()
        {
            var resuilt = await mediator.Send(new GetallWarehouseProductsQuery());
            if (resuilt == null)
            {
                return NotFound("No Warehouse Product found");
            }
            return Ok(resuilt);
        }
    }
}
