using InventoryManagementSystem.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Filters
{
    public class TranscationTypeFilter : IFilter
    {
        private readonly TransactionType transactionType;
        public TranscationTypeFilter(TransactionType transactionType)
        {
            this.transactionType = transactionType;
        }
        public Func<InventoryTransaction, bool> GetExpression()
        {
            return e => e.TransactionType == transactionType;
        }
    }
}
