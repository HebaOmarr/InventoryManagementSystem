using InventoryManagementSystem.DAL.DataContext;
using InventoryManagementSystem.DAL.Repository;
using InventoryManagementSystem.DAL.Repository.Contract;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace InventoryManagementSystem.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext applicationDBContext;
        public IProductRepository Product { get; }
        public ICategoryRepository Category { get; }
        public IInventoryTransactionRepository InventoryTransaction { get; }
        public IWarehouseProductsRepository WarehouseProducts { get; }
        public IWarehouseRepository Warehouse { get; }

        public UnitOfWork(ApplicationDBContext applicationDBContext , IProductRepository ProductRepository 
            ,ICategoryRepository category,IInventoryTransactionRepository inventoryTransaction
            , IWarehouseProductsRepository WarehouseProducts, IWarehouseRepository Warehouse
            )
        {
            this.applicationDBContext = applicationDBContext;
            Product = ProductRepository;
            Category= category;
            InventoryTransaction = inventoryTransaction;
            this.WarehouseProducts = WarehouseProducts;
            this.Warehouse = Warehouse;


        }

        

        public async void Dispose()
        {
            await applicationDBContext.DisposeAsync();
        }
        public async Task Save(CancellationToken cancellationToken)
        {
            await applicationDBContext.SaveChangesAsync(cancellationToken=default);
        }
    }
}
