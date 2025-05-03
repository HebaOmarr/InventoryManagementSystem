using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Entities.Model
{
    public class Product :BaseModel
    {
       public string Name { get; set; } = null!;
        public string Description { get; set; } =string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; }

        public int categoryId { get; set; }

        [ForeignKey(nameof(categoryId))]
        public Category? category { get; set; }

        
        public IEnumerable<InventoryTransaction>? TransferTransactions { get; set; }


    }
}
