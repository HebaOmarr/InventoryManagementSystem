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
    public class WarehouseRepository :GenericRepository<Warehouse>, IWarehouseRepository
    {

        private readonly ApplicationDBContext applicationDBContext;

        public WarehouseRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
            this.applicationDBContext = applicationDBContext;
        }
    }
}
