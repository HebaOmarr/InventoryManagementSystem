using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.BLL.Notification
{
    public interface INotificarionService
    {
        Task NotifyLowStock(int productId, string productName, int quantity);

    }
}
