using InventoryManagementSystem.API.DTOs.Product;
using InventoryManagementSystem.API.ViewModel;
using InventoryManagementSystem.BLL.CQRS.Commands;
using InventoryManagementSystem.BLL.CQRS.Commands.Products;
using InventoryManagementSystem.BLL.CQRS.Commands.WarehouseProductss;
using InventoryManagementSystem.BLL.CQRS.Queries;
using InventoryManagementSystem.BLL.CQRS.Queries.Products;
using InventoryManagementSystem.BLL.DTOs.Product;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InventoryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("CreateProduct")]
        public async Task<ApiResponseResult<CreateProductResult>> AddProduct([FromBody] AddProductCommand request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return ApiResponseResult<CreateProductResult>.Error( "Invalid Model State");

            var result = await mediator.Send(request, cancellationToken);
            return ApiResponseResult<CreateProductResult>.Success(result, "Product Created Successfully");
        }




        [HttpPut("UpdateProduct/{id:int}")]
        public async Task<ApiResponseResult<UpdateProductResult>> UpdateProduct(int id,[FromBody] UpdateProductDTO request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return ApiResponseResult<UpdateProductResult>.Error( "Invalid Model State");

            var result = await mediator.Send(new UpdateProductCommand { productId=id,updateProductDTO=request}, cancellationToken);
            return ApiResponseResult<UpdateProductResult>.Success(result, "Product Updated Successfully");

        }



        [HttpDelete("DeleteProduct/{id:int}")]
        public async Task<ApiResponseResult<int>> DeleteProduct(int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeleteProductCommand { ID = id},cancellationToken );
          if(result)
            {
                return ApiResponseResult<int>.Success(id,"Product Delete Successfully");
            }
            return ApiResponseResult<int>.Error("Product Delete Failed");
        }



        [HttpGet("GetProduct/{id:int}")]

        public async Task<ApiResponseResult<ProductDetails>> GetProduct(int id, CancellationToken cancellationToken)
        {
          
            var result = await mediator.Send(new ProductDetailsQuery { ProductID=id},cancellationToken);
            if (result == null)
                  return ApiResponseResult<ProductDetails>.Error("Product Not Found",404);

            return ApiResponseResult<ProductDetails>.Success(result);
        }



        [HttpGet("GetProductList")]
        public async Task<ApiResponseResult<IEnumerable<ProductDetails>>> GetProductList(CancellationToken cancellationToken)
        {

            var result = await mediator.Send(new GetAllProductQuery (),cancellationToken);
            if (result == null)
                return ApiResponseResult<IEnumerable<ProductDetails>>.Error("List is Empty");

            return ApiResponseResult<IEnumerable<ProductDetails>>.Success(result);
        }

        [HttpGet("GetProductListinCategory/{Categoryid:int}")]
        public async Task<ApiResponseResult<IEnumerable<ProductDetails>>> GetProductList(int Categoryid, CancellationToken cancellationToken)
        {

            var result = await mediator.Send(new GetAllproductInCategory { CategoryId=Categoryid}, cancellationToken);
            if (result == null)
                return ApiResponseResult<IEnumerable<ProductDetails>>.Error("No product in Category");

            return ApiResponseResult<IEnumerable<ProductDetails>>.Success(result);
        }







        [HttpPost("TestLowstockNotifaction")]
        public async Task<ApiResponseResult<bool>> Test([FromBody] UpdateProductQuntityCommand request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return ApiResponseResult<bool>.Error("Invalid Model State");

            var result = await mediator.Send(request, cancellationToken);

            if (result == false)
                return ApiResponseResult<bool>.Error();

            return ApiResponseResult<bool>.Success(result);
        }










    }
}
