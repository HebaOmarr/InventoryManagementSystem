using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Filters
{
    public class ToDateFilter : IFilter
    {
        private readonly DateTime toDate;

        public ToDateFilter(DateTime toDate)
        {
            this.toDate = toDate;
        }
        public Func<InventoryTransaction, bool> GetExpression()
        {
            return e => e.TransactionDate <= toDate;
        }
    }
}
