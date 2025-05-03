using InventoryManagementSystem.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Entities.Model
{
    public class  InventoryTransaction : BaseModel
    {
      
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType TransactionType { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }


        public int? FromWarehouseId { get; set; }
        [ForeignKey(nameof(FromWarehouseId))]
        public Warehouse? FromWarehouse { get; set; } 

        public int? ToWarehouseId { get; set; }
        [ForeignKey(nameof(ToWarehouseId))]
        public Warehouse? ToWarehouse { get; set; }


        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
    }
   
}
