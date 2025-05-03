using InventoryManagementSystem.DAL.Repository;
using InventoryManagementSystem.DAL.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IInventoryTransactionRepository InventoryTransaction { get; }
        IWarehouseProductsRepository WarehouseProducts { get; }
        IWarehouseRepository Warehouse { get; }
        Task Save(CancellationToken cancellationToken=default);  
    }
}
