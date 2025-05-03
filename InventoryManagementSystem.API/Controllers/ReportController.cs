using InventoryManagementSystem.API.ViewModel;
using InventoryManagementSystem.BLL.CQRS.Queries.Products;
using InventoryManagementSystem.BLL.CQRS.Queries.Reports;
using InventoryManagementSystem.BLL.DTOs.Reports;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace InventoryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        [Authorize]

    public class ReportController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReportController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        // Low Stock Report
        [EnableRateLimiting("ConcurrencyLimiter")]
        [HttpGet("low-stockReport")]
        public async Task<ApiResponseResult<IEnumerable<ProductStockREsponse>>> LowStockProducts(CancellationToken cancellationToken)
        {
   

                var result = await mediator.Send(new LowStockProductCommand(), cancellationToken);
                if (result == null)
                    return ApiResponseResult<IEnumerable<ProductStockREsponse>>.Error("No low Stock Product", 404);

                return ApiResponseResult<IEnumerable<ProductStockREsponse>>.Success(result);
            

        }

        [EnableRateLimiting("ConcurrencyLimiter")]
        [HttpGet("TransactionHistory")]
        public async Task<ApiResponseResult<IEnumerable<TransactionHistorResponse>>> TransactionHistory([FromQuery] TransactionHistoryQuery request , CancellationToken cancellationToken)
        {

            var result = await mediator.Send(request, cancellationToken);
            if (result == null)
                return ApiResponseResult<IEnumerable<TransactionHistorResponse>>.Error("No Transaction Found",404);

            return ApiResponseResult<IEnumerable<TransactionHistorResponse>>.Success(result);
        }



    }
}
