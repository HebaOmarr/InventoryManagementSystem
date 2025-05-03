using InventoryManagementSystem.DAL.UnitOfWork;
using InventoryManagementSystem.Entities.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Commands.Transaction
{
    public class ArchiveTrancationCommand :IRequest
    {
        public InventoryTransaction inventoryTransaction { get; set; }
    }
    public class ArchiveTrancationCommandHandler : IRequestHandler<ArchiveTrancationCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public ArchiveTrancationCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task Handle(ArchiveTrancationCommand request, CancellationToken cancellationToken)
        {

            request.inventoryTransaction.IsArchived = true;
            return Task.CompletedTask;
        }
    }
}
