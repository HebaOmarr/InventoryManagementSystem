using InventoryManagementSystem.BLL.CQRS.Commands.Products;
using InventoryManagementSystem.BLL.CQRS.Commands.WarehouseProductss;
using InventoryManagementSystem.BLL.DTOs.InventoryTransaction;
using InventoryManagementSystem.Entities.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Orchestrators
{
    public class AddStockOrchestrator :IRequest<bool>
    {
        public CreateTransactionDTO createTransactionDTO { get; set; }
    }
    public class AddStockOrchestratorHandler : IRequestHandler<AddStockOrchestrator,bool>
    {
        private readonly IMediator mediator;
        public AddStockOrchestratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<bool> Handle(AddStockOrchestrator request, CancellationToken cancellationToken)
        {
            bool result = true;
            result &= await mediator.Send(new UpdateProductQuntityCommand
            {
                ProductId = request.createTransactionDTO.ProductId,
                Quantity = request.createTransactionDTO.Quantity,
                IsRemoveStock=false
            }, cancellationToken);

            result &= await mediator.Send(new UpdateWarehouseOrchestrator
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
