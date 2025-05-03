using InventoryManagementSystem.DAL.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Queries
{
    public class TransactionArchivedQuery :IRequest<IEnumerable<InventoryTransaction>>
    {
    }
    public class TransactionArchivedQueryHandler : IRequestHandler<TransactionArchivedQuery, IEnumerable<InventoryTransaction>>
    {
        private readonly IUnitOfWork unitOfWork;
        public TransactionArchivedQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<InventoryTransaction>> Handle(TransactionArchivedQuery request, CancellationToken cancellationToken)
        {
            var transactions = await unitOfWork.InventoryTransaction.GetAllWithFilter(e=>e.TransactionDate.Date < DateTime.Now.AddYears(-1), cancellationToken);
            return transactions;
        }
    }
}
