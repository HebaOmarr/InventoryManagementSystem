using AutoMapper;
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

    public class IsExitWarehouseProductCommand : IRequest<bool>
    {
        public int WarehouseId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public bool IsRemoveStock { get; set; }


    }

    public class IsExitWarehouseProductCommandHandler : IRequestHandler<IsExitWarehouseProductCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public IsExitWarehouseProductCommandHandler(IUnitOfWork unitOfWork,IMediator mediator,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mediator = mediator;
            this.mapper = mapper;
        }
        public async Task<bool> Handle(IsExitWarehouseProductCommand request, CancellationToken cancellationToken)
        {

            var warehouseProductEXit = await unitOfWork.WarehouseProducts.IsExit(x => x.WarehouseId == request.WarehouseId && x.ProductId == request.ProductId, cancellationToken);
            if (!warehouseProductEXit&&!request.IsRemoveStock)
            {
                await mediator.Send(new AddWarehouseProductCommand{
                    WarehouseProductDTO = new CreateWarehouseProductDTO
                    {
                        ProductId = request.ProductId,
                        WarehouseId = request.WarehouseId,
                        Quantity =0
                    }
                }, cancellationToken);
            }

            return true;
        }
    }
}
