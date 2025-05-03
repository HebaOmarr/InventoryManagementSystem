using InventoryManagementSystem.DAL.UnitOfWork;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Commands.Products
{
    public class UpdateProductQuntityCommand : IRequest<bool>
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public bool IsRemoveStock { get; set; }
        public bool IsSuccess { get; set; }

    }
    public class UpdateProductQuntityCommandHandler : IRequestHandler<UpdateProductQuntityCommand,bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<UpdateProductQuntityCommandHandler> logger;

        public UpdateProductQuntityCommandHandler(IUnitOfWork unitOfWork,ILogger<UpdateProductQuntityCommandHandler> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }
        public async Task<bool> Handle(UpdateProductQuntityCommand request, CancellationToken cancellationToken)
        {
            Product? product = await unitOfWork.Product.GetItemAsync(e => e.ID == request.ProductId, cancellationToken);
            if (product == null)
                return false;

            if (request.IsRemoveStock)
            {  if(product.Quantity<request.Quantity) 
                return false;
                product.Quantity -= request.Quantity;

                if (product.Quantity < product.LowStockThreshold)
                    logger.LogWarning($"Product '{product.Name}' is low in stock: {product.Quantity}");

            }
            else product.Quantity += request.Quantity;
            bool isUpdate = await unitOfWork.Product.UpdateAsync(e => e.ID == request.ProductId, product, cancellationToken);
            if (isUpdate)
            {
                await unitOfWork.Save(cancellationToken);

                return true;
            }
            return false;

        }
    }

}
