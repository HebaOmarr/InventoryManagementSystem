using InventoryManagementSystem.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.DTOs.Reports
{
    public class TransactionProductFilter
    {
       
            public DateTime? FromDate { get; set; }
            public DateTime? ToDate { get; set; }
            public int? ProductCategory { get; set; }
            public TransactionType? TransactionType { get; set; }

    }
}
