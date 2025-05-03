using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Filters
{
    public interface IFilter
    {
        Func<InventoryTransaction, bool> GetExpression();

    }
}
