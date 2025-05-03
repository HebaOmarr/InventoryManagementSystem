using InventoryManagementSystem.BLL.DTOs.Category;
using InventoryManagementSystem.BLL.DTOs.Warehouse;
using InventoryManagementSystem.DAL.UnitOfWork;
using InventoryManagementSystem.Entities.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Commands.Warehouses
{
    public class AddWarehouseCommand : IRequest<bool>
    {
       public CreateWarehouseDTO warehouseDTO { get; }
        public AddWarehouseCommand(CreateWarehouseDTO warehouseDTO)
        {
            this.warehouseDTO = warehouseDTO;
        }
    }
    public class AddWarehouseCommandHandler : IRequestHandler<AddWarehouseCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        public AddWarehouseCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(AddWarehouseCommand request, CancellationToken cancellationToken)
        {
            
            await unitOfWork.Warehouse.AddAsync( new Warehouse
            {
              Name = request.warehouseDTO.Name,
                Location = request.warehouseDTO.Location,
                Capacity = request.warehouseDTO.Capacity
            }, cancellationToken);

            await unitOfWork.Save(cancellationToken);
            return true;
        }
    }
}
