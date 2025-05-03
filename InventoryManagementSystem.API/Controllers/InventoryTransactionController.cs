using InventoryManagementSystem.API.ViewModel;
using InventoryManagementSystem.BLL.CQRS.Commands;
using InventoryManagementSystem.BLL.CQRS.Commands.Products;
using InventoryManagementSystem.BLL.CQRS.Commands.Transaction;
using InventoryManagementSystem.BLL.CQRS.Orchestrators;
using InventoryManagementSystem.BLL.DTOs.InventoryTransaction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryTransactionController : ControllerBase
    {
        private readonly IMediator mediator;

        public InventoryTransactionController(IMediator mediator)
        {
         this.mediator = mediator;
        }
        [HttpPost("SaveTransaction")]
        public async Task<ApiResponseResult<bool>> CreateTransaction([FromBody] CreateTransactionDTO transactiondto,CancellationToken cancellationToken)
        {
          
            bool result = await mediator.Send(new TransactionOrchestrator { createTransactionDTO =transactiondto},cancellationToken);
            if (result)
                return ApiResponseResult<bool>.Success(result, "Transacrion Saved Successfully");
            return ApiResponseResult<bool>.Error("Saving Transacrion Failed");


        }


    } 
}
