using InventoryManagementSystem.API.ViewModel;
using InventoryManagementSystem.BLL.CQRS.Commands;
using InventoryManagementSystem.BLL.CQRS.Commands.Products;
using InventoryManagementSystem.BLL.CQRS.Commands.Transaction;
using InventoryManagementSystem.BLL.CQRS.Orchestrators;
using InventoryManagementSystem.BLL.CQRS.Orchestrators.Transaction;
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
        //[HttpPost("SaveTransaction")]
        //public async Task<ApiResponseResult<bool>> CreateTransaction([FromBody] CreateTransactionDTO transactiondto,CancellationToken cancellationToken)
        //{
          
        //    bool result = await mediator.Send(new TransactionOrchestrator { createTransactionDTO =transactiondto},cancellationToken);
        //    if (result)
        //        return ApiResponseResult<bool>.Success(result, "Transacrion Saved Successfully");
        //    return ApiResponseResult<bool>.Error("Saving Transacrion Failed");


        //}


        [HttpPost("AddStockTransaction")]
        public async Task<ApiResponseResult<bool>> AddStockTransaction([FromBody] CreateTransactionDTO transactiondto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new AddTransactioOrchestator { createTransactionDTO = transactiondto }, cancellationToken);
            if (result.IsSuccess)
                return ApiResponseResult<bool>.Success(result.IsSuccess, "Add Stock Transaction Created Successfully");
            return ApiResponseResult<bool>.Error("Add Stock Transaction Creation Failed");
        }

        [HttpPost("RemoveStockTransaction")]
        public async Task<ApiResponseResult<bool>> RemoveStockTransaction([FromBody] CreateTransactionDTO transactiondto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new RemoveTransactionOrcestartor { createTransactionDTO = transactiondto }, cancellationToken);
            if (result.IsSuccess)
                return ApiResponseResult<bool>.Success(result.IsSuccess, "Remove Stock Transaction Created Successfully");
            return ApiResponseResult<bool>.Error("Remove Stock Transaction Creation Failed");
        }

        [HttpPost("TransferStockTransaction")]
        public async Task<ApiResponseResult<bool>> TransferStockTransaction([FromBody] CreateTransactionDTO transactiondto, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new TransferTransactionOrchestator { createTransactionDTO = transactiondto }, cancellationToken);
            if (result.IsSuccess)
                return ApiResponseResult<bool>.Success(result.IsSuccess, "Transfer Stock Transaction Created Successfully");
            return ApiResponseResult<bool>.Error("Transfer Stock Transaction Creation Failed");
        }


    }
}
