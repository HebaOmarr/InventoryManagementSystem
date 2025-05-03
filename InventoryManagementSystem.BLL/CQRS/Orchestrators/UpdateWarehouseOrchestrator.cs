using InventoryManagementSystem.BLL.CQRS.Commands.WarehouseProductss;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Orchestrators
{
    public class UpdateWarehouseOrchestrator :IRequest<bool>
    {
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool IsRemoveStock { get; set; }
    }
    public class UpdateWarehouseOrchestratorHandler : IRequestHandler<UpdateWarehouseOrchestrator, bool>
    {
        private readonly IMediator mediator;
        public UpdateWarehouseOrchestratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<bool> Handle(UpdateWarehouseOrchestrator request, CancellationToken cancellationToken)
        {
            var isExit = await mediator.Send(new IsExitWarehouseProductCommand
            {
                WarehouseId = request.WarehouseId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                IsRemoveStock = request.IsRemoveStock
            }, cancellationToken);

            if (isExit == false) return false;

            var isUpdate = await mediator.Send(new UpdateWarehouseProductQuntityCommand
            {
                WarehouseId = request.WarehouseId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                IsRemoveStock = request.IsRemoveStock
            }, cancellationToken);
            return isUpdate;
        }
    }

}

