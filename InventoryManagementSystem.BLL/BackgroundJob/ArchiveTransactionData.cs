using InventoryManagementSystem.BLL.CQRS.Commands.Transaction;
using InventoryManagementSystem.BLL.CQRS.Queries;
using InventoryManagementSystem.BLL.CQRS.Queries.Reports;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.BackgroundJob
{
    public class ArchiveTransactionData
    {
        private readonly IMediator mediator;

        public ArchiveTransactionData(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task RunTask()
        {
            var result = await mediator.Send(new TransactionArchivedQuery());
            if (result != null && result.Any())
            {
                foreach (var transaction in result)
                {
                    var command = new ArchiveTrancationCommand
                    {
                        inventoryTransaction = transaction
                    };
                    await mediator.Send(command);
                }
            }
        }
    }
}
