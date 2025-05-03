using AutoMapper;
using InventoryManagementSystem.BLL.DTOs.Product;
using InventoryManagementSystem.DAL.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.CQRS.Queries.Products
{
    public class ProductDetails
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; }

    }
    public class ProductDetailsQuery : IRequest<ProductDetails>
    {
        public int ProductID { get; set; }
    }
    public class ProductDetailsHandler : IRequestHandler<ProductDetailsQuery, ProductDetails>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductDetailsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ProductDetails> Handle(ProductDetailsQuery request, CancellationToken cancellationToken)
        {

            Product? product = await unitOfWork.Product.GetItemAsync(e => e.ID == request.ProductID,cancellationToken);
            ProductDetails productDetails = mapper.Map<ProductDetails>(product);
            return productDetails;

        }
    }
}
