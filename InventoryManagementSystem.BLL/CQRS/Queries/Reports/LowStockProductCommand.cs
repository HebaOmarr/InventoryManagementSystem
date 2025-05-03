using AutoMapper;
using InventoryManagementSystem.DAL.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Queries.Reports
{
    public class ProductStockREsponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int LowStockThreshold { get; set; }

    }
    public class LowStockProductCommand :IRequest<IEnumerable<ProductStockREsponse>>
    {
    }
    public class LowStockProductCommandHandler : IRequestHandler<LowStockProductCommand, IEnumerable<ProductStockREsponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IMemoryCache memoryCache;

        public LowStockProductCommandHandler(IUnitOfWork unitOfWork,IMapper mapper,IMemoryCache memoryCache)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.memoryCache = memoryCache;
        }
        public async Task<IEnumerable<ProductStockREsponse>?> Handle(LowStockProductCommand request, CancellationToken cancellationToken)
        {
            return await memoryCache.GetOrCreateAsync("Low stock report", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3);
                entry.Priority = CacheItemPriority.Normal;
                var lowStockProducts = await unitOfWork.Product.GetAllWithFilter(Product => Product.Quantity < Product.LowStockThreshold, cancellationToken);
                return mapper.Map<IEnumerable<ProductStockREsponse>>(lowStockProducts);
            });

        }
    }

}
