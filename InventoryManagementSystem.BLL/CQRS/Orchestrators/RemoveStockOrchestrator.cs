using InventoryManagementSystem.BLL.CQRS.Commands.Products;
using InventoryManagementSystem.BLL.CQRS.Commands.WarehouseProductss;
using InventoryManagementSystem.BLL.DTOs.InventoryTransaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Orchestrators
{
    public class RemoveStockOrchestrator : IRequest<bool>
    {
        public CreateTransactionDTO createTransactionDTO { get; set; }
    }

    public class RemoveStockOrchestratorHandler : IRequestHandler<RemoveStockOrchestrator, bool>
    {
        private readonly IMediator mediator;
        public RemoveStockOrchestratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<bool> Handle(RemoveStockOrchestrator request, CancellationToken cancellationToken)
        {
            bool result = true;

            result &= await mediator.Send(new UpdateProductQuntityCommand
            {
                ProductId = request.createTransactionDTO.ProductId,
                Quantity = request.createTransactionDTO.Quantity,
                IsRemoveStock =true
            }, cancellationToken);

            result &= await mediator.Send(new UpdateWarehouseOrchestrator
            {
                WarehouseId = (int)request.createTransactionDTO.FromWarehouseId,
                ProductId = request.createTransactionDTO.ProductId,
                Quantity = request.createTransactionDTO.Quantity,
                  IsRemoveStock = true
            },cancellationToken);

            return result;

        }
    }


}
