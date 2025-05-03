using InventoryManagementSystem.DAL.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Queries.Products
{
    public class GetAllProductLessLowStockThresholdQuery : IRequest<IEnumerable<Product>>
    {
    }
    public class GetAllProductLessLowStockThresholdQueryHandler : IRequestHandler<GetAllProductLessLowStockThresholdQuery, IEnumerable<Product>>
    {
        private readonly IUnitOfWork unitOfWork;
        public GetAllProductLessLowStockThresholdQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Product>> Handle(GetAllProductLessLowStockThresholdQuery request, CancellationToken cancellationToken)
        {
            var products = await unitOfWork.Product.GetAllWithFilter(p => p.Quantity < p.LowStockThreshold);
            return products;
        }
    }
}
