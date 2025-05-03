using AutoMapper;
using InventoryManagementSystem.API.MappingProfile;
using InventoryManagementSystem.BLL.DTOs.Product;
using InventoryManagementSystem.DAL.UnitOfWork;
using InventoryManagementSystem.Entities.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Commands.Products
{
    public class UpdateProductResult
    {
        public int Id { get; set; }

    }

    public class UpdateProductCommand : IRequest<UpdateProductResult>
    {
        public int productId { get; set; }
       public UpdateProductDTO updateProductDTO { get; set; }

    }
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public UpdateProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;

        }
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product? productIsExit = await unitOfWork.Product.GetItemAsync(e => e.ID == request.productId,cancellationToken);
            if (productIsExit == null)
                throw new Exception("Product Not Found");

            bool categoryIsExit = await unitOfWork.Category.IsExit(x => x.ID == request.updateProductDTO.categoryId, cancellationToken);
            if (!categoryIsExit)
            {
                throw new Exception("Category Not Found");
            }


            mapper.Map(request.updateProductDTO, productIsExit);

           

            var Result = await unitOfWork.Product.UpdateAsync(e => e.ID == request.productId, productIsExit,cancellationToken);
            if (Result)
            {
                await unitOfWork.Save(cancellationToken);
                return new UpdateProductResult { Id = productIsExit.ID };
            }
             throw new Exception("Error While Update Product");

        }
    }
}
