using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Filters
{
    public class FromDateFilter :IFilter
    {
        private readonly DateTime fromDate;

        public FromDateFilter(DateTime fromDate)
        {
            this.fromDate = fromDate;
        }

        public Func<InventoryTransaction, bool> GetExpression()
        {
            return e => e.TransactionDate >= fromDate;
        }
    }
}
