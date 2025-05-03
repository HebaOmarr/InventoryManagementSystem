using InventoryManagementSystem.DAL.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Commands.WarehouseProductss
{
    public class UpdateWarehouseProductQuntityCommand : IRequest<bool>
    {
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool IsRemoveStock { get; set; }

    }
    public class UpdateWarehouseProductQuntityCommandHandler : IRequestHandler<UpdateWarehouseProductQuntityCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateWarehouseProductQuntityCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateWarehouseProductQuntityCommand request, CancellationToken cancellationToken)
        {
            var warehouseProduct = await unitOfWork.WarehouseProducts.GetItemAsync(x => x.WarehouseId == request.WarehouseId && x.ProductId == request.ProductId,cancellationToken);
            if (warehouseProduct == null)
            {
                //   throw new Exception("No Product Found in WareHOuse");
                return false;
            }

            if (request.IsRemoveStock)
            {
                if (warehouseProduct.Quantity < request.Quantity)
                    return false;
                warehouseProduct.Quantity -= request.Quantity;
            }
            else
                warehouseProduct.Quantity += request.Quantity;

            var isUpdate = await unitOfWork.WarehouseProducts.UpdateAsync(x => x.WarehouseId == request.WarehouseId && x.ProductId == request.ProductId, warehouseProduct,cancellationToken);
              
                    await unitOfWork.Save(cancellationToken);
                    return true;
               
                
            
            
        }
    }

}
