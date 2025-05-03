using InventoryManagementSystem.BLL.CQRS.Commands.Transaction;
using InventoryManagementSystem.BLL.DTOs.InventoryTransaction;
using InventoryManagementSystem.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Orchestrators.Transaction
{
    public class AddstockREsponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
    public class AddTransactioOrchestator :IRequest<AddstockREsponse>
    {
      public  CreateTransactionDTO createTransactionDTO { get; set; }
    }
    public class AddStockTransactioOrchestatorHandler : IRequestHandler<AddTransactioOrchestator, AddstockREsponse>
    {
        private readonly IMediator mediator;

        public AddStockTransactioOrchestatorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<AddstockREsponse> Handle(AddTransactioOrchestator request, CancellationToken cancellationToken)
        {

           bool result= await mediator.Send(new AddStockOrchestrator { createTransactionDTO = request.createTransactionDTO }, cancellationToken);
            if (result) result&=await mediator.Send(new SaveTranscaionCommand { createTransactionDTO = request.createTransactionDTO }, cancellationToken);
            return new AddstockREsponse
            {
                IsSuccess = result,
            };

        }
    }
}
