using InventoryManagementSystem.BLL.CQRS.Commands.Products;
using InventoryManagementSystem.BLL.CQRS.Commands.Transaction;
using InventoryManagementSystem.BLL.CQRS.Commands.WarehouseProductss;
using InventoryManagementSystem.BLL.CQRS.Commands.Warehouses;
using InventoryManagementSystem.BLL.DTOs.InventoryTransaction;
using InventoryManagementSystem.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Orchestrators
{
    public class TransactionOrchestrator :IRequest<bool>
    {
        public CreateTransactionDTO createTransactionDTO { get; set; }
       
    }
    public class TransactionOrchestratorHandler : IRequestHandler<TransactionOrchestrator,bool>
    {
        private readonly IMediator mediator;

        public TransactionOrchestratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public  async Task<bool> Handle(TransactionOrchestrator request, CancellationToken cancellationToken)
        {

            bool FinalResult = true;

            if (request.createTransactionDTO.TransactionType==TransactionType.AddStock )
            {
             FinalResult &= await mediator.Send(new AddStockOrchestrator { createTransactionDTO = request.createTransactionDTO }, cancellationToken);

            }
            else if(request.createTransactionDTO.TransactionType == TransactionType.RemoveStock)
            {
                FinalResult &= await mediator.Send(new RemoveStockOrchestrator { createTransactionDTO = request.createTransactionDTO }, cancellationToken);
            }
            else if (request.createTransactionDTO.TransactionType == TransactionType.TransferStock)
            {
                FinalResult &= await mediator.Send(new TransferStockOrchestrator { createTransactionDTO = request.createTransactionDTO }, cancellationToken);
            }
            else
            {
                return false;
                    
             }
            
            if(FinalResult) FinalResult &= await mediator.Send(new SaveTranscaionCommand { createTransactionDTO = request.createTransactionDTO }, cancellationToken);


            return FinalResult;
        }
    }
}
