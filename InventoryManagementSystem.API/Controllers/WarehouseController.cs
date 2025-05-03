using InventoryManagementSystem.BLL.CQRS.Commands.Warehouses;
using InventoryManagementSystem.BLL.CQRS.Queries.Products;
using InventoryManagementSystem.BLL.CQRS.Queries.Warehouses;
using InventoryManagementSystem.BLL.DTOs.Warehouse;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InventoryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IMediator mediator;

        public WarehouseController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> AddWarehouse([FromBody] CreateWarehouseDTO warehousedto)
        {
            if (warehousedto == null)
            {
                return BadRequest("Warehouse data is null");
            }
          bool result= await mediator.Send(new AddWarehouseCommand(warehousedto));


            return result ? Ok("WarehouseProduct added successfully") :
               BadRequest("Failed to add warehouseProduct");
        }
        [HttpGet("GetAllWarehouse")]
        public async Task<IActionResult> Getall()
        {
            var resuilt= await mediator.Send(new GetallWarehouseQuery());
            if (resuilt == null)
            {
                return NotFound("No warehouse found");
            }
            return Ok(resuilt);
        }
    }
}
