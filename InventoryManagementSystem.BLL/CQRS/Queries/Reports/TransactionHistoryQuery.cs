using AutoMapper;
using InventoryManagementSystem.BLL.DTOs.Reports;
using InventoryManagementSystem.DAL.UnitOfWork;
using InventoryManagementSystem.Entities.Enums;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Queries.Reports
{
    public class TransactionHistorResponse
    {
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public int ProductId { get; set; }
        public int? FromWarehouseId { get; set; }
        public int? ToWarehouseId { get; set; }
    }
    public class TransactionHistoryQuery :IRequest<IEnumerable<TransactionHistorResponse>>
    {
       public int? ProductId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? ProductCategoryId { get; set; }
        public TransactionType? TransactionType { get; set; }

    }
    public class TransactionHistoryQueryHandler : IRequestHandler<TransactionHistoryQuery, IEnumerable<TransactionHistorResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TransactionHistoryQueryHandler( IUnitOfWork unitOfWork,IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<TransactionHistorResponse>?> Handle(TransactionHistoryQuery request, CancellationToken cancellationToken)
        {

          

                var Transacarions = await unitOfWork.InventoryTransaction.ReadAllAsync(cancellationToken, "Product");

                if (request.ProductId != null)
                {
                    Transacarions = Transacarions.Where(e => e.ProductId == request.ProductId);
                }
                if (request.TransactionType != null)
                {
                    Transacarions = Transacarions.Where(e => e.TransactionType == request.TransactionType);
                }
                if (request.FromDate != null)
                {
                    Transacarions = Transacarions.Where(e => e.TransactionDate >= request.FromDate);

                }
                if (request.ToDate != null)
                {
                    Transacarions = Transacarions.Where(e => e.TransactionDate <= request.ToDate);
                }
                if (request.ProductCategoryId != null)
                {
                    Transacarions = Transacarions.Where(e => e.Product.categoryId == request.ProductCategoryId);
                }
                return mapper.Map<IEnumerable<TransactionHistorResponse>>(Transacarions);
            
        }
    }
}
