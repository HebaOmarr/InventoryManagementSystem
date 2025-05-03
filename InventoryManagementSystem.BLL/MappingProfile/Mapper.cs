using AutoMapper;
using InventoryManagementSystem.API.DTOs.Product;
using InventoryManagementSystem.BLL.CQRS.Commands.Products;
using InventoryManagementSystem.BLL.CQRS.Queries.Products;
using InventoryManagementSystem.BLL.CQRS.Queries.Reports;
using InventoryManagementSystem.BLL.DTOs.InventoryTransaction;
using InventoryManagementSystem.BLL.DTOs.Product;
using InventoryManagementSystem.BLL.DTOs.Warehouse;
using InventoryManagementSystem.BLL.DTOs.WarehouseProduct;
using InventoryManagementSystem.Entities.Model;

namespace InventoryManagementSystem.API.MappingProfile
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Product, AddProductCommand>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>();


            CreateMap<CreateTransactionDTO, InventoryTransaction>().ReverseMap();
            CreateMap<Warehouse, CreateWarehouseDTO>().ReverseMap();
            CreateMap<WarehouseProducts, CreateWarehouseProductDTO>().ReverseMap();
            CreateMap<WarehouseProducts, WarehousePoductResponse>().ReverseMap();



            CreateMap<Product, ProductDetails>().ReverseMap();
            CreateMap<Product, ProductStockREsponse>().ForMember(des => des.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(des => des.ProductId, opt => opt.MapFrom(src => src.ID));
            CreateMap<InventoryTransaction, TransactionHistorResponse>();



            CreateMap<UpdateProductDTO, Product>()
    .ForMember(dest => dest.Name,
        opt => opt.Condition(src => !string.IsNullOrWhiteSpace(src.Name)))
    .ForMember(dest => dest.Description,
        opt => opt.Condition(src => !string.IsNullOrWhiteSpace(src.Description)))
    .ForMember(dest => dest.Price,
        opt => opt.Condition(src => src.Price != 0))
    .ForMember(dest => dest.Quantity,
        opt => opt.Condition(src => src.Quantity != 0))
    .ForMember(dest => dest.LowStockThreshold,
        opt => opt.Condition(src => src.LowStockThreshold != 0))
    .ForMember(dest => dest.categoryId,
        opt => opt.Condition(src => src.categoryId != 0));

        }




    }
}
