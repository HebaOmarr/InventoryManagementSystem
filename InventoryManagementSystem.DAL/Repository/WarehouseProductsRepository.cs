using InventoryManagementSystem.DAL.DataContext;
using InventoryManagementSystem.DAL.Repository.Contract;
using InventoryManagementSystem.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Repository
{
    public class WarehouseProductsRepository : GenericRepository<WarehouseProducts>, IWarehouseProductsRepository
    {
        private readonly ApplicationDBContext applicationDBContext;
        public WarehouseProductsRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
    }
}
