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
    public class TransferstockREsponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
    public class TransferTransactionOrchestator :IRequest<TransferstockREsponse>
    {
        public CreateTransactionDTO createTransactionDTO { get; set; }

    }
    public class TransferStockOrchestatorHandler : IRequestHandler<TransferTransactionOrchestator, TransferstockREsponse>
    {
        private readonly IMediator mediator;
        public TransferStockOrchestatorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<TransferstockREsponse> Handle(TransferTransactionOrchestator request, CancellationToken cancellationToken)
        {
            bool result = await mediator.Send(new TransferStockOrchestrator { createTransactionDTO = request.createTransactionDTO }, cancellationToken);
            if (result) result &= await mediator.Send(new SaveTranscaionCommand { createTransactionDTO = request.createTransactionDTO }, cancellationToken);
            return new TransferstockREsponse
            {
                IsSuccess = result,
            };
        }
    }
}
