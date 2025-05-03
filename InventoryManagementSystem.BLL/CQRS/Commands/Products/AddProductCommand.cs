using AutoMapper;
using InventoryManagementSystem.API.DTOs.Product;
using InventoryManagementSystem.API.MappingProfile;
using InventoryManagementSystem.BLL.CQRS.Commands.Products;
using InventoryManagementSystem.DAL.UnitOfWork;
using InventoryManagementSystem.Entities.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Commands.Products
{
    public class CreateProductResult
    {
      public int Id { get; set; }
        public bool IsSuccess { get; set; }

    }

     public class AddProductCommand :IRequest<CreateProductResult>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; }
        public int categoryId { get; set; }
    }
    public class AddProductHandler : IRequestHandler<AddProductCommand, CreateProductResult>
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ILogger<AddProductHandler> logger;

        public AddProductHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddProductHandler> logger)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.logger = logger;
        
        }
        public async Task<CreateProductResult> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            bool categoryIsExit = await unitOfWork.Category.IsExit(x => x.ID == request.categoryId, cancellationToken);
            if(!categoryIsExit)
            {
                new CreateProductResult() { Id=0, IsSuccess = false };
            }
            var product = mapper.Map<Product>(request);
            await unitOfWork.Product.AddAsync(product, cancellationToken);
            await unitOfWork.Save(cancellationToken);
            logger.LogInformation("New Product added successfully");
            return new CreateProductResult { Id = product.ID, IsSuccess = true };
        }
    }
    
}
