using AutoMapper;
using InventoryManagementSystem.BLL.CQRS.Queries.Products;
using InventoryManagementSystem.BLL.DTOs.Product;
using InventoryManagementSystem.BLL.DTOs.Warehouse;
using InventoryManagementSystem.BLL.DTOs.WarehouseProduct;
using InventoryManagementSystem.DAL.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Queries.WarehouseProducts
{
    public class GetallWarehouseProductsQuery : IRequest<IEnumerable<CreateWarehouseProductDTO>>
    {
    }
    public class GetallWarehouseProductsQueryHandler : IRequestHandler<GetallWarehouseProductsQuery, IEnumerable<CreateWarehouseProductDTO>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetallWarehouseProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<CreateWarehouseProductDTO>> Handle(GetallWarehouseProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await unitOfWork.WarehouseProducts.ReadAllAsync();
            return mapper.Map<IEnumerable<CreateWarehouseProductDTO>>(products);

        }
    }
}
