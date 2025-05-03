using InventoryManagementSystem.BLL.CQRS.Commands.Warehouses;
using InventoryManagementSystem.BLL.DTOs.WarehouseProduct;
using InventoryManagementSystem.DAL.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Commands.WarehouseProductss
{
    public class AddWarehouseProductCommand : IRequest<bool>
    {
        public CreateWarehouseProductDTO WarehouseProductDTO { get; set; }
   
    }
    public class AddWarehouseProductCommandHandler : IRequestHandler<AddWarehouseProductCommand, bool>
    {

        private readonly IUnitOfWork unitOfWork;
        public AddWarehouseProductCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(AddWarehouseProductCommand request, CancellationToken cancellationToken)
        {
            var warehouseExit = await unitOfWork.Warehouse.IsExit(x => x.ID == request.WarehouseProductDTO.WarehouseId, cancellationToken);
            var productExit = await unitOfWork.Product.IsExit(x => x.ID == request.WarehouseProductDTO.ProductId, cancellationToken);
            if(warehouseExit==false|| productExit==false)
                return false;
            


            await unitOfWork.WarehouseProducts.AddAsync(new WarehouseProducts
            {
                ProductId = request.WarehouseProductDTO.ProductId,
                WarehouseId = request.WarehouseProductDTO.WarehouseId,
                Quantity = request.WarehouseProductDTO.Quantity,
            },cancellationToken);
            await unitOfWork.Save(cancellationToken);
            return true;

        }

    }
}
