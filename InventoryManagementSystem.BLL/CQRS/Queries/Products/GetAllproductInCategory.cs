using AutoMapper;
using InventoryManagementSystem.BLL.CQRS.Queries.Products;
using InventoryManagementSystem.DAL.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Queries.Products
{
    public class GetAllproductInCategory :IRequest<IEnumerable<ProductDetails>>
    {
        public int CategoryId { get; set; }
    }
   
    public class GetAllproductInCategoryHandler : IRequestHandler<GetAllproductInCategory, IEnumerable<ProductDetails>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllproductInCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<ProductDetails>> Handle(GetAllproductInCategory request, CancellationToken cancellationToken)
        {
        bool CategoryIsExist = await unitOfWork.Category.IsExit(e => e.ID == request.CategoryId, cancellationToken);
            if(!CategoryIsExist)
            {
                return null;
            }
            var products = await unitOfWork.Product.GetAllWithFilter(e=>e.categoryId==request.CategoryId,cancellationToken);
            return mapper.Map<IEnumerable<ProductDetails>>(products);

        }
    }
}
