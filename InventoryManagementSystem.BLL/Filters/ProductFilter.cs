using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Filters
{
    public class ProductFilter : IFilter
    {
        private readonly int productId;

        public ProductFilter(int ProductId)
        {
            productId = ProductId;
        }
        public Func<InventoryTransaction, bool> GetExpression()
        {
            return e => e.ProductId == productId;
        }
    }
}
