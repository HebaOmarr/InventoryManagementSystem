using AutoMapper;
using InventoryManagementSystem.BLL.CQRS.Queries.Products;
using InventoryManagementSystem.BLL.DTOs.Product;
using InventoryManagementSystem.BLL.DTOs.Warehouse;
using InventoryManagementSystem.DAL.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Queries.Warehouses
{
    public class GetallWarehouseQuery : IRequest<IEnumerable<CreateWarehouseDTO>>
    {
    }
    public class GetallWarehouseQueryHandler : IRequestHandler<GetallWarehouseQuery, IEnumerable<CreateWarehouseDTO>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetallWarehouseQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<CreateWarehouseDTO>> Handle(GetallWarehouseQuery request, CancellationToken cancellationToken)
        {
            var Warehouses = await unitOfWork.Warehouse.ReadAllAsync();
            return mapper.Map<IEnumerable<CreateWarehouseDTO>>(Warehouses);

        }
    }
}
