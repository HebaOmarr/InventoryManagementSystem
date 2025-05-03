using AutoMapper;
using InventoryManagementSystem.API.DTOs.Product;
using InventoryManagementSystem.BLL.DTOs.Product;
using InventoryManagementSystem.DAL.UnitOfWork;
using MediatR;

namespace InventoryManagementSystem.BLL.CQRS.Queries.Products
{

    
    public class GetAllProductQuery :IRequest<IEnumerable<ProductDetails>>
    {
        

    }
    public class GetAllProductHandler : IRequestHandler<GetAllProductQuery, IEnumerable<ProductDetails>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<ProductDetails>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var products = await unitOfWork.Product.ReadAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ProductDetails>>(products);

        }
    }
}
