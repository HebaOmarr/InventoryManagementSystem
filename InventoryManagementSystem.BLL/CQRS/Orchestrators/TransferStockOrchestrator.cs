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
    public class TransferStockOrchestrator :IRequest<bool>
    {
        public CreateTransactionDTO createTransactionDTO { get; set; }
    }
    public class TransferStockOrchestratorHandler : IRequestHandler<TransferStockOrchestrator, bool>
    {
        private readonly IMediator mediator;
        public TransferStockOrchestratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<bool> Handle(TransferStockOrchestrator request, CancellationToken cancellationToken)
        {



            bool result = await mediator.Send(new UpdateWarehouseOrchestrator
            {
                WarehouseId = (int)request.createTransactionDTO.FromWarehouseId,
                ProductId = request.createTransactionDTO.ProductId,
                Quantity = request.createTransactionDTO.Quantity,
                  IsRemoveStock = true

            }, cancellationToken);
            if(!result) return false;
            await mediator.Send(new UpdateWarehouseOrchestrator
            {
                WarehouseId = (int)request.createTransactionDTO.ToWarehouseId,
                ProductId = request.createTransactionDTO.ProductId,
                Quantity = request.createTransactionDTO.Quantity,
                IsRemoveStock = false
            }, cancellationToken);

            return result;

        }
    }

}
