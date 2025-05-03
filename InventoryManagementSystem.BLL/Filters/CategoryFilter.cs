using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Filters
{
    public class CategoryFilter : IFilter
    {
        private readonly int categoryId;
        public CategoryFilter(int categoryId)
        {
            this.categoryId = categoryId;
        }

        public Func<InventoryTransaction, bool> GetExpression()
        {
 return e=>e.Product.categoryId== categoryId;
        }
    }
}
