using InventoryManagementSystem.BLL.CQRS.Commands.Transaction;
using InventoryManagementSystem.BLL.DTOs.InventoryTransaction;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Orchestrators.Transaction
{
    public class RemovestockREsponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
    public class RemoveTransactionOrcestartor : IRequest<RemovestockREsponse>
    {
        public CreateTransactionDTO createTransactionDTO { get; set; }

    }
    public class RemoveStockTransactionOrcestartorHandler : IRequestHandler<RemoveTransactionOrcestartor, RemovestockREsponse>
    {
        private readonly IMediator mediator;
        public RemoveStockTransactionOrcestartorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<RemovestockREsponse> Handle(RemoveTransactionOrcestartor request, CancellationToken cancellationToken)
        {
            bool result = await mediator.Send(new RemoveStockOrchestrator { createTransactionDTO = request.createTransactionDTO }, cancellationToken);
            if (result) result &= await mediator.Send(new SaveTranscaionCommand { createTransactionDTO = request.createTransactionDTO }, cancellationToken);
            return new RemovestockREsponse
            {
                IsSuccess = result,
            };
        }
    }
}
