using AutoMapper;
using InventoryManagementSystem.BLL.DTOs.Reports;
using InventoryManagementSystem.BLL.Filters;
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



        public List<IFilter>GetFilters()
        {
            var filters = new List<IFilter>();
            if (ProductId != null)
            {
                filters.Add(new ProductFilter(ProductId.Value));
            }
            if (FromDate != null)
            {
                filters.Add(new FromDateFilter(FromDate.Value));
            }
            if (ToDate != null)
            {
                filters.Add(new ToDateFilter(ToDate.Value));
            }
            if (ProductCategoryId != null)
            {
                filters.Add(new CategoryFilter(ProductCategoryId.Value));
            }
            if (TransactionType != null)
            {
                filters.Add(new TranscationTypeFilter(TransactionType.Value));
            }
            return filters;
        }





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
            var TransacarionsResult = await unitOfWork.InventoryTransaction.ReadAllAsync(cancellationToken,"Product");

            List<IFilter> filters = request.GetFilters();
            if (filters.Count > 0)
            {
                foreach (var filter in filters)
                {
                    
                    TransacarionsResult = TransacarionsResult.Where(filter.GetExpression());

                }
            }
            var transactionResponse = TransacarionsResult.Select(x => new TransactionHistorResponse { 
            Quantity = x.Quantity,
                TransactionDate = x.TransactionDate,
                TransactionType=x.TransactionType,
                ProductId=x.ProductId,
                FromWarehouseId=x.FromWarehouseId,
                ToWarehouseId=x.ToWarehouseId,
            });
            return transactionResponse;
              //  return mapper.Map<IEnumerable<TransactionHistorResponse>>(TransacarionsResult);

        }
    }
}
